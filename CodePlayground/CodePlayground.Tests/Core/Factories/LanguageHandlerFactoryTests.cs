using CodePlayground.Core.Enums;
using CodePlayground.Core.Languages;

namespace CodePlayground.Tests.Core.Factories;
using CodePlayground.Core.Factories;

public class LanguageHandlerFactoryTests
{
    [Theory]
    [InlineData(SupportedLanguages.JavaScript, typeof(JavaScriptHandler))]
    [InlineData(SupportedLanguages.CSharp, typeof(CSharpHandler))]
    [InlineData(SupportedLanguages.Python, typeof(PythonHandler))]
    [InlineData(SupportedLanguages.TypeScript, typeof(TypeScriptHandler))]
    [InlineData(SupportedLanguages.Java, typeof(JavaHandler))]
    [InlineData(SupportedLanguages.Go, typeof(GoHandler))]
    [InlineData(SupportedLanguages.Ruby, typeof(RubyHandler))]
    [InlineData(SupportedLanguages.Rust, typeof(RustHandler))]
    [InlineData(SupportedLanguages.Kotlin, typeof(KotlinHandler))]
    [InlineData(SupportedLanguages.PHP, typeof(PHPHandler))]
    [InlineData(SupportedLanguages.Swift, typeof(SwiftHandler))]
    [InlineData(SupportedLanguages.Bash, typeof(BashHandler))]
    [InlineData(SupportedLanguages.Perl, typeof(PerlHandler))]
    [InlineData(SupportedLanguages.Elixir, typeof(ElixirHandler))]
    [InlineData(SupportedLanguages.Julia, typeof(JuliaHandler))]
    public void GetHandler_WhenLanguageIsSupported_ReturnsCorrectHandler(SupportedLanguages language, Type expectedHandlerType)
    {
        // Arrange
        var factory = new LanguageHandlerFactory();

        // Act
        var handler = factory.GetHandler(language);

        // Assert
        Assert.NotNull(handler);
        Assert.IsType(expectedHandlerType, handler);
    }

    [Fact]
    public void GetHandler_WhenLanguageIsNotSupported_ThrowsNotImplementedException()
    {
        // Arrange
        var factory = new LanguageHandlerFactory();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => factory.GetHandler((SupportedLanguages)999));
    }
}
