namespace Finturest.Iban.Abstractions.Models;

/// <summary>
/// Represents a country that supports IBAN, including relevant metadata such as SEPA membership.
/// </summary>
public record IbanCountryModel
{
    /// <summary>
    /// A required string representing the official name of the country in English.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string Name { get; init; }
#else
    public string Name { get; set; } = null!;
#endif

    /// <summary>
    /// An optional string representing the local name of the country, if different from the official name.
    /// </summary>
#if NET7_0_OR_GREATER
    public string? LocalName { get; init; }
#else
    public string? LocalName { get; set; }
#endif

    /// <summary>
    /// A required 2-character ISO 3166-1 country code representing the country. This code follows the ISO standard.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string Alpha2Code { get; init; }
#else
    public string Alpha2Code { get; set; } = null!;
#endif

    /// <summary>
    /// A required boolean indicating whether the country is a member of the SEPA (Single Euro Payments Area).
    /// </summary>
#if NET7_0_OR_GREATER
    public required bool IsSepaMember { get; init; }
#else
    public bool IsSepaMember { get; set; }
#endif
}
