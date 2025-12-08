using API.Dto.CollaboratorDto;
using Domain.Entities;

namespace API.Mappers.CollaboratorMapper;
public class UpdateCollaboratorMapper
{
    public static Collaborator ToDomain(UpdateCollaboratorDto updateCollaboratorDto)
    {
        return Collaborator.Update(
            id: updateCollaboratorDto.Id,
            name: updateCollaboratorDto.Name
        );
    }
}
