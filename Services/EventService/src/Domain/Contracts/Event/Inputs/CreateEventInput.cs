namespace Domain.Contracts.Event.Inputs;
public record CreateEventInput
(
    string Name,
    string Description,
    string Location,
    DateTime StartDate,
    int OwnerUserId
);
