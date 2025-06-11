using Application.Dto.EventDto;
using Domain.Entities;

namespace Application.Mappers.EventMapper;
public class ReturnEventCreatedMapper
{
    public static ReturnEventCreatedDto ToDto(Event eventItem)
    {
        return new ReturnEventCreatedDto
        {
            Id = eventItem.Id,
            Name = eventItem.Name,
        };
    }
}
