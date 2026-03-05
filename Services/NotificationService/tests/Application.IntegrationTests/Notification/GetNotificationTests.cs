using Domain.DTOs.Notification.Requests;
using Domain.Entities;
using Domain.Ports.Output;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.IntegrationTests.Notification;

public class GetNotificationTests : BaseIntegrationTest
{
    public GetNotificationTests(IntegrationTestFactory factory) : base(factory) { }

    [Fact]
    public async Task GetById_ShouldReturnNotification_WhenExists()
    {
        // Arrange 
        var input = new SendNotificationRequestDto(
            "5511999999999",
            "Test message",
            NotificationType.Custom
        );

        WhatsAppMock
            .Setup(x => x.SendTextMessageAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new WhatsAppSendResult(true, "msg-123"));

        var createResult = await NotificationUseCase.SendNotification(input);
        var notificationId = createResult.Data!.Id;

        // Act
        var result = await NotificationUseCase.GetById(notificationId);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be(notificationId);
        result.Data.PhoneNumber.Should().Be("5511999999999");
    }

    [Fact]
    public async Task GetById_ShouldReturnFailure_WhenNotExists()
    {
        // Act
        var result = await NotificationUseCase.GetById(99999);

        // Assert
        result.RequestSuccess.Should().BeFalse();
        result.Message.Should().Contain("not found");
    }
}
