using Application.IntegrationTests;
using Application.UseCases.Collaborator.Inputs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Collaborator;
public class DeleteCollaboratorTests : BaseIntegrationTest
{
    public DeleteCollaboratorTests(IntegrationTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Delete_ShouldRemoveCollaboratorFromDatabase_WhenCollaboratorExists()
    {
        // Arrange
        var createInput = new CreateCollaboratorInput(10, "To Be Deleted");
        var createResult = await CollaboratorUseCase.Create(createInput);
        var idToDelete = createResult.Data!.Id;

        var collaboratorBefore = await DbContext.Collaborators
            .FirstOrDefaultAsync(c => c.Id == idToDelete);

        collaboratorBefore.Should().NotBeNull();

        // Act
        var result = await CollaboratorUseCase.Delete(idToDelete);

        // Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Collaborator deleted with success!");

        var collaboratorAfter = await DbContext.Collaborators
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == idToDelete);

        collaboratorAfter.Should().BeNull("o colaborador não deve mais existir no banco");
    }

    [Fact]
    public async Task Delete_ShouldReturnFailure_WhenCollaboratorDoesNotExist()
    {
        // Arrange
        var nonExistentId = 9999;

        // Act
        var result = await CollaboratorUseCase.Delete(nonExistentId);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Collaborator not found.");
    }
}
