using Domain.Ports.Input;
using Application.UseCases.Collaborator;
using Application.UseCases.Event;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddEventUseCase(this IServiceCollection services)
    {
        services.AddScoped<IEventUseCase, EventUseCase>();
        services.AddScoped<ICollaboratorUseCase, CollaboratorUseCase>();
        return services;
    }
}
