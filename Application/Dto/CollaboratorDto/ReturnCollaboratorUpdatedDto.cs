using static Domain.Entities.EventCollaborator;

namespace Application.Dto.CollaboratorDto;
public class ReturnCollaboratorUpdatedDto
{
    public int Id { get; set; }
    public CollaboratorRole Role { get; set; }
}
