using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;
using System;

namespace CodePlayground.Core.Languages;
public class TypeScriptHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "code-playground/typescript";
    }

    public string GetExecutionCommand(string code)
    {
        var base64Code = CodeSanitizer.ToBase64(code);

        var runCommand = $"mkdir -p /code && echo \"{base64Code}\" | base64 -d > /code/temp.ts && tsc /code/temp.ts && node /code/temp.js";

        return runCommand;
    }
}