using static Domain.Entities.EventCollaborator;

namespace Application.Dto.CollaboratorDto;
public class UpdateCollaboratorRoleDto
{
    public int Id { get; set; }
    public CollaboratorRole Role { get; set; }
}
