using Application.UseCases.Event.Inputs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests.Event;
public class UpdateEventTests : BaseIntegrationTest
{
    public UpdateEventTests(IntegrationTestFactory factory)
       : base(factory)
    {
    }

    [Fact]
    public async Task Update_ShouldUpdateEvent_WhenDataIsValid()
    {
        //Arrange
        var createInput = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);
        var createResult = await EventUseCase.Create(createInput);

        var updateInput = new UpdateEventInput(createResult.Data!.Id, "Event Edited", "Event 1 description edited", "Street 2.2", DateTime.UtcNow.AddDays(10), 2);

        // Act
        var result = await EventUseCase.Update(updateInput);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Event updated with success!");
        result.Data.Should().NotBeNull();
        result.Data.Name.Should().Be(updateInput.Name);
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenNameIsTooShort()
    {
        //Arrange
        var createInput = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);
        var createResult = await EventUseCase.Create(createInput);

        var updateInput = new UpdateEventInput(createResult.Data!.Id, "Ed", "Event 1 description edited", "Street 2.2", DateTime.UtcNow.AddDays(10), 2);

        // Act
        var act = async () => await EventUseCase.Update(updateInput);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Name must be at least 3 characters long");

        var eventInDb = await DbContext.Events.FirstOrDefaultAsync(e => e.Id == createResult.Data.Id);

        eventInDb!.Name.Should().Be(createInput.Name);
        eventInDb!.Name.Should().NotBe(updateInput.Name);
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenDateIsInPast()
    {
        //Arrange
        var createInput = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);
        var createResult = await EventUseCase.Create(createInput);

        var updateInput = new UpdateEventInput(createResult.Data!.Id, "Event Edited", "Event 1 description edited", "Street 2.2", DateTime.UtcNow.AddDays(-2), 2);

        // Act
        var act = async () => await EventUseCase.Update(updateInput);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("StartDate cannot be in the past");

        var eventInDb = await DbContext.Events.FirstOrDefaultAsync(e => e.Id == createResult.Data.Id);

        eventInDb!.Name.Should().Be(createInput.Name);
        eventInDb!.Name.Should().NotBe(updateInput.Name);
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenOwnerIdIsInvalid()
    {
        //Arrange
        var createInput = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);
        var createResult = await EventUseCase.Create(createInput);

        var updateInput = new UpdateEventInput(createResult.Data!.Id, "Event Edited", "Event 1 description edited", "Street 2.2", DateTime.UtcNow.AddDays(10), 0);

        // Act
        var act = async () => await EventUseCase.Update(updateInput);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid owner");

        var eventInDb = await DbContext.Events.FirstOrDefaultAsync(e => e.Id == createResult.Data.Id);

        eventInDb!.Name.Should().Be(createInput.Name);
        eventInDb!.Name.Should().NotBe(updateInput.Name);
    }
}
