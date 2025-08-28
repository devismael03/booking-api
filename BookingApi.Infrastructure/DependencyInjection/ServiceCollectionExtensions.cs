using BookingApi.Application.Interfaces;
using BookingApi.Application.Services;
using BookingApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApi.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IHomeRepository, InMemoryHomeRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IHomeApplicationService, HomeApplicationService>();

        return services;
    }
}