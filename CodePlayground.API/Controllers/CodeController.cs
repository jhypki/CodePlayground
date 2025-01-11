using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodePlayground.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CodeController : ControllerBase
{
    private readonly ICodeExecutionService _codeExecutionService;

    public CodeController(ICodeExecutionService codeExecutionService)
    {
        _codeExecutionService = codeExecutionService;
    }

    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteCode([FromBody] CodeExecutionRequest request)
    {
        try
        {
            var result = await _codeExecutionService.ExecuteCodeAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}