using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
public class UpdateEventMapper
{
    public static Event ToDomain(UpdateEventDto updateEventoDto)
    {
        return new Event(
            id: updateEventoDto.Id,
            name: updateEventoDto.Name,
            description: updateEventoDto.Description,
            location: updateEventoDto.Location,
            startDate: updateEventoDto.StartDate,
            ownerUserId: updateEventoDto.OwnerUserId
        );
    }
}
