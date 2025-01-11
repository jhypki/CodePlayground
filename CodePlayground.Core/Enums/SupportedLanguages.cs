using System.Runtime.Serialization;

namespace CodePlayground.Core.Enums;

public enum SupportedLanguages
{
    [EnumMember(Value = "csharp")] CSharp,
    [EnumMember(Value = "python")] Python,
    [EnumMember(Value = "javascript")] JavaScript
}