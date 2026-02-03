using Application.IntegrationTests;
using Domain.DTOs.Collaborator.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Collaborator;
public class UpdateCollaboratorTests : BaseIntegrationTest
{
    public UpdateCollaboratorTests(IntegrationTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Update_ShouldUpdate_WhenCollaboratorExists()
    {
        // Arrange
        var createInput = new CreateCollaboratorRequestDto(10, "Original Name");
        var createResult = await CollaboratorUseCase.Create(createInput);
        var collaboratorId = createResult.Data!.Id;

        var updateInput = new UpdateCollaboratorRequestDto(collaboratorId, 99, "Updated Name");

        // Act
        var result = await CollaboratorUseCase.Update(updateInput);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Collaborator updated with success!");
        result.Data!.Name.Should().Be(updateInput.Name);

        var collaboratorInDb = await DbContext.Collaborators
            .FirstOrDefaultAsync(c => c.Id == collaboratorId);

        collaboratorInDb.Should().NotBeNull();
        collaboratorInDb.Name.Should().Be(updateInput.Name);
        collaboratorInDb.UserId.Should().Be(updateInput.UserId);

        collaboratorInDb.Name.Should().NotBe(createInput.Name);
    }

    [Fact]
    public async Task Update_ShouldReturnFailure_WhenCollaboratorDoesNotExist()
    {
        // Arrange
        var updateInput = new UpdateCollaboratorRequestDto(9999, 1, "Ghost");

        // Act
        var result = await CollaboratorUseCase.Update(updateInput);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Be("Collaborator not found.");

        var exists = await DbContext.Collaborators.AnyAsync(c => c.Id == updateInput.Id);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenNameIsTooShort()
    {
        // Arrange
        var createInput = new CreateCollaboratorRequestDto(1, "Valid Name");
        var createResult = await CollaboratorUseCase.Create(createInput);
        var originalId = createResult.Data!.Id;

        var updateInput = new UpdateCollaboratorRequestDto(originalId, 1, "Jo");

        // Act
        var act = async () => await CollaboratorUseCase.Update(updateInput);

        // Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Name must be at least 3 characters long");

        var collaboratorInDb = await DbContext.Collaborators
            .FirstAsync(c => c.Id == originalId);

        collaboratorInDb.Name.Should().Be("Valid Name"); 
    }

    [Fact]
    public async Task Update_ShouldThrowArgumentException_WhenUserIdIsInvalid()
    {
        // Arrange
        var createInput = new CreateCollaboratorRequestDto(50, "Valid Name");
        var createResult = await CollaboratorUseCase.Create(createInput);
        var originalId = createResult.Data!.Id;

        var updateInput = new UpdateCollaboratorRequestDto(originalId, 0, "Valid Name");

        // Act
        var act = async () => await CollaboratorUseCase.Update(updateInput);

        // Assert 
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid user id");

        var collaboratorInDb = await DbContext.Collaborators
            .FirstAsync(c => c.Id == originalId);

        collaboratorInDb.UserId.Should().Be(50); 
    }
}
