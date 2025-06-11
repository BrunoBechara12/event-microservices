using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
public class ReturnCollaboratorUpdatedMapper
{
    public static ReturnCollaboratorUpdatedDto ToDto(EventCollaborator collaborator)
    {
        return new ReturnCollaboratorUpdatedDto
        {
            Id = collaborator.Id,
            Role = collaborator.Role
        };
    }
}
