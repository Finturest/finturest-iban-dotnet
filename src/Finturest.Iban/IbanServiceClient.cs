using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

using Finturest.Iban.Abstractions;
using Finturest.Iban.Abstractions.Models.Requests;
using Finturest.Iban.Abstractions.Models.Responses;
using Finturest.Iban.Constants;
using Finturest.Iban.Models;

namespace Finturest.Iban;

public class IbanServiceClient : IIbanServiceClient
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public IbanServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }

    public async Task<GenerateIbanResponseModel> GenerateIbanAsync(GenerateIbanRequestModel request, CancellationToken cancellationToken = default)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        var uri = $"{RouteConstants.V1}/{RouteConstants.Ibans}";

        var response = await _httpClient.PostAsJsonAsync(uri, request, _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            await HandleBadRequestStatusCode(response, defaultMessage: "Invalid parameters.", cancellationToken).ConfigureAwait(false);
        }
        else
        {
            response.EnsureSuccessStatusCode();
        }

        return await response.Content.ReadFromJsonAsync<GenerateIbanResponseModel>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false) ?? throw new InvalidOperationException("Failed to deserialize response.");
    }

    public async Task<ValidateIbanResponseModel> ValidateIbanAsync(ValidateIbanRequestModel request, CancellationToken cancellationToken = default)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        var uri = $"{RouteConstants.V1}/{RouteConstants.Ibans}/{RouteConstants.Validate}";

        var response = await _httpClient.PostAsJsonAsync(uri, request, _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            await HandleBadRequestStatusCode(response, defaultMessage: "Invalid IBAN.", cancellationToken).ConfigureAwait(false);
        }
        else
        {
            response.EnsureSuccessStatusCode();
        }

        return await response.Content.ReadFromJsonAsync<ValidateIbanResponseModel>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false) ?? throw new InvalidOperationException("Failed to deserialize response.");
    }

    private async Task HandleBadRequestStatusCode(HttpResponseMessage response, string defaultMessage, CancellationToken cancellationToken = default)
    {
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

        var message = problemDetails?.Detail ?? problemDetails?.Title ?? defaultMessage;

        throw new IbanException(message);
    }
}
