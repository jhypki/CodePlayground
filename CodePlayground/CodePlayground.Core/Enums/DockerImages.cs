using System.Runtime.Serialization;

namespace CodePlayground.Core.Enums;

public enum DockerImages
{
    [EnumMember(Value = "code-playground/csharp")]
    CSharp,

    [EnumMember(Value = "code-playground/python")]
    Python,

    [EnumMember(Value = "code-playground/javascript")]
    JavaScript,

    [EnumMember(Value = "code-playground/go")]
    Go,

    [EnumMember(Value = "code-playground/typescript")]
    TypeScript,

    [EnumMember(Value = "code-playground/cpp")]
    Cpp
}