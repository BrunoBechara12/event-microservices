using Domain.DTOs.Notification.Responses;
using NotificationEntity = Domain.Entities.Notification;

namespace Domain.Mappers;

public static class NotificationMapper
{
    public static NotificationResponseDto ToResponseDto(this NotificationEntity entity)
    {
        return new NotificationResponseDto(
            entity.Id,
            entity.PhoneNumber,
            entity.Message,
            entity.Status.ToString(),
            entity.Type.ToString(),
            entity.CreatedAt,
            entity.SentAt,
            entity.ErrorMessage
        );
    }
}
