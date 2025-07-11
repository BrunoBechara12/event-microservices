using Application.Dto.CollaboratorDto;
using Domain.Entities;

namespace Application.Mappers.CollaboratorMapper;
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
