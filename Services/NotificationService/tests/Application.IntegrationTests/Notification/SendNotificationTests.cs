using Domain.DTOs.Notification.Requests;
using Domain.Entities;
using Domain.Ports.Output;
using FluentAssertions;
using Moq;
using Xunit;
using Application.IntegrationTests;

namespace Tests.Notification;

public class SendNotificationTests : BaseIntegrationTest
{
    public SendNotificationTests(IntegrationTestFactory factory) : base(factory) { }

    [Fact]
    public async Task SendNotification_ShouldCreateAndSendNotification_WhenValid()
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

        // Act
        var result = await NotificationUseCase.SendNotification(input);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.PhoneNumber.Should().Be("5511999999999");
        result.Data.Message.Should().Be("Test message");
        result.Data.Status.Should().Be("Sent");
        
        WhatsAppMock.Verify(x => x.SendTextMessageAsync(
            It.Is<string>(p => p.Contains("5511999999999")),
            "Test message"), Times.Once);
    }

    [Fact]
    public async Task SendNotification_ShouldMarkAsFailed_WhenWhatsAppFails()
    {
        // Arrange
        var input = new SendNotificationRequestDto(
            "5511888888888",
            "Failed message",
            NotificationType.Custom
        );

        WhatsAppMock
            .Setup(x => x.SendTextMessageAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new WhatsAppSendResult(false, ErrorMessage: "Connection refused"));

        // Act
        var result = await NotificationUseCase.SendNotification(input);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Status.Should().Be("Failed");
        result.Data.ErrorMessage.Should().Be("Connection refused");
    }

    [Fact]
    public async Task SendNotification_ShouldStoreCorrectType_WhenProvided()
    {
        // Arrange
        var input = new SendNotificationRequestDto(
            "5511777777777",
            "Event notification",
            NotificationType.EventInvitation
        );

        WhatsAppMock
            .Setup(x => x.SendTextMessageAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new WhatsAppSendResult(true, "msg-456"));

        // Act
        var result = await NotificationUseCase.SendNotification(input);

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data!.Type.Should().Be("EventInvitation");
    }
}
