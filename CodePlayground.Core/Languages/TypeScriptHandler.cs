using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;

namespace CodePlayground.Core.Languages;

public class TypeScriptHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return EnumHelper.GetEnumStringValue(DockerImages.TypeScript);
    }

    public string GetExecutionCommand(string code)
    {
        var sanitizedCode = code
            .Replace("'", "'\"'\"'")
            .Replace("\n", "\\n");

        return
            $"mkdir -p /code && echo \"{sanitizedCode}\" > /code/temp.ts && tsc /code/temp.ts && node /code/temp.js";
    }
}