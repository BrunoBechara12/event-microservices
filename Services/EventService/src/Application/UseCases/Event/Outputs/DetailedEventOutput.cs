namespace Application.UseCases.Event.Outputs;
public record DetailedEventOutput
(
    int Id,
    string Name,
    string Description,
    string Location,
    DateTime StartDate,
    int OwnerUserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

    