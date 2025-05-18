namespace Finturest.Iban.Abstractions.Models.Responses;

/// <summary>
/// Represents the response model returned after validating an IBAN.
/// </summary>
public record ValidateIbanResponseModel
{
    /// <summary>
    /// A required string representing the validated IBAN (International Bank Account Number).
    /// The IBAN follows the format defined by the ISO 13616 standard, consisting of a 2-character country code,
    /// a 2-digit check number, and the bank account number. It typically ranges between 15 and 34 characters in length.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string Iban { get; init; }
#else
    public string Iban { get; set; } = null!;
#endif

    /// <summary>
    /// An optional alphanumeric string representing the bank identifier (e.g., Bank Code or Bank Identifier Code).
    /// This is used to identify the specific bank where the account is held. It can be up to 30 characters long.
    /// </summary>
#if NET7_0_OR_GREATER
    public string? BankIdentifier { get; init; }
#else
    public string? BankIdentifier { get; set; }
#endif

    /// <summary>
    /// An optional alphanumeric string representing the branch identifier.
    /// This is used to identify a specific branch of the bank where the account is held. Up to 30 characters.
    /// </summary>
#if NET7_0_OR_GREATER
    public string? BranchIdentifier { get; init; }
#else
    public string? BranchIdentifier { get; set; }
#endif

    /// <summary>
    /// A required string representing the Bank Account Number (BBAN).
    /// This part of the IBAN is specific to the country and institution. Typically 1 to 30 alphanumeric characters.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string Bban { get; init; }
#else
    public string Bban { get; set; } = null!;
#endif

    /// <summary>
    /// A required object providing the country details associated with the IBAN.
    /// This includes the country name, code, and SEPA membership status.
    /// </summary>
#if NET7_0_OR_GREATER
    public required IbanCountryModel Country { get; init; }
#else
    public IbanCountryModel Country { get; set; } = null!;
#endif
}
