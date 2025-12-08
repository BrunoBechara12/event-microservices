using API.Dto.CollaboratorDto;
using Domain.Entities;

namespace API.Mappers.CollaboratorMapper;
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
