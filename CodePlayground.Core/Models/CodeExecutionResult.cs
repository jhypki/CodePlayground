namespace CodePlayground.Core.Models;

public class CodeExecutionResult
{
    public string? StdOut { get; set; }
    public string? StdErr { get; set; }
    public long ExitCode { get; set; }
}