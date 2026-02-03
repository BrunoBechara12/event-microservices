using Application.IntegrationTests;
using Domain.DTOs.Guest.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Guest;
public class InviteResponseTests : BaseIntegrationTest
{
    public InviteResponseTests(IntegrationTestFactory factory)
       : base(factory)
    {
    }

    private async Task<Domain.Entities.Event> CreateEventAsync(int id, string name)
    {
        var eventEntity = Domain.Entities.Event.CreateEvent(id, name);
        return await EventRepository.Create(eventEntity);
    }

    [Fact]
    public async Task AcceptInvite_ShouldChangeStatusToConfirmed()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(1, "Party");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "John Doe", "john.doe@email.com", "11999999999");
        var createResult = await GuestUseCase.Create(createInput);
        
        var inviteInput = new InviteResponseRequestDto(createResult.Data!.Id);

        // Act
        var result = await GuestUseCase.AcceptInvite(inviteInput);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Invite accepted with success!");

        var guestInDb = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        guestInDb!.Status.Should().Be(Domain.Entities.Guest.InviteStatus.Confirmed);
        guestInDb.ResponseDate.Should().NotBeNull();
    }

    [Fact]
    public async Task AcceptInvite_ShouldReturnFailure_WhenGuestDoesNotExist()
    {
        //Arrange
        var inviteInput = new InviteResponseRequestDto(9999);

        // Act
        var result = await GuestUseCase.AcceptInvite(inviteInput);

        //Assert 
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Guest not found.");
    }

    [Fact]
    public async Task DeclineInvite_ShouldChangeStatusToDeclined()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(2, "Conference");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "Jane Doe", "jane.doe@email.com", "11888888888");
        var createResult = await GuestUseCase.Create(createInput);
        
        var inviteInput = new InviteResponseRequestDto(createResult.Data!.Id);

        // Act
        var result = await GuestUseCase.DeclineInvite(inviteInput);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Invite declined with success!");

        var guestInDb = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        guestInDb!.Status.Should().Be(Domain.Entities.Guest.InviteStatus.Declined);
        guestInDb.ResponseDate.Should().NotBeNull();
    }

    [Fact]
    public async Task DeclineInvite_ShouldReturnFailure_WhenGuestDoesNotExist()
    {
        //Arrange
        var inviteInput = new InviteResponseRequestDto(9999);

        // Act
        var result = await GuestUseCase.DeclineInvite(inviteInput);

        //Assert 
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Guest not found.");
    }

    [Fact]
    public async Task AcceptInvite_ShouldSetUpdatedAt()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(3, "Wedding");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "Maria Silva", "maria.silva@email.com", "11777777777");
        var createResult = await GuestUseCase.Create(createInput);
        
        var guestBeforeAccept = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        var updatedAtBefore = guestBeforeAccept!.UpdatedAt;
        
        var inviteInput = new InviteResponseRequestDto(createResult.Data!.Id);

        // Act
        await GuestUseCase.AcceptInvite(inviteInput);

        //Assert 
        var guestAfterAccept = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        guestAfterAccept!.UpdatedAt.Should().NotBe(updatedAtBefore);
        guestAfterAccept.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task DeclineInvite_ShouldSetUpdatedAt()
    {
        //Arrange
        var eventEntity = await CreateEventAsync(4, "Meeting");
        
        var createInput = new CreateGuestRequestDto(eventEntity.Id, "Carlos Souza", "carlos.souza@email.com", "11666666666");
        var createResult = await GuestUseCase.Create(createInput);
        
        var guestBeforeDecline = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        var updatedAtBefore = guestBeforeDecline!.UpdatedAt;
        
        var inviteInput = new InviteResponseRequestDto(createResult.Data!.Id);

        // Act
        await GuestUseCase.DeclineInvite(inviteInput);

        //Assert 
        var guestAfterDecline = await DbContext.Guests.FirstOrDefaultAsync(g => g.Id == createResult.Data!.Id);
        guestAfterDecline!.UpdatedAt.Should().NotBe(updatedAtBefore);
        guestAfterDecline.UpdatedAt.Should().NotBeNull();
    }
}
