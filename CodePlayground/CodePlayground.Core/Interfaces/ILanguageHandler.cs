namespace CodePlayground.Core.Interfaces;

public interface ILanguageHandler
{
    string GetDockerImage();
    string GetExecutionCommand(string code);
}