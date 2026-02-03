using Domain.DTOs.Guest.Responses;
using GuestEntity = Domain.Entities.Guest;

namespace Domain.Mappers;
public static class GuestMapper
{
    public static DefaultGuestResponseDto? ToDefaultResponseDto(this GuestEntity entity)
    {
        if (entity is null) return null;

        return new DefaultGuestResponseDto(
            entity.Id,
            entity.EventId,
            entity.Name
        );
    }

    public static DetailedGuestResponseDto? ToDetailedResponseDto(this GuestEntity entity)
    {
        if (entity is null) return null;

        return new DetailedGuestResponseDto(
            entity.Id,
            entity.EventId,
            entity.Name,
            entity.Email,
            entity.PhoneNumber,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.ResponseDate
        );
    }
}
