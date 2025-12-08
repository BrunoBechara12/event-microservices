using API.Dto.EventDto;
using Domain.Entities;

namespace API.Mappers.EventMapper;
public class UpdateEventMapper
{
    public static Event ToDomain(UpdateEventDto updateEventoDto)
    {
        return Event.Update(
            id: updateEventoDto.Id,
            name: updateEventoDto.Name,
            description: updateEventoDto.Description,
            location: updateEventoDto.Location,
            startDate: updateEventoDto.StartDate,
            ownerUserId: updateEventoDto.OwnerUserId
        );
    }
}
