using Adapters.Secondary.Context;
using Domain.Ports.Input;
using Domain.Ports.Output;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestFactory>
{
    private readonly IServiceScope _scope;
    protected readonly INotificationUseCase NotificationUseCase;
    protected readonly ITemplateUseCase TemplateUseCase;
    protected readonly INotificationRepository NotificationRepository;
    protected readonly IMessageTemplateRepository TemplateRepository;
    protected readonly NotificationDbContext DbContext;
    protected readonly IntegrationTestFactory Factory;
    protected readonly Mock<IWhatsAppService> WhatsAppMock;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        Factory = factory;
        _scope = factory.Services.CreateScope();

        factory.ResetDatabaseAsync().GetAwaiter().GetResult();

        NotificationUseCase = _scope.ServiceProvider.GetRequiredService<INotificationUseCase>();
        TemplateUseCase = _scope.ServiceProvider.GetRequiredService<ITemplateUseCase>();
        NotificationRepository = _scope.ServiceProvider.GetRequiredService<INotificationRepository>();
        TemplateRepository = _scope.ServiceProvider.GetRequiredService<IMessageTemplateRepository>();
        DbContext = _scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        WhatsAppMock = factory.WhatsAppServiceMock;
    }
}
