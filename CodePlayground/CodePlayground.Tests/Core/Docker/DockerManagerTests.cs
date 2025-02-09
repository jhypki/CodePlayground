using Docker.DotNet;
using Docker.DotNet.Models;
using Moq;

namespace CodePlayground.Tests.Core.Docker;
using CodePlayground.Core.Docker;

public class DockerManagerTests
{
    [Fact]
    public async Task CreateContainerParameters_WhenCalled_ReturnsCreateContainerParameters()
    {
        // Arrange
        var dockerClient = new Mock<IDockerClient>();
        var mockImageOperations = new Mock<IImageOperations>();
        dockerClient.Setup(d => d.Images).Returns(mockImageOperations.Object);

        mockImageOperations
            .Setup(img => img.ListImagesAsync(It.IsAny<ImagesListParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ImagesListResponse>());

        var dockerManager = new DockerManager(dockerClient.Object);
        const string imageName = "code-playground/julia";
        const string command = "echo 'Hello, World!'";

        // Act
        var result = await dockerManager.CreateContainerParameters(imageName, command);

        // Assert
        Assert.Equal(imageName, result.Image);
        Assert.Equal(new[] { "sh", "-c", command }, result.Cmd);
        Assert.Equal(512 * 1024 * 1024, result.HostConfig.Memory);
        Assert.Equal(500_000_000, result.HostConfig.NanoCPUs);
    }

    [Fact]
    public async Task RunContainerAsync_WhenCalled_ReturnsStdOutStdErrAndExitCode()
    {
        // Arrange
        var dockerClient = new Mock<IDockerClient>();
        var mockContainerOperations = new Mock<IContainerOperations>();
        dockerClient.Setup(d => d.Containers).Returns(mockContainerOperations.Object);

        mockContainerOperations
            .Setup(parameters =>
                parameters.CreateContainerAsync(It.IsAny<CreateContainerParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CreateContainerResponse { ID = "test-container-id" });

        mockContainerOperations
            .Setup(parameters =>
                parameters.StartContainerAsync(It.IsAny<string>(), It.IsAny<ContainerStartParameters>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var emptyStream = new MultiplexedStream(new MemoryStream(), true);

        mockContainerOperations
            .Setup(parameters =>
                parameters.GetContainerLogsAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<ContainerLogsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyStream);

        mockContainerOperations
            .Setup(parameters => parameters.InspectContainerAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ContainerInspectResponse { State = new ContainerState { ExitCode = 0 } });

        var dockerManager = new DockerManager(dockerClient.Object);
        var createContainerParameters = new CreateContainerParameters();

        // Act
        var result = await dockerManager.RunContainerAsync(createContainerParameters);

        // Assert
        Assert.Equal(string.Empty, result.StdOut);
        Assert.Equal(string.Empty, result.StdErr);
        Assert.Equal(0, result.ExitCode);
    }
}