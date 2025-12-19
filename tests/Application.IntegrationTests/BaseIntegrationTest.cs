using Adapters.Secondary.Data.Context;
using Application.Ports.In;
using Domain.Ports.Output;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestFactory>
{
    private readonly IServiceScope _scope;
    protected readonly IEventRepository EventRepository;
    protected readonly IEventUseCase EventUseCase;
    protected readonly EventDbContext DbContext; 

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        _scope = factory.Services.CreateScope();

        factory.ResetDatabaseAsync().GetAwaiter().GetResult();

        EventRepository = _scope.ServiceProvider.GetRequiredService<IEventRepository>();
        EventUseCase = _scope.ServiceProvider.GetRequiredService<IEventUseCase>();

        DbContext = _scope.ServiceProvider.GetRequiredService<EventDbContext>(); 
    }
}
