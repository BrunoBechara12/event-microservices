using Application.IntegrationTests;
using Application.UseCases.Collaborator.Inputs;
using Application.UseCases.Event.Inputs;
using FluentAssertions;

namespace Tests.Collaborator;
public class GetCollaboratorTests : BaseIntegrationTest
{
    public GetCollaboratorTests(IntegrationTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetById_ShouldReturnCollaborator_WhenExists()
    {
        // Arrange
        var createResult = await CollaboratorUseCase.Create(new CreateCollaboratorInput(1, "Jane Doe"));
        var id = createResult.Data!.Id;

        // Act
        var result = await CollaboratorUseCase.GetById(id);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Collaborator found with success!");
        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be(id);
    }

    [Fact]
    public async Task GetById_ShouldReturnNullData_WhenDoesNotExist()
    {
        // Act
        var result = await CollaboratorUseCase.GetById(999);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Collaborator not found.");
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task GetByEventId_ShouldReturnCollaborators_WhenLinkedToEvent()
    {
        // Arrange
        var eventInput = new CreateEventInput("Tech Conf", "Desc", "Street", DateTime.UtcNow.AddDays(10), 1);
        var eventResult = await EventUseCase.Create(eventInput);
        var eventId = eventResult.Data!.Id;

        var collab1Result = await CollaboratorUseCase.Create(new CreateCollaboratorInput(1, "Collab A"));
        var collab2Result = await CollaboratorUseCase.Create(new CreateCollaboratorInput(2, "Collab B"));
        var collabOtherResult = await CollaboratorUseCase.Create(new CreateCollaboratorInput(3, "Collab C (Not Linked)"));

        await CollaboratorUseCase.AddToEvent(new AddCollaboratorToEventInput(collab1Result.Data!.Id, eventId, 0));
        await CollaboratorUseCase.AddToEvent(new AddCollaboratorToEventInput(collab2Result.Data!.Id, eventId, 0));

        // Act
        var result = await CollaboratorUseCase.GetByEventId(eventId);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(2);

        result.Data.Select(c => c.Name).Should().Contain(["Collab A", "Collab B"]);
        result.Data.Select(c => c.Name).Should().NotContain("Collab C (Not Linked)");
    }

    [Fact]
    public async Task GetByEventId_ShouldReturnFailure_WhenEventDoesNotExist()
    {
        // Act
        var result = await CollaboratorUseCase.GetByEventId(9999);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Event not found.");
    }
}
