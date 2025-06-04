using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
public class CreateCollaboratorMapper
{
    public static Collaborator ToCollaboratorDomain(CreateCollaboratorDto dto)
    {
        return new Collaborator(
            userId: dto.UserId, 
            name: dto.Name
        );
    }

    public static EventCollaborator ToEventCollaboratorDomain(CreateCollaboratorDto dto, int collaboratorId)
    {
        return new EventCollaborator(
            eventId: dto.EventId, 
            collaboratorId: collaboratorId, 
            role: dto.Role
        );
    }
}
