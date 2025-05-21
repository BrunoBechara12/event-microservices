using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
public class UpdateEventMapper
{
    public static Event ToDomain(UpdateEventDto updateEventoDto)
    {
        return new Event()
        {
            Id = updateEventoDto.Id,
            Name = updateEventoDto.Name,
            Description = updateEventoDto.Description,
            Location = updateEventoDto.Location,
            StartDate = updateEventoDto.StartDate,
            OwnerUserId = updateEventoDto.OwnerUserId
        };
    }
}
