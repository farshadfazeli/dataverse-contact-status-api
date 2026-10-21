using Test.Application.Common.Interfaces;
using Test.Infrastructure.Repositories;
using Data8.PowerPlatform.Dataverse.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(serviceProvider =>
        {
            var settings = configuration.GetSection("Dataverse");
            var serviceUrl = settings["ServiceUrl"];

            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                throw new InvalidOperationException(
                    "Dataverse:ServiceUrl must be supplied through configuration or an environment variable.");
            }

            var username = settings["Username"];
            var password = settings["Password"];

            return string.IsNullOrWhiteSpace(username)
                ? new OnPremiseClient(serviceUrl)
                : new OnPremiseClient(serviceUrl, username, password ?? string.Empty);
        });

        services.AddScoped<IContactRepository, ContactRepository>();
        return services;
    }
}
