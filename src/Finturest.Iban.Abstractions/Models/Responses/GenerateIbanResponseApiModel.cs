namespace Finturest.Iban.Abstractions.Models.Responses;

/// <summary>
/// Represents the response model containing the generated IBAN.
/// </summary>
public record GenerateIbanResponseApiModel
{
    /// <summary>
    /// A required string representing the generated IBAN (International Bank Account Number).
    /// The IBAN follows the format defined by the ISO 13616 standard, consisting of a 2-character country code,
    /// a 2-digit check number, and the bank account number. It is alphanumeric and typically ranges between
    /// 15 and 34 characters in length, depending on the country.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string Iban { get; init; }
#else
    public string Iban { get; set; } = null!;
#endif
}
