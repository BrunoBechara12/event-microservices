using Application.IntegrationTests;
using Domain.DTOs.Guest.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Guest;
public class DeleteGuestTests : BaseIntegrationTest
{
    public DeleteGuestTests(IntegrationTestFactory factory)
       : base(factory)
    {
    }

    private async Task<Domain.Entities.Event> CreateEventAsync(int id, string name)
    {
        var eventEntity = Domain.Entities.Event.CreateEvent(id, name);
        return await EventRepository.Create(eventEntity);
    }

    [Fact]
    public async Task Delete_ShouldDeleteGuestFromDatabase()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(1, "Party");
        
        var input = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(input);

        // Act
        var resultGetBeforeDelete = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);

        var result = await GuestUseCase.Delete(createResult.Data!.Id);

        var resultGetAfterDelete = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Guest deleted with success!");

        resultGetBeforeDelete.Should().NotBeNull();
        resultGetBeforeDelete!.Name.Should().Be(input.Name);

        resultGetAfterDelete.Should().BeNull();
    }

    [Fact]
    public async Task Delete_ShouldReturnFailure_WhenGuestDoesNotExist()
    {
        var fakeId = 2;

        // Act
        var result = await GuestUseCase.Delete(fakeId);

        //Assert 
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Guest not found.");

        var exists = await DbContext.Guests.AnyAsync(g => g.Id == fakeId);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_ShouldOnlyDeleteSpecifiedGuest_WhenMultipleGuestsExist()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(2, "Conference");
        
        var input1 = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var input2 = new CreateGuestRequestDto(eventEntity.Id, "Jane Doe", "jane.doe@email.com", "11888888888");
        
        var createResult1 = await GuestUseCase.Create(input1);
        var createResult2 = await GuestUseCase.Create(input2);

        // Act
        await GuestUseCase.Delete(createResult1.Data!.Id);

        //Assert 
        var deletedGuest = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult1.Data!.Id);
        var remainingGuest = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult2.Data!.Id);

        deletedGuest.Should().BeNull();
        remainingGuest.Should().NotBeNull();
        remainingGuest!.Name.Should().Be(input2.Name);
    }
}
