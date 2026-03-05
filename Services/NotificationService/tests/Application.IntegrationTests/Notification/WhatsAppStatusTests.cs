using Domain.Ports.Output;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.IntegrationTests.Notification;

public class WhatsAppStatusTests : BaseIntegrationTest
{
    public WhatsAppStatusTests(IntegrationTestFactory factory) : base(factory) { }

    [Fact]
    public async Task GetWhatsAppStatus_ShouldReturnConnected_WhenConnected()
    {
        // Arrange
        WhatsAppMock
            .Setup(x => x.IsConnectedAsync())
            .ReturnsAsync(true);

        // Act
        var result = await NotificationUseCase.GetWhatsAppStatus();

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data!.IsConnected.Should().BeTrue();
        result.Data.QrCode.Should().BeNull();
    }

    [Fact]
    public async Task GetWhatsAppStatus_ShouldReturnQrCode_WhenNotConnected()
    {
        // Arrange
        var qrCode = "base64encodedqrcode";
        
        WhatsAppMock
            .Setup(x => x.IsConnectedAsync())
            .ReturnsAsync(false);
        
        WhatsAppMock
            .Setup(x => x.GetQrCodeAsync())
            .ReturnsAsync(qrCode);

        // Act
        var result = await NotificationUseCase.GetWhatsAppStatus();

        // Assert
        result.RequestSuccess.Should().BeTrue();
        result.Data!.IsConnected.Should().BeFalse();
        result.Data.QrCode.Should().Be(qrCode);
    }
}
