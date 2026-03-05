using Domain.DTOs.Template.Requests;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Template;

public class TemplateTests : BaseIntegrationTest
{
    public TemplateTests(IntegrationTestFactory factory) : base(factory) { }

    [Fact]
    public async Task GetAll_ShouldReturnDefaultTemplates()
    {
        // Act
        var result = await TemplateUseCase.GetAll();

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data.Should().NotBeEmpty();
        result.Data.Should().Contain(t => t.Type == "EventInvitation");
        result.Data.Should().Contain(t => t.Type == "EventReminder");
        result.Data.Should().Contain(t => t.Type == "InviteConfirmation");
    }

    [Fact]
    public async Task Create_ShouldCreateTemplate_WhenValid()
    {
        // Arrange
        var input = new CreateTemplateRequestDto(
            "Custom Template",
            NotificationType.Custom,
            "Hello {name}! This is a custom message."
        );

        // Act
        var result = await TemplateUseCase.Create(input);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be("Custom Template");
        result.Data.Type.Should().Be("Custom");
        result.Data.Template.Should().Contain("{name}");
        result.Data.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task Update_ShouldUpdateTemplate_WhenExists()
    {
        // Arrange 
        var createInput = new CreateTemplateRequestDto(
            "Original Name",
            NotificationType.Custom,
            "Original template"
        );
        var createResult = await TemplateUseCase.Create(createInput);
        var templateId = createResult.Data!.Id;

        var updateInput = new UpdateTemplateRequestDto(
            "Updated Name",
            "Updated template with {placeholder}"
        );

        // Act
        var result = await TemplateUseCase.Update(templateId, updateInput);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data!.Name.Should().Be("Updated Name");
        result.Data.Template.Should().Be("Updated template with {placeholder}");
        result.Data.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task Delete_ShouldDeleteTemplate_WhenExists()
    {
        // Arrange
        var createInput = new CreateTemplateRequestDto(
            "Template to Delete",
            NotificationType.Custom,
            "Will be deleted"
        );
        var createResult = await TemplateUseCase.Create(createInput);
        var templateId = createResult.Data!.Id;

        // Act
        var deleteResult = await TemplateUseCase.Delete(templateId);
        var getResult = await TemplateUseCase.GetById(templateId);

        // Assert
        deleteResult.RequestSuccess.Should().BeTrue();
        getResult.RequestSuccess.Should().BeFalse();
        getResult.Message.Should().Contain("not found");
    }

    [Fact]
    public async Task GetById_ShouldReturnFailure_WhenNotExists()
    {
        // Act
        var result = await TemplateUseCase.GetById(99999);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Contain("not found");
    }
}
