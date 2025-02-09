using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CodePlayground.Tests.API.Controllers;
using CodePlayground.API.Controllers;

public class CodeControllerTests
{
    [Fact]
    public async Task ExecuteCode_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var codeExecutionService = new Mock<ICodeExecutionService>();
        codeExecutionService
            .Setup(service => service.ExecuteCodeAsync(It.IsAny<CodeExecutionRequest>()))
            .ReturnsAsync(new CodeExecutionResult());

        var codeController = new CodeController(codeExecutionService.Object);

        // Act
        var result = await codeController.ExecuteCode(new CodeExecutionRequest());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ExecuteCode_WhenCalled_ReturnsCodeExecutionResult()
    {
        // Arrange
        var codeExecutionService = new Mock<ICodeExecutionService>();
        var expected = new CodeExecutionResult();
        codeExecutionService
            .Setup(service => service.ExecuteCodeAsync(It.IsAny<CodeExecutionRequest>()))
            .ReturnsAsync(expected);

        var codeController = new CodeController(codeExecutionService.Object);

        // Act
        var result = await codeController.ExecuteCode(new CodeExecutionRequest()) as OkObjectResult;

        // Assert
        Assert.Equal(expected, result?.Value);
    }

    [Fact]
    public async Task ExecuteCode_WhenCalled_ReturnsBadRequestResult()
    {
        // Arrange
        var codeExecutionService = new Mock<ICodeExecutionService>();
        codeExecutionService
            .Setup(service => service.ExecuteCodeAsync(It.IsAny<CodeExecutionRequest>()))
            .ThrowsAsync(new Exception());

        var codeController = new CodeController(codeExecutionService.Object);

        // Act
        var result = await codeController.ExecuteCode(new CodeExecutionRequest());

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}