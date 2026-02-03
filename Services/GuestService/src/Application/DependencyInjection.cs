using Application.UseCases;
using Domain.Ports.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddGuestUseCase(this IServiceCollection services)
    {
        services.AddScoped<IGuestUseCase, GuestUseCase>();
        return services;
    }
}
