using CodePlayground.Core.Enums;

namespace CodePlayground.Core.Models;

public class CodeExecutionRequest
{
    public SupportedLanguages Language { get; set; }
    public string? Code { get; set; }
}