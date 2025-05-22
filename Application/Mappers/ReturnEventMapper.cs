
using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
public class ReturnEventMapper
{
    public static ReturnEventDto ToDto(Event eventItem)
    {
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
