using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;
using System;

namespace CodePlayground.Core.Languages;
public class ScalaHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "code-playground/scala";
    }

    public string GetExecutionCommand(string code)
    {
        var base64Code = CodeSanitizer.ToBase64(code);

        var runCommand = $"mkdir -p /code && echo \"{base64Code}\" | base64 -d > /code/Main.scala && cd /code && scalac Main.scala && scala Main";

        return runCommand;
    }
}