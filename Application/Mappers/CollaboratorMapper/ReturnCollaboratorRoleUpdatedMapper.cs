using Application.Dto.CollaboratorDto;
using Domain.Entities;

namespace Application.Mappers.CollaboratorMapper;
public class ReturnCollaboratorRoleUpdatedMapper
{
    public static ReturnCollaboratorRoleUpdatedDto ToDto(EventCollaborator collaborator)
    {
        return new ReturnCollaboratorRoleUpdatedDto
        {
            Id = collaborator.Id,
            Role = collaborator.Role
        };
    }
}
