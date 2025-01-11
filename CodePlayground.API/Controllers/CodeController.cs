using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodePlayground.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CodeController(ICodeExecutionService codeExecutionService) : ControllerBase
{
    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteCode([FromBody] CodeExecutionRequest request)
    {
        var result = await codeExecutionService.ExecuteCodeAsync(request);
        return Ok(result);
    }
}