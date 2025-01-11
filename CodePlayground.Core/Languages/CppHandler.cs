using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;

namespace CodePlayground.Core.Languages;

public class CppHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return EnumHelper.GetEnumStringValue(DockerImages.Cpp);
    }

    public string GetExecutionCommand(string code)
    {
        return $"""
                    mkdir -p /code &&
                    echo '{code}' > /code/temp.cpp &&
                    g++ /code/temp.cpp -o /code/temp.out &&
                    /code/temp.out
                """;
    }
}