using Adapters.Secondary.Context;
using Domain.Ports.Input;
using Domain.Ports.Output;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestFactory>
{
    private readonly IServiceScope _scope;
    protected readonly IGuestRepository GuestRepository;
    protected readonly IGuestUseCase GuestUseCase;
    protected readonly IEventRepository EventRepository;
    protected readonly GuestDbContext DbContext;
    protected readonly IntegrationTestFactory Factory;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        Factory = factory;
        _scope = factory.Services.CreateScope();

        factory.ResetDatabaseAsync().GetAwaiter().GetResult();

        GuestRepository = _scope.ServiceProvider.GetRequiredService<IGuestRepository>();
        GuestUseCase = _scope.ServiceProvider.GetRequiredService<IGuestUseCase>();
        EventRepository = _scope.ServiceProvider.GetRequiredService<IEventRepository>();

        DbContext = _scope.ServiceProvider.GetRequiredService<GuestDbContext>();
    }
}
