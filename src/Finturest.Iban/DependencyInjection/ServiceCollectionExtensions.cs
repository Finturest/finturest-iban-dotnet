using Finturest.Iban.Abstractions;
using Finturest.Iban.Constants;
using Finturest.Iban.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Finturest.Iban.DependencyInjection;

/// <summary>
/// Provides extension methods to register the Finturest IBAN API client and its dependencies with the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures the Finturest IBAN client using configuration from the specified <see cref="IConfigurationSection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configurationSection">The configuration section containing settings for <see cref="IbanOptions"/>.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> or <paramref name="configurationSection"/> is <c>null</c>.</exception>
    public static IServiceCollection AddFinturestIban(this IServiceCollection services, IConfigurationSection configurationSection)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configurationSection);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configurationSection is null)
        {
            throw new ArgumentNullException(nameof(configurationSection));
        }
#endif

        services.Configure<IbanOptions>(configurationSection);

        services.AddFinturestIban();

        return services;
    }

    /// <summary>
    /// Adds and configures the Finturest IBAN client using an action delegate to set <see cref="IbanOptions"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configureOptions">An action delegate to configure the <see cref="IbanOptions"/>.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> or <paramref name="configureOptions"/> is <c>null</c>.</exception>
    public static IServiceCollection AddFinturestIban(this IServiceCollection services, Action<IbanOptions> configureOptions)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions is null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }
#endif

        services.Configure(configureOptions);

        services.AddFinturestIban();

        return services;
    }

    private static IServiceCollection AddFinturestIban(this IServiceCollection services)
    {
        services.AddHttpClient<IIbanServiceClient, IbanServiceClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<IbanOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseAddress);

            client.DefaultRequestHeaders.Add(HeaderConstants.ApiKey, options.ApiKey);
        });

        return services;
    }
}
