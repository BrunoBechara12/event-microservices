using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;

public class CreateEventMapper
{
    public static Event ToDomain(CreateEventDto createEventDto)
    {
        return new Event(
            name: createEventDto.Name,
            description: createEventDto.Description,
            location: createEventDto.Location,
            startDate: createEventDto.StartDate,
            ownerUserId: createEventDto.OwnerUserId
        );
    }
}



