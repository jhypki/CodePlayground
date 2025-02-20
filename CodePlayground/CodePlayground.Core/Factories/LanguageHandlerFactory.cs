using CodePlayground.Core.Enums;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Languages;

namespace CodePlayground.Core.Factories;
public class LanguageHandlerFactory : ILanguageHandlerFactory
{
    public ILanguageHandler GetHandler(SupportedLanguages language)
    {
        return language switch
        {
            SupportedLanguages.JavaScript => new JavaScriptHandler(),
    SupportedLanguages.CSharp => new CSharpHandler(),
    SupportedLanguages.Python => new PythonHandler(),
    SupportedLanguages.TypeScript => new TypeScriptHandler(),
    SupportedLanguages.Java => new JavaHandler(),
    SupportedLanguages.Go => new GoHandler(),
    SupportedLanguages.Ruby => new RubyHandler(),
    SupportedLanguages.Rust => new RustHandler(),
    SupportedLanguages.Kotlin => new KotlinHandler(),
    SupportedLanguages.PHP => new PHPHandler(),
    SupportedLanguages.Swift => new SwiftHandler(),
    SupportedLanguages.Bash => new BashHandler(),
    SupportedLanguages.Perl => new PerlHandler(),
    SupportedLanguages.Elixir => new ElixirHandler(),
    SupportedLanguages.Julia => new JuliaHandler(),
            _ => throw new NotImplementedException()
        };
    }
}