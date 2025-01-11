using CodePlayground.Core.Interfaces;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CodePlayground.Core.Docker;

public class DockerManager(DockerClient dockerClient) : IDockerManager
{
    public async Task<CreateContainerParameters> CreateContainerParameters(string imageName, string command)
    {
        await EnsureImageExistsAsync(imageName);

        return new CreateContainerParameters
        {
            Image = imageName,
            Cmd =
            [
                "sh",
                "-c",
                command
            ],
            HostConfig = new HostConfig
            {
                AutoRemove = true,
                Memory = 512 * 1024 * 1024,
                NanoCPUs = 500_000_000
            }
        };
    }

    public async Task<(string StdOut, string StdErr, long ExitCode)> RunContainerAsync(
        CreateContainerParameters parameters)
    {
        var container = await dockerClient.Containers.CreateContainerAsync(parameters);
        await dockerClient.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());

        var (stdout, stderr, exitCode) = await GetContainerLogsAsync(container.ID);

        return (stdout, stderr, exitCode);
    }

    private async Task EnsureImageExistsAsync(string image)
    {
        var images = await dockerClient.Images.ListImagesAsync(new ImagesListParameters
        {
            Filters = new Dictionary<string, IDictionary<string, bool>>
            {
                { "reference", new Dictionary<string, bool> { { image, true } } }
            }
        });
        if (!images.Any())
            await dockerClient.Images.CreateImageAsync(
                new ImagesCreateParameters { FromImage = image },
                null,
                new Progress<JSONMessage>(message => { Console.WriteLine(message.Status); })
            );
    }

    private async Task<(string stdout, string stderr, long exitCode)> GetContainerLogsAsync(string containerId)
    {
        var logStream = await dockerClient.Containers.GetContainerLogsAsync(containerId,
            false,
            new ContainerLogsParameters
            {
                Follow = true,
                ShowStdout = true,
                ShowStderr = true
            });

        var stdout = new MemoryStream();
        var stderr = new MemoryStream();

        await logStream.CopyOutputToAsync(null, stdout, stderr, CancellationToken.None);

        stdout.Seek(0, SeekOrigin.Begin);
        stderr.Seek(0, SeekOrigin.Begin);

        var stdoutResult = await new StreamReader(stdout).ReadToEndAsync();
        var stderrResult = await new StreamReader(stderr).ReadToEndAsync();

        var containerInspect = await dockerClient.Containers.InspectContainerAsync(containerId);
        var exitCode = containerInspect.State.ExitCode;

        Console.WriteLine($"StdOut: {stdoutResult}");
        Console.WriteLine($"StdErr: {stderrResult}");
        Console.WriteLine($"ExitCode: {exitCode}");

        return (stdoutResult, stderrResult, exitCode);
    }
}