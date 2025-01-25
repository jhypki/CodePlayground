using Docker.DotNet.Models;

namespace CodePlayground.Core.Interfaces;

public interface IDockerManager
{
    Task<CreateContainerParameters> CreateContainerParameters(string imageName, string command);
    Task<(string StdOut, string StdErr, long ExitCode)> RunContainerAsync(CreateContainerParameters parameters);
}