using Application.Ports.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases;
public static class ManageGuestUseCase
{
    public static IServiceCollection AddGuestUseCase(this IServiceCollection services)
    {
        services.AddScoped<IGuestUseCase, GuestUseCase>();
        return services;
    }
}
