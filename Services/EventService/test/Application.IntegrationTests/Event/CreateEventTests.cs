using Application.IntegrationTests;
using Application.UseCases.Event.Inputs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Event;
public class CreateEventTests : BaseIntegrationTest
{
    public CreateEventTests(IntegrationTestFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldAddNewEventToDatabase_WhenDataIsValid()
    {
        //Arrange
        var input = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);

        // Act
        var result = await EventUseCase.Create(input);

        var getResult = await EventUseCase.GetById(result.Data!.Id);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Event created with success!");
        result.Data.Should().NotBeNull();
        result.Data.Name.Should().Be(input.Name);

        var eventInDb = await DbContext.Events.FirstOrDefaultAsync(x => x.Name == input.Name);

        eventInDb.Should().NotBeNull();
        eventInDb.Name.Should().Be(input.Name);
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenNameIsTooShort()
    {
        //Arrange
        var input = new CreateEventInput("Ev", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);

        // Act
        var act = async () => await EventUseCase.Create(input);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Name must be at least 3 characters long");

        var exists = await DbContext.Events.AnyAsync(e => e.Name == input.Name);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenDateIsInPast()
    {
        //Arrange
        var input = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(-2), 2);

        // Act
        var act = async () => await EventUseCase.Create(input);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("StartDate cannot be in the past");

        var exists = await DbContext.Events.AnyAsync(e => e.Name == input.Name);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenOwnerIdIsInvalid()
    {
        //Arrange
        var input = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 0);

        // Act
        var act = async () => await EventUseCase.Create(input);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid owner");

        var exists = await DbContext.Events.AnyAsync(e => e.Name == input.Name);
        exists.Should().BeFalse();
    }
}
