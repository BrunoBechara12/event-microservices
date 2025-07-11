using static Domain.Entities.EventCollaborator;

namespace Application.Dto.CollaboratorDto;
public class ReturnCollaboratorRoleUpdatedDto
{
    public int Id { get; set; }
    public CollaboratorRole Role { get; set; }
}
