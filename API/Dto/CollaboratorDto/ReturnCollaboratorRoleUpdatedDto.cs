using static Domain.Entities.EventCollaborator;

namespace API.Dto.CollaboratorDto;
public class ReturnCollaboratorRoleUpdatedDto
{
    public int Id { get; set; }
    public CollaboratorRole Role { get; set; }
}
