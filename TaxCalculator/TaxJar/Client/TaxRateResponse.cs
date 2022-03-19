// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// these types represent a third-party javascript object, so choosing to stick with naming to match the source

#nullable disable

namespace TaxCalculator.TaxJar.Client;

/// <summary>
/// Represents the response returned by TaxJar's Rates endpoint
/// https://developers.taxjar.com/api/reference/?shell#rates
/// </summary>
public class TaxRateResponse
{
    public string zip { get; init; }
    public string country { get; init; }
    public float country_rate { get; init; }
    public string state { get; init; }
    public float state_rate { get; init; }
    public string county { get; init; }
    public float county_rate { get; init; }
    public string city { get; init; }
    public float city_rate { get; init; }
    public float combined_district_rate { get; init; }
    public float combined_rate { get; init; }
    public bool freight_taxable { get; init; }
}