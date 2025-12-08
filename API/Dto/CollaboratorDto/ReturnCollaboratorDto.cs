namespace API.Dto.CollaboratorDto;
public class ReturnCollaboratorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime AddedAt { get; set; }
}
