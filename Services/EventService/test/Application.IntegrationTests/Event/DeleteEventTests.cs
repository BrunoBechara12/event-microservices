using Application.UseCases.Event.Inputs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests.Event;
public class DeleteEventTests : BaseIntegrationTest
{
    public DeleteEventTests(IntegrationTestFactory factory)
       : base(factory)
    {
    }

    [Fact]
    public async Task Delete_ShouldDeleteEventFromDatabase()
    {
        //Arrange
        var input = new CreateEventInput("Event", "Event 1 description", "Street 2", DateTime.UtcNow.AddDays(2), 2);
        var createResult = await EventUseCase.Create(input);

        // Act
        var resultGetBeforeDelete = await DbContext.Events.FirstOrDefaultAsync(e => e.Id == createResult.Data!.Id);

        var result = await EventUseCase.Delete(createResult.Data!.Id);

        var resultGetAfterDelete = await DbContext.Events.FirstOrDefaultAsync(e => e.Id == createResult.Data!.Id);

        //Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Event deleted with success!");

        resultGetBeforeDelete.Should().NotBeNull();
        resultGetBeforeDelete.Name.Should().Be(input.Name);

        resultGetAfterDelete.Should().BeNull();
    }

    [Fact]
    public async Task Delete_ShouldReturnFailure_WhenEventDoesNotExist()
    {
        var fakeId = 2;

        // Act
        var result = await EventUseCase.Delete(fakeId);

        //Assert 
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Event not found.");

        var exists = await DbContext.Events.AnyAsync(c => c.Id == fakeId);
        exists.Should().BeFalse();
    }

}
