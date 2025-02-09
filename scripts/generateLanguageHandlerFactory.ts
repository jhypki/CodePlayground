import path from "path";
import fs from "fs";
import languages from "../languages.json";

export const generateLanguageHandlerFactory = (): void => {
  const factoriesDir = path.join(
    process.cwd(),
    "../CodePlayground/CodePlayground.Core/Factories"
  );

  const fileContent = `using CodePlayground.Core.Enums;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Languages;

namespace CodePlayground.Core.Factories;
public abstract class LanguageHandlerFactory : ILanguageHandlerFactory
{
    public ILanguageHandler GetHandler(SupportedLanguages language)
    {
        return language switch
        {
        ${languages
          .map(
            (language) =>
              `    SupportedLanguages.${language.name} => new ${language.name}Handler(),`
          )
          .join("\n")}
            _ => throw new NotImplementedException()
        };
    }
}`;

  fs.writeFileSync(
    path.join(factoriesDir, "LanguageHandlerFactory.cs"),
    fileContent
  );
};
