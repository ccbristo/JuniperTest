using System.Diagnostics.CodeAnalysis;

namespace TaxCalculator.TaxJar.Client;

#nullable disable

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class TaxJarSettings
{
    public Uri Url { get; set; }
    public string ApiKey { get; set; }
}