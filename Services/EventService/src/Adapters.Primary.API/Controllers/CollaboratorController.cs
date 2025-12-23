using Application.UseCases.Collaborator.Inputs;
using Application.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace Adapters.Primary.API.Controllers;

[Route("api/collaborators")]
[ApiController]
public class CollaboratorController : ControllerBase
{
    private readonly ICollaboratorUseCase _collaboratorUseCase;

    public CollaboratorController(ICollaboratorUseCase collaboratorUseCase)
    {
        _collaboratorUseCase = collaboratorUseCase;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var collaborator = await _collaboratorUseCase.GetById(id);

        if (collaborator.RequestSuccess)
        {
            return Ok(new
            {
                message = collaborator.Message,
                data = collaborator.Data
            });
        }

        return BadRequest(new { message = collaborator.Message });
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetByEvent(int eventId)
    {
        var collaborator = await _collaboratorUseCase.GetByEventId(eventId);

        if (collaborator.RequestSuccess)
        {
            return Ok(new
            {
                message = collaborator.Message,
                data = collaborator.Data
            });
        }

        return BadRequest(new { message = collaborator.Message });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCollaboratorInput input)
    {
        var collaborator = await _collaboratorUseCase.Create(input);

        if (collaborator.RequestSuccess)
        {
            return Ok(new
            {
                message = collaborator.Message,
                data = collaborator.Data
            });
        }

        return BadRequest(new { message = collaborator.Message });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCollaboratorInput input)
    {
        var collaborator = await _collaboratorUseCase.Update(input);

        if (collaborator.RequestSuccess)
        {
            return Ok(new
            {
                message = collaborator.Message,
                data = collaborator.Data
            });
        }

        return BadRequest(new { message = collaborator.Message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var collaborator = await _collaboratorUseCase.Delete(id);

        if (collaborator.RequestSuccess)
        {
            return Ok(new
            {
                message = collaborator.Message,
                data = collaborator.Data
            });
        }

        return BadRequest(new { message = collaborator.Message });
    }

    [HttpPost("AddToEvent")]
    public async Task<IActionResult> AddToEvent(AddCollaboratorToEventInput input)
    {
        var collaborator = await _collaboratorUseCase.AddToEvent(input);

        if (collaborator.RequestSuccess)
        {
            return Ok(new
            {
                message = collaborator.Message,
                data = collaborator.Data
            });
        }

        return BadRequest(new { message = collaborator.Message });
    }

    [HttpDelete("RemoveFromEvent")]
    public async Task<IActionResult> RemoveFromEvent(RemoveCollaboratorFromEventInput input)
    {
        var collaborator = await _collaboratorUseCase.RemoveFromEvent(input);

        if (collaborator.RequestSuccess)
        {
            return Ok(new
            {
                message = collaborator.Message,
                data = collaborator.Data
            });
        }

        return BadRequest(new { message = collaborator.Message });
    }
}