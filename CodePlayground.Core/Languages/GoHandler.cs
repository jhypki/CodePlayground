using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;

namespace CodePlayground.Core.Languages;

public class GoHandler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return EnumHelper.GetEnumStringValue(DockerImages.Go);
    }

    public string GetExecutionCommand(string code)
    {
        return $"""
                mkdir -p /code &&
                cat << 'EOF' > /code/temp.go
                {code}
                EOF
                cd /code &&
                go run temp.go
                """;
    }
}