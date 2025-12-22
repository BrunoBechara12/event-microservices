using EventEntity = Domain.Entities.Event;

namespace Application.UseCases.Event.Outputs;

public static class EventExtensions
{
    public static DefaultEventOutput? ToDefaultEventOutput(this EventEntity entity)
    {
        if (entity == null) return null;

        return new DefaultEventOutput(
            entity.Id,
            entity.Name
        );
    }

    public static DetailedEventOutput? ToDetailedEventOutput(this EventEntity entity)
    {
        if (entity == null) return null;

        return new DetailedEventOutput(
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
