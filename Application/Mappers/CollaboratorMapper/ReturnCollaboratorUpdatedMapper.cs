using Application.Dto.CollaboratorDto;
using Domain.Entities;

namespace Application.Mappers.CollaboratorMapper;
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
