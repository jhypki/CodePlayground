using CodePlayground.Core.Enums;
using CodePlayground.Core.Factories;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Models;
using Docker.DotNet.Models;
using Moq;

namespace CodePlayground.Tests.API.Services;
using CodePlayground.API.Services;

public class CodeExecutionServiceTests
{
    [Fact]
    public async Task ExecuteCodeAsync_WhenLanguageIsUnsupported_ThrowsArgumentException()
    {
        // Arrange
        var dockerManager = new Mock<IDockerManager>();
        var codeExecutionService = new CodeExecutionService(dockerManager.Object, new Mock<ILanguageHandlerFactory>().Object);
        var request = new CodeExecutionRequest
        {
            Language = "unsupported"
        };

        // Act
        async Task Act() => await codeExecutionService.ExecuteCodeAsync(request);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(Act);
    }

    [Fact]
    public async Task ExecuteCodeAsync_WhenLanguageIsSupported_ReturnsCodeExecutionResult()
    {
        // Arrange
        var dockerManager = new Mock<IDockerManager>();
        var handlerMock = new Mock<ILanguageHandler>();

        handlerMock.Setup(handler => handler.GetDockerImage()).Returns("code-playground/julia");
        handlerMock.Setup(handler => handler.GetExecutionCommand(It.IsAny<string>())).Returns("echo 'Hello, World!'");

        var languageHandlerFactoryMock = new Mock<ILanguageHandlerFactory>();
        languageHandlerFactoryMock.Setup(factory => factory.GetHandler(SupportedLanguages.Julia))
            .Returns(handlerMock.Object);

        dockerManager.Setup(dm => dm.CreateContainerParameters(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new CreateContainerParameters { Image = "code-playground/julia", Cmd = new[] { "sh", "-c", "echo 'Hello, World!'" } });

        dockerManager.Setup(dm => dm.RunContainerAsync(It.IsAny<CreateContainerParameters>()))
            .ReturnsAsync(("Hello, World!", string.Empty, 0));

        var codeExecutionService = new CodeExecutionService(dockerManager.Object, languageHandlerFactoryMock.Object);

        var request = new CodeExecutionRequest
        {
            Language = "julia",
            Code = "println(\"Hello, World!\")"
        };

        // Act
        var result = await codeExecutionService.ExecuteCodeAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Hello, World!", result.StdOut);
        Assert.Empty(result.StdErr);
        Assert.Equal(0, result.ExitCode);
    }
}