using Finturest.Iban.Abstractions.Models.Requests;
using Finturest.Iban.Abstractions.Models.Responses;

namespace Finturest.Iban.Abstractions;

/// <summary>
/// Provides methods for sending requests to and receiving responses from the Finturest IBAN API.
/// </summary>
public interface IIbanServiceClient
{
    /// <summary>
    /// Generate IBAN
    /// </summary>
    /// <param name="request">Request model to generate IBAN</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <exception cref="ArgumentNullException">The request model was null.</exception>
    /// <exception cref="InvalidOperationException">The request failed due to deserialization issue.</exception>
    /// <exception cref="HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
    /// <exception cref="IbanException">Thrown when the input values violate business rules specific to the IBAN generation logic (e.g., unsupported country, invalid structure, or failed checksum).</exception>
    Task<GenerateIbanResponseApiModel> GenerateIbanAsync(GenerateIbanRequestApiModel request, CancellationToken cancellationToken = default);
}
