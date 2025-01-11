using CodePlayground.Core.Enums;

namespace CodePlayground.Core.Helpers;

public static class CodeExecutionHelper
{
    public static string? GetDockerImageForLanguage(SupportedLanguages language)
    {
        return language switch
        {
            SupportedLanguages.CSharp => "mcr.microsoft.com/dotnet/sdk:5.0",
            SupportedLanguages.Python => "python:3.9",
            SupportedLanguages.JavaScript => "node:14",
            _ => null
        };
    }

    public static string GetCommandForLanguage(SupportedLanguages language, string code)
    {
        var sanitizedCode = code
            .Replace("'", "'\"'\"'") // Escape single quotes for shell compatibility
            .Replace("\n", "\\n"); // Replace newlines with literal "\n" for shell

        return language switch
        {
            SupportedLanguages.CSharp =>
                $"mkdir -p /code && echo \"{sanitizedCode}\" > /code/temp.cs && dotnet run /code/temp.cs",
            SupportedLanguages.Python =>
                $"mkdir -p /code && echo \"{sanitizedCode}\" > /code/temp.py && python /code/temp.py",
            SupportedLanguages.JavaScript =>
                $"mkdir -p /code && echo \"{sanitizedCode}\" > /code/temp.js && node /code/temp.js",
            _ => throw new ArgumentException("Unsupported language")
        };
    }
}