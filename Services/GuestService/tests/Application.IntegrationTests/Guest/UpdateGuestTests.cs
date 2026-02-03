using Application.IntegrationTests;
using Domain.DTOs.Guest.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Guest;
public class UpdateGuestTests : BaseIntegrationTest
{
    public UpdateGuestTests(IntegrationTestFactory factory)
       : base(factory)
    {
    }

    private async Task<Domain.Entities.Event> CreateEventAsync(int id, string name)
    {
        var eventEntity = Domain.Entities.Event.CreateEvent(id, name);
        return await EventRepository.Create(eventEntity);
    }

    [Fact]
    public async Task Update_ShouldUpdateGuest_WhenDataIsValid()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(1, "Party");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(createInput);

        var updateInput = new UpdateGuestRequestDto(createResult.Data!.Id, "John Updated", "john.updated@email.com", "11888888888");

        // Act
        var result = await GuestUseCase.Update(updateInput);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Guest updated with success!");
        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be(updateInput.Name);
    }

    [Fact]
    public async Task Update_ShouldReturnFailure_WhenGuestDoesNotExist()
    {
        // Arrange
        var updateInput = new UpdateGuestRequestDto(9999, "John Updated", "john.updated@email.com", "11888888888");

        // Act
        var result = await GuestUseCase.Update(updateInput);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Guest not found.");

        var exists = await DbContext.Guests.AnyAsync(g => g.Id == updateInput.Id);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenNameIsTooShort()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(2, "Conference");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(createInput);

        var updateInput = new UpdateGuestRequestDto(createResult.Data!.Id, "Jo", "john.updated@email.com", "11888888888");

        // Act
        var act = async () => await GuestUseCase.Update(updateInput);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Name must be at least 3 characters long.");

        var guestInDb = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data.Id);

        guestInDb!.Name.Should().Be(createInput.Name);
        guestInDb!.Name.Should().NotBe(updateInput.Name);
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenEmailIsInvalid()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(3, "Wedding");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(createInput);

        var updateInput = new UpdateGuestRequestDto(createResult.Data!.Id, "John Updated", "invalidemail", "11888888888");

        // Act
        var act = async () => await GuestUseCase.Update(updateInput);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid email format.");

        var guestInDb = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data.Id);

        guestInDb!.Email.Should().Be(createInput.Email);
        guestInDb!.Email.Should().NotBe(updateInput.Email);
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenPhoneNumberIsEmpty()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(4, "Meeting");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(createInput);

        var updateInput = new UpdateGuestRequestDto(createResult.Data!.Id, "John Updated", "john.updated@email.com", "");

        // Act
        var act = async () => await GuestUseCase.Update(updateInput);

        //Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Phone number is required.");

        var guestInDb = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data.Id);

        guestInDb!.PhoneNumber.Should().Be(createInput.PhoneNumber);
        guestInDb!.PhoneNumber.Should().NotBe(updateInput.PhoneNumber);
    }

    [Fact]
    public async Task Update_ShouldNotChangeStatus_WhenUpdatingOtherFields()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(5, "Seminar");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(createInput);

        var guestBeforeUpdate = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        var statusBeforeUpdate = guestBeforeUpdate!.Status;

        var updateInput = new UpdateGuestRequestDto(createResult.Data!.Id, "John Updated", "john.updated@email.com", "11888888888");

        // Act
        await GuestUseCase.Update(updateInput);

        //Assert 
        var guestAfterUpdate = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        guestAfterUpdate!.Status.Should().Be(statusBeforeUpdate);
    }
}
