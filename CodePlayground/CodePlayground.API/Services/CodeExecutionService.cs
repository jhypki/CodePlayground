using CodePlayground.Core.Enums;
using CodePlayground.Core.Factories;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Models;

namespace CodePlayground.API.Services;

public class CodeExecutionService(IDockerManager dockerManager, ILanguageHandlerFactory languageHandlerFactory) : ICodeExecutionService
{
    public async Task<CodeExecutionResult> ExecuteCodeAsync(CodeExecutionRequest request)
    {
        if (!Enum.TryParse<SupportedLanguages>(request.Language, true, out var parsedLanguage) ||
            !Enum.IsDefined(typeof(SupportedLanguages), parsedLanguage))
            throw new ArgumentException($"Unsupported language: {request.Language}");

        var handler = languageHandlerFactory.GetHandler(parsedLanguage);

        var image = handler.GetDockerImage();
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
