namespace CodePlayground.Core.Models;

public class CodeExecutionResult
{
    public string? StdOut { get; set; }
    public string? StdErr { get; set; }
    public int ExitCode { get; set; }
}