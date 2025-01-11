using CodePlayground.Core.Interfaces;

namespace CodePlayground.Core.Languages;

public class PythonHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "python:3.9";
    }

    public string GetExecutionCommand(string code)
    {
        var sanitizedCode = code
            .Replace("'", "'\"'\"'")
            .Replace("\n", "\\n");

        return $"mkdir -p /code && echo \"{sanitizedCode}\" > /code/temp.py && python /code/temp.py";
    }
}