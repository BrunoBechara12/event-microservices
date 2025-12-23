using Application.IntegrationTests;
using Application.UseCases.Collaborator.Inputs;
using Application.UseCases.Event.Inputs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using static Domain.Entities.EventCollaborator;

namespace Tests.Collaborator;
public class EventCollaboratorTests : BaseIntegrationTest
{
    public EventCollaboratorTests(IntegrationTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddToEvent_ShouldLinkCollaborator_WhenDataIsValid()
    {
        // Arrange
        var eventResult = await EventUseCase.Create(new CreateEventInput("Event", "Desc", "St", DateTime.UtcNow.AddDays(5), 1));
        var collabResult = await CollaboratorUseCase.Create(new CreateCollaboratorInput(1, "John"));

        var input = new AddCollaboratorToEventInput(eventResult.Data!.Id, collabResult.Data!.Id, 0);

        // Act
        var result = await CollaboratorUseCase.AddToEvent(input);

        // Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Collaborator added to event with success!");
 
        var linkInDb = await DbContext.EventCollaborators
            .FirstOrDefaultAsync(ec => ec.EventId == input.EventId && ec.CollaboratorId == input.CollaboratorId);

        linkInDb.Should().NotBeNull();
        linkInDb.Role.Should().Be(0);
    }

    [Fact]
    public async Task AddToEvent_ShouldReturnFailure_WhenAlreadyLinked()
    {
        // Arrange
        var eventResult = await EventUseCase.Create(new CreateEventInput("Event", "Desc", "St", DateTime.UtcNow.AddDays(5), 1));
        var collabResult = await CollaboratorUseCase.Create(new CreateCollaboratorInput(1, "John"));
        var input = new AddCollaboratorToEventInput(collabResult.Data!.Id, eventResult.Data!.Id, CollaboratorRole.Inviter);

        // Act 
        await CollaboratorUseCase.AddToEvent(input);

        var result = await CollaboratorUseCase.AddToEvent(input);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Collaborator is already in this event.");
    }

    [Fact]
    public async Task AddToEvent_ShouldReturnFailure_WhenEventDoesNotExist()
    {
        // Arrange
        var collabResult = await CollaboratorUseCase.Create(new CreateCollaboratorInput(1, "John"));
        var fakeEventId = 9999;

        var input = new AddCollaboratorToEventInput(collabResult.Data!.Id, fakeEventId, CollaboratorRole.Inviter);

        // Act
        var result = await CollaboratorUseCase.AddToEvent(input);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Event not found.");
    }

    [Fact]
    public async Task AddToEvent_ShouldReturnFailure_WhenCollaboratorDoesNotExist()
    {
        // Arrange
        var eventResult = await EventUseCase.Create(new CreateEventInput("Event", "Desc", "St", DateTime.UtcNow.AddDays(5), 1));
        var fakeCollabId = 9999;

        var input = new AddCollaboratorToEventInput(eventResult.Data!.Id, fakeCollabId, CollaboratorRole.Adm);

        // Act
        var result = await CollaboratorUseCase.AddToEvent(input);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Collaborator not found.");
    }

    [Fact]
    public async Task RemoveFromEvent_ShouldUnlinkCollaborator_WhenLinked()
    {
        // Arrange
        var eventResult = await EventUseCase.Create(new CreateEventInput("Event", "Desc", "St", DateTime.UtcNow.AddDays(5), 4));
        var collabResult = await CollaboratorUseCase.Create(new CreateCollaboratorInput(1, "John"));

        await CollaboratorUseCase.AddToEvent(new AddCollaboratorToEventInput(collabResult.Data!.Id, eventResult.Data!.Id, CollaboratorRole.Inviter));

        var removeInput = new RemoveCollaboratorFromEventInput(collabResult.Data.Id, eventResult.Data.Id);

        // Act
        var result = await CollaboratorUseCase.RemoveFromEvent(removeInput);

        // Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Collaborator removed from event with success!");

        var linkInDb = await DbContext.EventCollaborators
            .FirstOrDefaultAsync(ec => ec.EventId == removeInput.EventId && ec.CollaboratorId == removeInput.CollaboratorId);

        linkInDb.Should().BeNull();
    }

    [Fact]
    public async Task RemoveFromEvent_ShouldReturnFailure_WhenNotLinked()
    {
        // Arrange
        var eventResult = await EventUseCase.Create(new CreateEventInput("Event", "Desc", "St", DateTime.UtcNow.AddDays(5), 1));
        var collabResult = await CollaboratorUseCase.Create(new CreateCollaboratorInput(1, "John"));

        var removeInput = new RemoveCollaboratorFromEventInput(collabResult.Data!.Id, eventResult.Data!.Id);

        // Act 
        var result = await CollaboratorUseCase.RemoveFromEvent(removeInput);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Collaborator is not linked to this event.");
    }
}
