using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;
using System;

namespace CodePlayground.Core.Languages;
public class KotlinHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "code-playground/kotlin";
    }

    public string GetExecutionCommand(string code)
    {
        var base64Code = CodeSanitizer.ToBase64(code);

        var runCommand = $"mkdir -p /code && echo \"{base64Code}\" | base64 -d > /code/Main.kt && cd /code && kotlinc Main.kt -include-runtime -d main.jar && java -jar main.jar";

        return runCommand;
    }
}