using Application.UseCases;
using Domain.Ports.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddNotificationUseCase(this IServiceCollection services)
    {
        services.AddScoped<INotificationUseCase, NotificationUseCase>();
        services.AddScoped<ITemplateUseCase, TemplateUseCase>();
        return services;
    }
}
