using Application.Dto.EventDto;
using Domain.Entities;

namespace Application.Mappers.EventMapper;
public class ReturnEventMapper
{
    public static ReturnEventDto ToDto(Event eventItem)
    {
        if (eventItem == null)
            return null;

        return new ReturnEventDto()
        {
            Id = eventItem.Id,
            Name = eventItem.Name,
            Description = eventItem.Description,
            Location = eventItem.Location,
            StartDate = eventItem.StartDate,
            OwnerUserId = eventItem.OwnerUserId,
            CreatedAt = eventItem.CreatedAt,
            UpdatedAt = eventItem.UpdatedAt
        };
    }
}
