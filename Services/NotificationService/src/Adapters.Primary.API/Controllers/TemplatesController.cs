using Domain.DTOs.Template.Requests;
using Domain.Ports.Input;
using Microsoft.AspNetCore.Mvc;

namespace Adapters.Primary.API.Controllers;

[ApiController]
[Route("api/templates")]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateUseCase _templateUseCase;

    public TemplatesController(ITemplateUseCase templateUseCase)
    {
        _templateUseCase = templateUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _templateUseCase.GetAll();

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _templateUseCase.GetById(id);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTemplateRequestDto request)
    {
        var result = await _templateUseCase.Create(request);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTemplateRequestDto request)
    {
        var result = await _templateUseCase.Update(id, request);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _templateUseCase.Delete(id);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }
}
