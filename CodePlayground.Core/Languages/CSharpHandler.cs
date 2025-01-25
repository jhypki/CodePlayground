using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;
using System;

namespace CodePlayground.Core.Languages;
public class CSharpHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "code-playground/csharp";
    }

    public string GetExecutionCommand(string code)
    {
        var base64Code = CodeSanitizer.ToBase64(code);

        var runCommand = $"mkdir -p /code/TemplateProject && echo \"{base64Code}\" | base64 -d > /code/TemplateProject/Program.cs && cd /code/TemplateProject && dotnet run";

        return runCommand;
    }
}