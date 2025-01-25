using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;
using System;

namespace CodePlayground.Core.Languages;
public class BashHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "code-playground/bash";
    }

    public string GetExecutionCommand(string code)
    {
        var base64Code = CodeSanitizer.ToBase64(code);

        var runCommand = $"mkdir -p /code && echo \"{base64Code}\" | base64 -d > /code/script.sh && bash /code/script.sh";

        return runCommand;
    }
}