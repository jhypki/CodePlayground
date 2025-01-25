import path from "path";
import fs from "fs";
import languages from "../languages.json";

export const generateHandlers = (): void => {
  const handlersDir = path.join(
    process.cwd(),
    "../CodePlayground/CodePlayground.Core/Languages"
  );

  for (const language of languages) {
    fs.mkdirSync(handlersDir, { recursive: true });

    const fileContent = `using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;
using System;

namespace CodePlayground.Core.Languages;
public class ${language.name}Handler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "code-playground/${language.language}";
    }

    public string GetExecutionCommand(string code)
    {
        var base64Code = CodeSanitizer.ToBase64(code);

        var runCommand = $"${language.runCommand}";

        return runCommand;
    }
}`;

    fs.writeFileSync(
      path.join(handlersDir, `${language.name}Handler.cs`),
      fileContent
    );
  }
};
