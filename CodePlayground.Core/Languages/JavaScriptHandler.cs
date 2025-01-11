using CodePlayground.Core.Interfaces;

namespace CodePlayground.Core.Languages;

public class JavaScriptHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "node:14";
    }

    public string GetExecutionCommand(string code)
    {
        var sanitizedCode = code
            .Replace("'", "'\"'\"'")
            .Replace("\n", "\\n");

        return $"mkdir -p /code && echo \"{sanitizedCode}\" > /code/temp.js && node /code/temp.js";
    }
}