namespace Finturest.Iban.Options;

/// <summary>
/// Represents configuration options for accessing the Finturest IBAN API.
/// </summary>
public record IbanOptions
{
    /// <summary>
    /// Gets or sets the API key used to authenticate requests to the Finturest IBAN API.
    /// This property is required.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string ApiKey { get; set; }
#else
    public string ApiKey { get; set; } = null!;
#endif

    /// <summary>
    /// Gets or sets the base URL of the Finturest IBAN API.
    /// Defaults to <c>https://api.finturest.com/</c>.
    /// </summary>
    public string BaseAddress { get; set; } = "https://api.finturest.com/";
}
