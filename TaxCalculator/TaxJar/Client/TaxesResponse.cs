// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// these types represent a third-party javascript object, so choosing to stick with naming to match the source

#nullable disable

namespace TaxCalculator.TaxJar.Client;

/// <summary>
/// Represents the response returned from TaxJar's Taxes endpoint
/// https://developers.taxjar.com/api/reference/?shell#taxes
/// </summary>
public class TaxesResponse
{
    public float order_total_amount { get; init; }
    public float shipping { get; init; }
    public int taxable_amount { get; init; }
    public float amount_to_collect { get; init; }
    public float rate { get; init; }
    public bool has_nexus { get; init; }
    public bool freight_taxable { get; init; }
    public string tax_source { get; init; }
    public Jurisdictions jurisdictions { get; init; }
    public Breakdown breakdown { get; init; }
}

public class Jurisdictions
{
    public string country { get; init; }
    public string state { get; init; }
    public string county { get; init; }
    public string city { get; init; }
}

public class Breakdown
{
    public float taxable_amount { get; init; }
    public float tax_collectable { get; init; }
    public float combined_tax_rate { get; init; }
    public float state_taxable_amount { get; init; }
    public float state_tax_rate { get; init; }
    public float state_tax_collectable { get; init; }
    public float county_taxable_amount { get; init; }
    public float county_tax_rate { get; init; }
    public float county_tax_collectable { get; init; }
    public float city_taxable_amount { get; init; }
    public float city_tax_rate { get; init; }
    public float city_tax_collectable { get; init; }
    public float special_district_taxable_amount { get; init; }
    public float special_tax_rate { get; init; }
    public float special_district_tax_collectable { get; init; }
    public IList<LineItem> line_items { get; init; }
}