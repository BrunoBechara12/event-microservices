using GuestEntity = Domain.Entities.Guest;

namespace Application.UseCases.Outputs;
public static class GuestExtensions
{
    public static DefaultGuestOutput? ToDefaultGuestOutput(this GuestEntity entity)
    {
        if (entity is null) return null;

        return new DefaultGuestOutput(
            entity.Id,
            entity.Name
        );
    }

    public static DetailedGuestOutput? ToDetailedGuestOutput(this GuestEntity entity)
    {
        if (entity is null) return null;

        return new DetailedGuestOutput(
            entity.Id,
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
