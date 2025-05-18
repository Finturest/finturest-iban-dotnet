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
}
