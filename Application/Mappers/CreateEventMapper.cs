using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;

public class CreateEventMapper
{
    public static Event ToDomain(CreateEventDto createEventoDto)
    {
        return new Event()
        {
            Name = createEventoDto.Name,
            Description = createEventoDto.Description,
            Location = createEventoDto.Location,
            StartDate = createEventoDto.StartDate,
            OwnerUserId = createEventoDto.OwnerUserId,
            CreatedAt = DateTime.UtcNow
        };
    }
}



