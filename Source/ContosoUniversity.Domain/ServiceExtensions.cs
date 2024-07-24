using Microsoft.Extensions.DependencyInjection;

namespace ContosoUniversity.Domain;

public static class ServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services) => services;
}
