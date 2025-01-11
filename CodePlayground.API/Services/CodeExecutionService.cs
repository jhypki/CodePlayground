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
                AutoRemove = true,
                Memory = 512 * 1024 * 1024,
                NanoCPUs = 500_000_000
            }
        };

        var container = await dockerClient.Containers.CreateContainerAsync(containerConfig);

        await dockerClient.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());

        var (stdout, stderr) = await GetContainerLogsAsync(container.ID);

        return new CodeExecutionResult
        {
            StdOut = stdout,
            StdErr = stderr,
            ExitCode = 0
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

    private async Task<(string stdout, string stderr)> GetContainerLogsAsync(string containerId)
    {
        var logStream = await dockerClient.Containers.GetContainerLogsAsync(containerId,
            false,
            new ContainerLogsParameters
            {
                Follow = true,
                ShowStdout = true,
                ShowStderr = true
            });

        var (stdout, stderr) = await logStream.ReadOutputToEndAsync(CancellationToken.None);

        return (stdout, stderr);
    }
}