using CodePlayground.Core.Factories;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Models;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CodePlayground.API.Services;

public class CodeExecutionService(DockerClient dockerClient) : ICodeExecutionService
{
    public async Task<CodeExecutionResult> ExecuteCodeAsync(CodeExecutionRequest request)
    {
        var handler = LanguageHandlerFactory.GetHandler(request.Language);

        var image = handler.GetDockerImage();
        if (image == null) throw new ArgumentException("Unsupported language");

        await EnsureImageExistsAsync(image);

        var command = handler.GetExecutionCommand(request.Code);

        var containerConfig = new CreateContainerParameters
        {
            Image = image,
            Cmd =
            [
                "sh",
                "-c",
                command
            ],
            HostConfig = new HostConfig
            {
                Memory = 512 * 1024 * 1024,
                NanoCPUs = 500_000_000
            }
        };

        var container = await dockerClient.Containers.CreateContainerAsync(containerConfig);

        await dockerClient.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());

        var (stdout, stderr, exitCode) = await GetContainerLogsAsync(container.ID);

        await dockerClient.Containers.StopContainerAsync(container.ID, new ContainerStopParameters());
        await dockerClient.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());

        return new CodeExecutionResult
        {
            StdOut = stdout,
            StdErr = stderr,
            ExitCode = exitCode
        };
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