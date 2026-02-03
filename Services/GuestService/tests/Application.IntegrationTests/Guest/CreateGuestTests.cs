using Application.IntegrationTests;
using Domain.DTOs.Guest.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Guest;
public class CreateGuestTests : BaseIntegrationTest
{
    public CreateGuestTests(IntegrationTestFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldAddNewGuestToDatabase_WhenEventExistsAndDataIsValid()
    {
        //Arrange
        var eventEntity = Domain.Entities.Event.CreateEvent(1, "Birthday Party");
        await EventRepository.Create(eventEntity);

        var input = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");

        // Act
        var result = await GuestUseCase.Create(input);

        var getResult = await GuestUseCase.GetById(result.Data!.Id);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Guest created with success!");
        result.Data.Should().NotBeNull();
        result.Data.Name.Should().Be(input.Name);
        result.Data.EventId.Should().Be(eventEntity.Id);

        var guestInDb = await DbContext.Guests.FirstOrDefaultAsync(x => x.Name == input.Name);

        guestInDb.Should().NotBeNull();
        guestInDb!.Name.Should().Be(input.Name);
        guestInDb.Email.Should().Be(input.Email);
        guestInDb.PhoneNumber.Should().Be(input.PhoneNumber);
        guestInDb.EventId.Should().Be(eventEntity.Id);
    }

    [Fact]
    public async Task Create_ShouldFail_WhenEventDoesNotExist()
    {
        //Arrange
        var nonExistentEventId = 999;
        var input = new CreateGuestRequestDto(nonExistentEventId, "John Doe", "john.doe@email.com", "11999999999");

        // Act
        var result = await GuestUseCase.Create(input);

        //Assert 
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Event not found");
        result.Data.Should().BeNull();

        var exists = await DbContext.Guests.AnyAsync(g => g.Name == input.Name);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenNameIsTooShort()
    {
        //Arrange
        var eventEntity = Domain.Entities.Event.CreateEvent(2, "Conference");
        await EventRepository.Create(eventEntity);

        var input = new CreateGuestRequestDto(eventEntity.Id, "Jo", "john.doe@email.com", "11999999999");

        // Act
        var act = async () => await GuestUseCase.Create(input);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Name must be at least 3 characters long.");

        var exists = await DbContext.Guests.AnyAsync(g => g.Name == input.Name);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenEmailIsInvalid()
    {
        //Arrange
        var eventEntity = Domain.Entities.Event.CreateEvent(3, "Wedding");
        await EventRepository.Create(eventEntity);

        var input = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "invalidemail", "11999999999");

        // Act
        var act = async () => await GuestUseCase.Create(input);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid email format.");

        var exists = await DbContext.Guests.AnyAsync(g => g.Email == input.Email);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenPhoneNumberIsEmpty()
    {
        //Arrange
        var eventEntity = Domain.Entities.Event.CreateEvent(4, "Meeting");
        await EventRepository.Create(eventEntity);

        var input = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "");

        // Act
        var act = async () => await GuestUseCase.Create(input);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Phone number is required.");

        var exists = await DbContext.Guests.AnyAsync(g => g.Name == input.Name);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Create_ShouldSetStatusAsPending_WhenGuestIsCreated()
    {
        //Arrange
        var eventEntity = Domain.Entities.Event.CreateEvent(5, "Party");
        await EventRepository.Create(eventEntity);

        var input = new CreateGuestRequestDto(eventEntity.Id, "Jane Doe", "jane.doe@email.com", "11888888888");

        // Act
        var result = await GuestUseCase.Create(input);

        //Assert 
        result.RequestSuccess.Should().BeTrue();

        var guestInDb = await DbContext.Guests.FirstOrDefaultAsync(x => x.Id == result.Data!.Id);

        guestInDb.Should().NotBeNull();
        guestInDb!.Status.Should().Be(Domain.Entities.Guest.InviteStatus.Pending);
        guestInDb.EventId.Should().Be(eventEntity.Id);
    }

    [Fact]
    public async Task Create_ShouldFail_WhenEventWasDeletedViaRabbitMQ()
    {
        //Arrange
        var eventEntity = Domain.Entities.Event.CreateEvent(6, "Deleted Event");
        await EventRepository.Create(eventEntity);

        // Simulate event deletion
        await EventRepository.Delete(eventEntity);

        var input = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");

        // Act
        var result = await GuestUseCase.Create(input);

        //Assert 
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Event not found");
        result.Data.Should().BeNull();

        var exists = await DbContext.Guests.AnyAsync(g => g.Name == input.Name);
        exists.Should().BeFalse();
    }
}
