using Application.Dto.CollaboratorDto;
using Domain.Entities;

namespace Application.Mappers.CollaboratorMapper;
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
