using static Domain.Entities.EventCollaborator;

namespace Application.Dto.CollaboratorDto;
public class CreateCollaboratorDto
{
    public int UserId { get; set; }
    public int EventId { get; set; }
    public string Name { get; set; }
    public CollaboratorRole Role { get; set; }
}
