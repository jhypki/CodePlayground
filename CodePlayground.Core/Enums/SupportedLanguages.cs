using System.Runtime.Serialization;

namespace CodePlayground.Core.Enums;

public enum SupportedLanguages
{
    [EnumMember(Value = "javascript")] JavaScript,
    [EnumMember(Value = "csharp")] CSharp,
    [EnumMember(Value = "python")] Python,
    [EnumMember(Value = "typescript")] TypeScript,
    [EnumMember(Value = "java")] Java,
    [EnumMember(Value = "go")] Go,
    [EnumMember(Value = "ruby")] Ruby,
    [EnumMember(Value = "rust")] Rust,
    [EnumMember(Value = "kotlin")] Kotlin,
    [EnumMember(Value = "php")] PHP,
    [EnumMember(Value = "swift")] Swift,
    [EnumMember(Value = "bash")] Bash,
    [EnumMember(Value = "perl")] Perl,
    [EnumMember(Value = "elixir")] Elixir,
    [EnumMember(Value = "julia")] Julia,
    [EnumMember(Value = "haskell")] Haskell,
    [EnumMember(Value = "lua")] Lua,
    [EnumMember(Value = "groovy")] Groovy,
    [EnumMember(Value = "c")] C,
}