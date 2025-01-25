using CodePlayground.Core.Models;

namespace CodePlayground.Core.Interfaces;

public interface ICodeExecutionService
{
    Task<CodeExecutionResult> ExecuteCodeAsync(CodeExecutionRequest request);
}