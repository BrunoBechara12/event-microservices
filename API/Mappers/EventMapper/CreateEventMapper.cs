using API.Dto.EventDto;
using Domain.Entities;

namespace API.Mappers.EventMapper;

public class CreateEventMapper
{
    public static Event ToDomain(CreateEventDto createEventDto)
    {
        return Event.Create(
            name: createEventDto.Name,
            description: createEventDto.Description,
            location: createEventDto.Location,
            startDate: createEventDto.StartDate,
            ownerUserId: createEventDto.OwnerUserId
        );
    }
}



