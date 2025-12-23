using Application.IntegrationTests;
using Application.UseCases.Collaborator.Inputs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Collaborator;
public class CreateCollaboratorTests : BaseIntegrationTest
{
    public CreateCollaboratorTests(IntegrationTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldAddCollaboratorToDatabase_WhenDataIsValid()
    {
        // Arrange
        var input = new CreateCollaboratorInput(123, "John Doe");

        // Act
        var result = await CollaboratorUseCase.Create(input);

        // Assert 
        result.RequestSuccess.Should().BeTrue();
        result.Message.Should().Be("Collaborator created with success!");
        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be(input.Name);

        var collaboratorInDb = await DbContext.Collaborators
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == result.Data.Id);

        collaboratorInDb.Should().NotBeNull();
        collaboratorInDb.Name.Should().Be(input.Name);
        collaboratorInDb.UserId.Should().Be(input.UserId);
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenNameIsTooShort()
    {
        // Arrange
        var input = new CreateCollaboratorInput(1, "Jo");

        // Act
        var act = async () => await CollaboratorUseCase.Create(input);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Name must be at least 3 characters long");

        // Assert 
        var exists = await DbContext.Collaborators.AnyAsync(c => c.UserId == input.UserId);
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenUserIdIsInvalid()
    {
        // Arrange
        var input = new CreateCollaboratorInput(0, "John Doe");

        // Act
        var act = async () => await CollaboratorUseCase.Create(input);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid user id");

        var exists = await DbContext.Collaborators.AnyAsync(c => c.Name == input.Name);
        exists.Should().BeFalse();
    }
}
