using Application.Dto;
using Application.Mappers;
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

    [HttpPost]
    public async Task<IActionResult> Create(CreateCollaboratorDto createCollaborator)
    {
        var domainCollaborator = CreateCollaboratorMapper.ToCollaboratorDomain(createCollaborator);

        var domainEventCollaborator = CreateCollaboratorMapper.ToEventCollaboratorDomain(createCollaborator, domainCollaborator.Id);

        var collaboratorItem = await _collaboratorUseCase.Create(domainCollaborator, domainEventCollaborator);

        if (collaboratorItem.RequestSuccess == true)
        {
            var returnCollaboratorCreated = ReturnCollaboratorCreatedMapper.ToDto(collaboratorItem.Data!);

            return Ok(new
            {
                message = collaboratorItem.Message,
                data = returnCollaboratorCreated
            });
        }

        return BadRequest(new { message = collaboratorItem.Message });
    }
}
