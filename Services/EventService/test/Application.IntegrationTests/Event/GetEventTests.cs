using Application.IntegrationTests;
using Application.UseCases.Event.Inputs;
using FluentAssertions;

namespace Tests.Event;
public class GetEventTests : BaseIntegrationTest
{
    public GetEventTests(IntegrationTestFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Get_ShouldReturnEventList_WhenEventsExist()
    {
        //Arrange
        var input1 = new CreateEventInput("Event1", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);
        var input2 = new CreateEventInput("Event2", "Event 2 description", "Street 3", DateTime.UtcNow.AddDays(2), 2);

        // Act
        await EventUseCase.Create(input1);
        await EventUseCase.Create(input2);

        var result = await EventUseCase.Get();

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Events found with success!");
        result.Data.Should().HaveCount(2);
    }

    [Fact]
    public async Task Get_ShouldReturnEmptyList_WhenNoEventsExist()
    {
        // Arrange/Act
        var result = await EventUseCase.Get();

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("No events found.");
        result.Data.Should().HaveCount(0);
        result.Data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetById_ShouldReturnEvent_WhenEventExists()
    {
        // Arrange
        var input = new CreateEventInput("Event 1", "Description", "Street 1", DateTime.UtcNow.AddDays(5), 10);
        var createResult = await EventUseCase.Create(input);
        var createdId = createResult.Data!.Id;

        // Act
        var result = await EventUseCase.GetById(createdId);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Event found with success!");

        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be(createdId);
        result.Data.Name.Should().Be(input.Name);
    }

    [Fact]
    public async Task GetById_ShouldReturnNullData_WhenEventDoesNotExist()
    {
        // Arrange
        var nonExistentId = 9999;

        // Act
        var result = await EventUseCase.GetById(nonExistentId);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Event not found.");
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task GetByUserId_ShouldReturnListOfEvents_WhenUserHasEvents()
    {
        // Arrange
        var userId = 50;

        var input1 = new CreateEventInput("Event A", "Desc A", "Street A", DateTime.UtcNow.AddDays(1), userId);
        var input2 = new CreateEventInput("Event B", "Desc B", "Street B", DateTime.UtcNow.AddDays(2), userId);

        var inputOtherUser = new CreateEventInput("Event C", "Desc C", "Street C", DateTime.UtcNow.AddDays(3), 99);

        await EventUseCase.Create(input1);
        await EventUseCase.Create(input2);
        await EventUseCase.Create(inputOtherUser);

        // Act
        var result = await EventUseCase.GetByUserId(userId);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Events found with success!");

        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(2); 

        result.Data.Select(x => x.Name).Should().Contain([input1.Name, input2.Name]);
        result.Data.Select(x => x.Name).Should().NotContain(inputOtherUser.Name);
    }

    [Fact]
    public async Task GetByUserId_ShouldReturnEmptyList_WhenUserHasNoEvents()
    {
        // Arrange
        var userIdWithNoEvents = 100;

        // Act
        var result = await EventUseCase.GetByUserId(userIdWithNoEvents);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("No events found for this user.");

        result.Data.Should().NotBeNull(); 
        result.Data.Should().BeEmpty();
    }
}
