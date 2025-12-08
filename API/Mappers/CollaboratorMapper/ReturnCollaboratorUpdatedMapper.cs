using API.Dto.CollaboratorDto;
using Domain.Entities;

namespace API.Mappers.CollaboratorMapper;
public class ReturnCollaboratorUpdatedMapper
{
    public static ReturnCollaboratorUpdatedDto ToDto(Collaborator collaborator)
    {
        return new ReturnCollaboratorUpdatedDto
        {
            Id = collaborator.Id,
            Name = collaborator.Name
        };
    }
}
