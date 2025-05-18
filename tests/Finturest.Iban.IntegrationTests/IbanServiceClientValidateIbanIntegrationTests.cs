using Finturest.Iban.Abstractions.Models.Requests;

namespace Finturest.Iban.IntegrationTests;

public partial class IbanServiceClientIntegrationTests
{
    [Fact]
    public async Task ValidateIbanAsync_RequestIsValid_ReturnCorrectResult()
    {
        // Arrange
        var request = new ValidateIbanRequestApiModel
        {
            Iban = "FR7630006000011234567890189"
        };

        // Act
        var result = await _sut.ValidateIbanAsync(request);

        // Assert
        result.ShouldNotBeNull();

        result.Iban.ShouldBe(request.Iban);
        result.BankIdentifier.ShouldBe("30006");
        result.BranchIdentifier.ShouldBe("00001");
        result.Bban.ShouldBe("30006000011234567890189");

        result.Country.ShouldNotBeNull();
        result.Country.Name.ShouldBe("France");
        result.Country.LocalName.ShouldBe("France");
        result.Country.Alpha2Code.ShouldBe("FR");
        result.Country.IsSepaMember.ShouldBeTrue();
    }
}
