using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;

namespace CodePlayground.Core.Languages;

public class CSharpHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return EnumHelper.GetEnumStringValue(DockerImages.CSharp);
    }

    public string GetExecutionCommand(string code)
    {
        var sanitizedCode = code
            .Replace("\"", "\\\"")
            .Replace("'", "'\"'\"'")
            .Replace("\n", "\\n");

        return $"""
                    mkdir -p /code/TemplateProject &&
                    echo '{code}' > /code/TemplateProject/Program.cs &&
                    cd /code/TemplateProject &&
                    dotnet run
                """;
    }
}