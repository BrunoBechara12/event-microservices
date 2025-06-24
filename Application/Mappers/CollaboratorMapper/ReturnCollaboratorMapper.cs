using Application.Dto.CollaboratorDto;
using Domain.Entities;

namespace Application.Mappers.CollaboratorMapper;
public class ReturnCollaboratorMapper
{
    public static ReturnCollaboratorDto ToDto(Collaborator collaboratorItem)
    {
        return new ReturnCollaboratorDto
        {
            UserId = collaboratorItem.UserId,
            Name = collaboratorItem.Name
        };
    }
}
