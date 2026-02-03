using Domain.DTOs.Event.Responses;
using EventEntity = Domain.Entities.Event;

namespace Domain.Mappers;

public static class EventMapper
{
    public static DefaultEventResponseDto? ToDefaultResponseDto(this EventEntity entity)
    {
        if (entity == null) return null;

        return new DefaultEventResponseDto(
            entity.Id,
            entity.Name
        );
    }

    public static DetailedEventResponseDto? ToDetailedResponseDto(this EventEntity entity)
    {
        if (entity == null) return null;

        return new DetailedEventResponseDto(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Location,
            entity.StartDate,
            entity.OwnerUserId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}
