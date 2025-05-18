namespace Finturest.Iban.Abstractions.Models.Requests;

/// <summary>
/// Represents the request model for validating an IBAN (International Bank Account Number).
/// </summary>
public record ValidateIbanRequestApiModel
{
    /// <summary>
    /// A required string representing the IBAN (International Bank Account Number) that needs to be validated. 
    /// The IBAN must be between 5 and 40 characters long and follow the standard format, which includes 
    /// a country code, check digits, and a domestic bank account number.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string Iban { get; init; }
#else
    public string Iban { get; set; } = null!;
#endif
}