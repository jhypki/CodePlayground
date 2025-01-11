using System.Text.Json;
using System.Text.Json.Serialization;
using CodePlayground.API.Services;
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

builder.Services.AddSingleton<DockerClient>(sp =>
{
    var dockerUri = new Uri("unix:///var/run/docker.sock"); // Use "npipe://./pipe/docker_engine" for Windows
    return new DockerClientConfiguration(dockerUri).CreateClient();
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