using Application.Dto.CollaboratorDto;
using Domain.Entities;

namespace Application.Mappers.CollaboratorMapper;
public class CreateCollaboratorMapper
{
    public static Collaborator ToCollaboratorDomain(CreateCollaboratorDto dto)
    {
        return Collaborator.Create(
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
