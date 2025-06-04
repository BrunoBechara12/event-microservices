
using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
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
