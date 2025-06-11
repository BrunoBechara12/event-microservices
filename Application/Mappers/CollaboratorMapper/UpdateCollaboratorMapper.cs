using Application.Dto.CollaboratorDto;
using Domain.Entities;

namespace Application.Mappers.CollaboratorMapper;
public class UpdateCollaboratorMapper
{
    public static EventCollaborator ToDomain(UpdateCollaboratorDto updateCollaboratorDto)
    {
        return new EventCollaborator(
            id: updateCollaboratorDto.Id,
            role: updateCollaboratorDto.Role
        );
    }
}
