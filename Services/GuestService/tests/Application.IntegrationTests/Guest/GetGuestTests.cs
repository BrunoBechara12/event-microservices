using Application.IntegrationTests;
using Domain.Contracts.Guest.Inputs;
using FluentAssertions;

namespace Tests.Guest;
public class GetGuestTests : BaseIntegrationTest
{
    public GetGuestTests(IntegrationTestFactory factory)
        : base(factory)
    {
    }

    private async Task<Domain.Entities.Event> CreateEventAsync(int id, string name)
    {
        var eventEntity = Domain.Entities.Event.CreateEvent(id, name);
        return await EventRepository.Create(eventEntity);
    }

    [Fact]
    public async Task Get_ShouldReturnGuestList_WhenGuestsExist()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(1, "Party");
        
        var input1 = new CreateGuestInput(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var input2 = new CreateGuestInput(eventEntity.Id, "Jane Doe", "jane.doe@email.com", "11888888888");

        // Act
        await GuestUseCase.Create(input1);
        await GuestUseCase.Create(input2);

        var result = await GuestUseCase.Get();

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Guests found with success!");
        result.Data.Should().HaveCount(2);
    }

    [Fact]
    public async Task Get_ShouldReturnNullData_WhenNoGuestsExist()
    {
        // Arrange/Act
        var result = await GuestUseCase.Get();

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("No guests found.");
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task GetById_ShouldReturnGuest_WhenGuestExists()
    {
        // Arrange
        var eventEntity = await CreateEventAsync(2, "Conference");
        
        var input = new CreateGuestInput(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(input);
        var createdId = createResult.Data!.Id;

        // Act
        var result = await GuestUseCase.GetById(createdId);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Guest found with success!");

        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be(createdId);
        result.Data.EventId.Should().Be(eventEntity.Id);
        result.Data.Name.Should().Be(input.Name);
        result.Data.Email.Should().Be(input.Email);
        result.Data.PhoneNumber.Should().Be(input.PhoneNumber);
    }

    [Fact]
    public async Task GetById_ShouldReturnFailure_WhenGuestDoesNotExist()
    {
        // Arrange
        var nonExistentId = 9999;

        // Act
        var result = await GuestUseCase.GetById(nonExistentId);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Guest not found.");
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task Get_ShouldReturnGuestsWithCorrectDetails()
    {
        // Arrange
        var eventEntity = await CreateEventAsync(3, "Wedding");
        
        var input = new CreateGuestInput(eventEntity.Id, "Maria Silva", "maria.silva@email.com", "11777777777");
        await GuestUseCase.Create(input);

        // Act
        var result = await GuestUseCase.Get();

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        
        var guest = result.Data!.First();
        guest.Name.Should().Be(input.Name);
        guest.Email.Should().Be(input.Email);
        guest.PhoneNumber.Should().Be(input.PhoneNumber);
        guest.EventId.Should().Be(eventEntity.Id);
    }
}
