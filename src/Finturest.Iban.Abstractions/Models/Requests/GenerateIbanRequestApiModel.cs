namespace Finturest.Iban.Abstractions.Models.Requests;

/// <summary>
/// Represents the request model for generating an IBAN (International Bank Account Number).
/// </summary>
public record GenerateIbanRequestApiModel
{
    /// <summary>
    /// A required string representing the 2-character ISO 3166-1 country code for the country where the bank is located.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string CountryCode { get; init; }
#else
    public string CountryCode { get; set; } = null!;
#endif

    /// <summary>
    /// An optional string representing the alphanumeric bank identifier (such as a bank code). 
    /// It can be up to 30 alphanumeric characters long. This is used to uniquely identify the bank within the country.
    /// </summary>
#if NET7_0_OR_GREATER
    public string? BankIdentifier { get; init; }
#else
    public string? BankIdentifier { get; set; }
#endif

    /// <summary>
    /// An optional string representing the alphanumeric branch identifier. 
    /// It can be up to 30 alphanumeric characters long. This identifies a specific branch of the bank, if applicable.
    /// </summary>
#if NET7_0_OR_GREATER
    public string? BranchIdentifier { get; init; }
#else
    public string? BranchIdentifier { get; set; }
#endif

    /// <summary>
    /// A required string representing the bank account number. 
    /// It must be an alphanumeric string, between 1 and 30 characters long, that uniquely identifies an account within the bank.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string BankAccountNumber { get; init; }
#else
    public string BankAccountNumber { get; set; } = null!;
#endif
}
