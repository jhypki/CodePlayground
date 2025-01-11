using CodePlayground.Core.Interfaces;

namespace CodePlayground.Core.Languages;

public class CSharpHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "mcr.microsoft.com/dotnet/sdk:5.0";
    }

    public string GetExecutionCommand(string code)
    {
        var sanitizedCode = code
            .Replace("\"", "\\\"") // Escape double quotes
            .Replace("'", "'\"'\"'") // Escape single quotes
            .Replace("\n", "\\n"); // Replace newlines

        const string pattern = """
                               
                                           mkdir -p /code &&
                                           cd /code &&
                                           dotnet new console --force &&
                                           echo "{0}" > /code/Program.cs &&
                                           dotnet run
                               """;

        return string.Format(pattern, sanitizedCode);
    }
}