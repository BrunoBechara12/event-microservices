using Application.Ports.In;
using Application.UseCases;
using Domain.Ports.In;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class ManageEventUseCase
{
    public static IServiceCollection AddEventUseCase (this IServiceCollection services)
    {
        services.AddScoped<IEventUseCase, EventUseCase>();
        services.AddScoped<ICollaboratorUseCase, CollaboratorUseCase>();
        return services;
    }
}
