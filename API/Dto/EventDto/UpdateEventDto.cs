namespace API.Dto.EventDto;
public class UpdateEventDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public int OwnerUserId { get; set; }
}
