using CodePlayground.Core.Enums;

namespace CodePlayground.Core.Interfaces;

public interface ILanguageHandlerFactory
{
    ILanguageHandler GetHandler(SupportedLanguages language);
}
