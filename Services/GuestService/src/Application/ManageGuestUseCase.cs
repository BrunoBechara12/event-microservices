using Application.UseCases;
using Domain.Ports.In;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class ManageGuestUseCase
{
    public static IServiceCollection AddGuestUseCase(this IServiceCollection services)
    {
        services.AddScoped<IGuestUseCase, GuestUseCase>();
        return services;
    }
}
