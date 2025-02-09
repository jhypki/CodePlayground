using Xunit.Abstractions;

namespace CodePlayground.Tests.Core.Helpers;
using CodePlayground.Core.Helpers;

public class CodeSanitizerTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CodeSanitizerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Sanitize_WhenCalled_ReturnsSanitizedCode()
    {
        // Arrange
        var code = "using System;\nConsole.WriteLine(\"Hello, World!\");";

        // Act
        var result = CodeSanitizer.Sanitize(code);

        // Assert
        Assert.Equal("using System;\\nConsole.WriteLine(\\\"Hello, World!\\\");", result);
    }

    [Fact]
    public void ToBase64_WhenCalled_ReturnsBase64EncodedCode()
    {
        // Arrange
        var code = "using System;\nConsole.WriteLine(\"Hello, World!\");";

        // Act
        var result = CodeSanitizer.ToBase64(code);

        // Assert
        Assert.Equal("dXNpbmcgU3lzdGVtOwpDb25zb2xlLldyaXRlTGluZSgiSGVsbG8sIFdvcmxkISIpOw==", result);
    }
}