using API.Dto.CollaboratorDto;
using Domain.Entities;

namespace API.Mappers.CollaboratorMapper;
public class UpdateCollaboratorRoleMapper
{
    public static EventCollaborator ToDomain(UpdateCollaboratorRoleDto updateCollaboratorRoleDto)
    {
        return new EventCollaborator(
            id: updateCollaboratorRoleDto.Id,
            role: updateCollaboratorRoleDto.Role
        );
    }
}
