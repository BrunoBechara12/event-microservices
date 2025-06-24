using Application.Dto.CollaboratorDto;
using Application.Mappers.CollaboratorMapper;
using Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/collaborators")]
[ApiController]
public class CollaboratorController : ControllerBase
{
    public readonly ICollaboratorUseCase _collaboratorUseCase;

    public CollaboratorController(ICollaboratorUseCase collaboratorUseCase)
    {
        _collaboratorUseCase = collaboratorUseCase;
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetByEvent(int eventId)
    {
        var domainCollaborator = await _collaboratorUseCase.GetByEventId(eventId);

        if (domainCollaborator.RequestSuccess == true)
        {
            var dtoCollaborator = domainCollaborator.Data!.Select(ReturnCollaboratorMapper.ToDto).ToList();

            return Ok(new
            {
                message = domainCollaborator.Message,
                data = dtoCollaborator
            });
        }

        return BadRequest(new { message = domainCollaborator.Message });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCollaboratorDto createCollaborator)
    {
        var domainCollaborator = CreateCollaboratorMapper.ToCollaboratorDomain(createCollaborator);

        var domainEventCollaborator = CreateCollaboratorMapper.ToEventCollaboratorDomain(createCollaborator, domainCollaborator.Id);

        var collaboratorItem = await _collaboratorUseCase.Create(domainCollaborator, domainEventCollaborator);

        if (collaboratorItem.RequestSuccess == true)
        {
            var dtoCollaborator = ReturnCollaboratorCreatedMapper.ToDto(collaboratorItem.Data!);

            return Ok(new
            {
                message = collaboratorItem.Message,
                data = dtoCollaborator
            });
        }

        return BadRequest(new { message = collaboratorItem.Message });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole(UpdateCollaboratorDto collaborator)
    {
        var domainCollaborator = UpdateCollaboratorMapper.ToDomain(collaborator);

        var collaboratorItem = await _collaboratorUseCase.UpdateRole(domainCollaborator);

        if (collaboratorItem.RequestSuccess == true)
        {
            var dtoCollaborator = ReturnCollaboratorUpdatedMapper.ToDto(collaboratorItem.Data!);

            return Ok(new
            {
                message = collaboratorItem.Message,
                data = dtoCollaborator
            });
        }

        return BadRequest(new { message = collaboratorItem.Message });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int collaboratorId, int eventId)
    {
        var deletedCollaborator = await _collaboratorUseCase.Remove(collaboratorId, eventId);

        if (deletedCollaborator.RequestSuccess == true)
        {
            return Ok(new
            {
                message = deletedCollaborator.Message,
                data = deletedCollaborator.Data
            });
        }

        return BadRequest(new { message = deletedCollaborator.Message });
    }
}
