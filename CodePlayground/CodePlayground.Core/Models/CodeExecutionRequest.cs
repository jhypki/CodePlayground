using System.ComponentModel.DataAnnotations;

namespace CodePlayground.Core.Models;

public class CodeExecutionRequest
{
    [Required] public string? Language { get; set; }
    [Required] public string? Code { get; set; }
}