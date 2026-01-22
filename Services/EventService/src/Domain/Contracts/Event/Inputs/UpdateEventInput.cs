namespace Domain.Contracts.Event.Inputs;
public record UpdateEventInput
(
    int Id,
    string Name,
    string Description,
    string Location,
    DateTime StartDate,
    int OwnerUserId
);