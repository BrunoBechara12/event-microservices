using static Domain.Entities.EventCollaborator;

namespace API.Dto.CollaboratorDto;
public class UpdateCollaboratorRoleDto
{
    public int Id { get; set; }
    public CollaboratorRole Role { get; set; }
}
