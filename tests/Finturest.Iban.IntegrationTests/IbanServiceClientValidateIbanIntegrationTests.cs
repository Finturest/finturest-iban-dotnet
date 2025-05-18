using Finturest.Iban.Abstractions;
using Finturest.Iban.Abstractions.Models.Requests;

namespace Finturest.Iban.IntegrationTests;

public partial class IbanServiceClientIntegrationTests
{
    [Fact]
    public async Task ValidateIbanAsync_RequestIsValid_ReturnCorrectResult()
    {
        // Arrange
        var request = new ValidateIbanRequestModel
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

    [Fact]
    public async Task ValidateIbanAsync_CountryIsInvalid_ThrowIbanException()
    {
        // Arrange
        var request = new ValidateIbanRequestModel
        {
            Iban = "PP7630006000011234567890189"
        };

        // Act
        Func<Task> action = async () => await _sut.ValidateIbanAsync(request).ConfigureAwait(false);

        // Assert
        var assertion = await action.ShouldThrowAsync<IbanException>();

        assertion.Message.ShouldBe("The country code is unknown/not supported.");
    }

    [Fact]
    public async Task ValidateIbanAsync_ChecksumIsInvalid_ThrowIbanException()
    {
        // Arrange
        var request = new ValidateIbanRequestModel
        {
            Iban = "FR2330006000011234567890189"
        };

        // Act
        Func<Task> action = async () => await _sut.ValidateIbanAsync(request).ConfigureAwait(false);

        // Assert
        var assertion = await action.ShouldThrowAsync<IbanException>();

        assertion.Message.ShouldBe("The IBAN check digits are incorrect.");
    }
}
