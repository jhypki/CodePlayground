using System.Text.Json;
using System.Text.Json.Serialization;
using CodePlayground.API.Services;
using CodePlayground.Core.Docker;
using CodePlayground.Core.Interfaces;
using Docker.DotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});
;

builder.Services.AddSingleton<IDockerManager, DockerManager>();
builder.Services.AddSingleton<DockerClient>(sp =>
{
    var dockerUri =
        Environment.GetEnvironmentVariable("DOCKER_URI") ?? "unix:///var/run/docker.sock"; // Default for Linux/macOS
    return new DockerClientConfiguration(new Uri(dockerUri)).CreateClient();
});


builder.Services.AddScoped<ICodeExecutionService, CodeExecutionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();