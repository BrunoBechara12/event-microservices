using API.Dto.CollaboratorDto;
using Domain.Entities;

namespace API.Mappers.CollaboratorMapper;
public class ReturnCollaboratorCreatedMapper
{
    public static ReturnCollaboratorCreatedDto ToDto(Collaborator collaboratorItem)
    {
        return new ReturnCollaboratorCreatedDto
        {
            Id = collaboratorItem.Id,
            Name = collaboratorItem.Name,
            UserId = collaboratorItem.UserId
        };
    }
}
