# Finturest IBAN API C# SDK

[![NuGet](https://img.shields.io/nuget/v/Finturest.Iban.svg)](https://www.nuget.org/packages/Finturest.Iban)
[![CI](https://github.com/Finturest/finturest-iban-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/Finturest/finturest-iban-dotnet/actions/workflows/ci.yml)

Official C# SDK for the [Finturest IBAN API](https://finturest.com/products/iban-api) - supports .NET Standard 2.0+ and all modern .NET versions.

## Overview

This SDK provides a fast and secure way to integrate Finturest IBAN validation, generation, and bank data lookup into your .NET applications. It supports .NET Standard 2.0 and later, ensuring compatibility with .NET Core and the latest .NET releases.

## Features

- **IBAN Format Validation**: Validates IBAN structure based on official formats for 100+ countries.

- **IBAN Generation**: Dynamically generates valid IBANs using country-specific rules, given bank and account identifiers.

- **Checksum Verification**: Performs MOD-97 checksum validation to ensure IBAN authenticity.

- **SEPA & Non-SEPA Coverage**: Supports both SEPA and international IBANs for broad banking network compatibility.

- **Real-Time Response**: Get accurate validation, generation, and bank metadata in milliseconds for streamlined financial operations.

## Installation

Using the [.NET Core command-line interface (CLI) tools](https://learn.microsoft.com/en-us/dotnet/core/tools/):

```sh
dotnet add package Finturest.Iban
```

Using the [NuGet Command Line Interface (CLI)](https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference):

```sh
nuget install Finturest.Iban
```

Using the [Package Manager Console](https://docs.microsoft.com/en-us/nuget/tools/package-manager-console):

```powershell
Install-Package Finturest.Iban
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on _Manage NuGet Packages..._
4. Click on the _Browse_ tab and search for "Finturest.Iban".
5. Click on the Finturest.Iban package, select the appropriate version in the
   right-tab and click _Install_.

## Usage

### Registering

To use the `Finturest.Iban` client, register it in your application's dependency injection container using `AddFinturestIban`. This configures the services required to communicate with the Finturest IBAN API.

```C#
var services = new ServiceCollection();

services.AddFinturestIban(options =>
{
    options.ApiKey = "YOUR_API_KEY";
});
```

> **Note**  
> `IIbanServiceClient` is registered in the DI container and should be resolved via dependency injection.  
> In ASP.NET Core applications, it's recommended to inject it through constructor injection.

> **Note**  
> The abstractions for the Finturest Iban API client are provided in a separate package named `Finturest.Iban.Abstractions`.  
> You can reference this package in your business layer to avoid a tight dependency on the implementation.  
> Only the root application or composition root should reference the full `Finturest.Iban` package that contains the implementation.

### Validating

To validate an IBAN using the Finturest IBAN API, create a `ValidateIbanRequestModel` and call the `ValidateIbanRequestModel` method on the `IIbanServiceClient`.

```C#
var serviceProvider = services.BuildServiceProvider();

var ibanServiceClient = serviceProvider.GetRequiredService<IIbanServiceClient>();

try
{
    var request = new ValidateIbanRequestModel
    {
        Iban = "FR7630006000011234567890189"
    };

    var result = await ibanServiceClient.ValidateIbanAsync(request);
    
    Console.WriteLine("IBAN is valid.");
}
catch (IbanException exception)
{
    Console.WriteLine("IBAN is invalid.");
}
```

> **Note**  
> In production applications, avoid using `BuildServiceProvider()` manually.  
> Instead, use constructor injection to get `IIbanServiceClient` from the framework’s dependency injection system.

### Generating

To generate an IBAN using the Finturest IBAN API, create a `GenerateIbanRequestModel` and call the `GenerateIbanAsync` method on the `IIbanServiceClient`.

```C#
try
{
    var request = new GenerateIbanRequestModel
    {
        CountryCode = "FR",
        BankIdentifier = "30006",
        BranchIdentifier = "00001",
        BankAccountNumber = "1234567890189"
    };

    var result = await ibanServiceClient.GenerateIbanAsync(request);
    
    Console.WriteLine($"IBAN: {result.Iban}"); // IBAN: FR7630006000011234567890189
}
catch (IbanException exception)
{
    Console.WriteLine("Invalid parameters.");
}
```

## Subscription & Pricing

To get access to the Finturest IBAN API or subscribe to a plan, please visit the subscription page. An active subscription is required to access the API in production.

[Manage subscriptions](https://finturest.com/dashboard/subscriptions)

## API Key Generation

An API key is required to use the SDK and can be generated on your Finturest dashboard:

[Generate API key](https://finturest.com/dashboard/access-tokens)

## Documentation

For full API reference and usage guides, please visit the official Finturest IBAN API documentation:

[View API reference](https://api.finturest.com/docs/#tag/iban)

## Contact

For support, questions, or inquiries, please contact us at: [support@finturest.com](mailto:support@finturest.com)
