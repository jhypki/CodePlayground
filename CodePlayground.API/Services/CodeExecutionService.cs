using CodePlayground.Core.Factories;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Models;

namespace CodePlayground.API.Services;

public class CodeExecutionService(IDockerManager dockerManager) : ICodeExecutionService
{
    public async Task<CodeExecutionResult> ExecuteCodeAsync(CodeExecutionRequest request)
    {
        var handler = LanguageHandlerFactory.GetHandler(request.Language);

        var image = handler.GetDockerImage();
        if (image == null) throw new ArgumentException("Unsupported language");

        var command = handler.GetExecutionCommand(request.Code ?? string.Empty);

        var containerConfig = await dockerManager.CreateContainerParameters(image, command);

        var (stdout, stderr, exitCode) = await dockerManager.RunContainerAsync(containerConfig);

        return new CodeExecutionResult
        {
            StdOut = stdout,
            StdErr = stderr,
            ExitCode = exitCode
        };
    }
}