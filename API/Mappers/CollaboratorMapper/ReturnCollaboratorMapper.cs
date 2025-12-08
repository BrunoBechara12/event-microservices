using API.Dto.CollaboratorDto;
using Domain.Entities;

namespace API.Mappers.CollaboratorMapper;
public class ReturnCollaboratorMapper
{
    public static ReturnCollaboratorDto ToDto(EventCollaborator collaboratorItem)
    {
        return new ReturnCollaboratorDto
        {
            Id = collaboratorItem.Collaborator.Id,
            Name = collaboratorItem.Collaborator.Name,
            UserId = collaboratorItem.Collaborator.UserId,
            EventId = collaboratorItem.EventId,
            Role = collaboratorItem.Role.ToString(),
            CreatedAt = collaboratorItem.Collaborator.CreatedAt,
            AddedAt = collaboratorItem.CreatedAt
        };
    }
}
