using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
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
