using System.Net;
using System.Net.Http;

using Finturest.Iban.Abstractions;
using Finturest.Iban.Abstractions.Models.Requests;
using Finturest.Iban.DependencyInjection;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace Finturest.Iban.IntegrationTests;

/// <summary>
/// Integration tests for <see cref="IIbanServiceClient"/>
/// </summary>
public partial class IbanServiceClientIntegrationTests
{
    private readonly IIbanServiceClient _sut;

    public IbanServiceClientIntegrationTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
#if DEBUG
            .AddUserSecrets<IbanServiceClientIntegrationTests>()
#endif
            .Build();

        var apiKey = configuration["Iban:ApiKey"] ?? throw new InvalidOperationException("Finturest IBAN API key must be set in environment or user secrets.");

        _sut = BuildClient(apiKey);
    }

    [Fact]
    public async Task SendRequestAsync_ApiKeyIsNotValid_EnsureUnauthorizedStatusCode()
    {
        // Arrange
        var request = new GenerateIbanRequestApiModel
        {
            CountryCode = "PL",
            BankAccountNumber = "123456789"
        };

        // Act
        Func<Task> action = async () => await BuildClient(apiKey: "invalid-api-key").GenerateIbanAsync(request).ConfigureAwait(false);

        // Assert
#if NET5_0_OR_GREATER
        var assertion = await action.ShouldThrowAsync<HttpRequestException>();

        assertion.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
#else
        await action.ShouldThrowAsync<HttpRequestException>();
#endif
    }

    private static IIbanServiceClient BuildClient(string apiKey)
    {
        var services = new ServiceCollection();

        services.AddFinturestIban(options =>
        {
            options.ApiKey = apiKey;
        });

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IIbanServiceClient>();
    }
}
