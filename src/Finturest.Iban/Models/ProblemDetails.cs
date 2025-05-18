namespace Finturest.Iban.Models;

internal sealed record ProblemDetails
{
#if NET7_0_OR_GREATER
    public string? Title { get; init; }
#else
    public string? Title { get; set; }
#endif

#if NET7_0_OR_GREATER
    public string? Detail { get; init; }
#else
    public string? Detail { get; set; }
#endif
}
