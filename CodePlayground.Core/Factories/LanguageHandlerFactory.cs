using CodePlayground.Core.Enums;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Languages;

namespace CodePlayground.Core.Factories;

public class LanguageHandlerFactory
{
    public static ILanguageHandler GetHandler(SupportedLanguages language)
    {
        return language switch
        {
            SupportedLanguages.Python => new PythonHandler(),
            SupportedLanguages.JavaScript => new JavaScriptHandler(),
            SupportedLanguages.CSharp => new CSharpHandler(),
            _ => throw new NotImplementedException()
        };
    }
}