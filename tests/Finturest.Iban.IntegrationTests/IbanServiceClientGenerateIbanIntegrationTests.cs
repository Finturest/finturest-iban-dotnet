using Finturest.Iban.Abstractions;
using Finturest.Iban.Abstractions.Models.Requests;

namespace Finturest.Iban.IntegrationTests;

public partial class IbanServiceClientIntegrationTests
{
    [Fact]
    public async Task GenerateIbanRequestApiModel_RequestIsValid_ReturnCorrectResult()
    {
        // Arrange
        var request = new GenerateIbanRequestApiModel
        {
            CountryCode = "FR",
            BankIdentifier = "30006",
            BranchIdentifier = "00001",
            BankAccountNumber = "1234567890189"
        };

        // Act
        var result = await _sut.GenerateIbanAsync(request);

        // Assert
        result.ShouldNotBeNull();

        result.Iban.ShouldBe("FR7630006000011234567890189");
    }

    [Fact]
    public async Task GenerateIbanRequestApiModel_CountryHasInvalidFormat_ThrowIbanException()
    {
        // Arrange
        var request = new GenerateIbanRequestApiModel
        {
            CountryCode = "PLTLY",
            BankIdentifier = "30006",
            BranchIdentifier = "00001",
            BankAccountNumber = "1234567890189"
        };

        // Act
        Func<Task> action = async () => await _sut.GenerateIbanAsync(request).ConfigureAwait(false);

        // Assert
        var assertion = await action.ShouldThrowAsync<IbanException>();

        assertion.Message.ShouldBe("One or more validation errors occurred.");
    }

    [Fact]
    public async Task GenerateIbanRequestApiModel_CountryIsNotSupported_ThrowIbanException()
    {
        // Arrange
        var request = new GenerateIbanRequestApiModel
        {
            CountryCode = "PP",
            BankIdentifier = "30006",
            BranchIdentifier = "00001",
            BankAccountNumber = "1234567890189"
        };

        // Act
        Func<Task> action = async () => await _sut.GenerateIbanAsync(request).ConfigureAwait(false);

        // Assert
        var assertion = await action.ShouldThrowAsync<IbanException>();

        assertion.Message.ShouldBe("The country code 'PP' is not registered. (Parameter 'countryCode')");
    }

    [Fact]
    public async Task GenerateIbanRequestApiModel_BranchIdentifierIsNotSupported_ThrowIbanException()
    {
        // Arrange
        var request = new GenerateIbanRequestApiModel
        {
            CountryCode = "SE",
            BankIdentifier = "912",
            BranchIdentifier = "123",
            BankAccountNumber = "124124"
        };

        // Act
        Func<Task> action = async () => await _sut.GenerateIbanAsync(request).ConfigureAwait(false);

        // Assert
        var assertion = await action.ShouldThrowAsync<IbanException>();

        assertion.Message.ShouldBe("A value for 'Branch' is not supported for country code SE.");
    }
}
