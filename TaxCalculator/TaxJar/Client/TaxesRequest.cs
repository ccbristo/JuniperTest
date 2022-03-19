// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// these types represent a third-party javascript object, so choosing to stick with naming to match the source

#nullable disable

namespace TaxCalculator.TaxJar.Client;

/// <summary>
/// Represents the request sent to TaxJar's Taxes endpoint
/// https://developers.taxjar.com/api/reference/?shell#taxes
/// </summary>
public class TaxesRequest
{
    public string from_country { get; init; }
    public string from_zip { get; init; }
    public string from_state { get; init; }
    public string from_city { get; init; }
    public string from_street { get; init; }
    public string to_country { get; init; }
    public string to_zip { get; init; }
    public string to_state { get; init; }
    public string to_city { get; init; }
    public string to_street { get; init; }
    public float amount { get; init; }
    public float shipping { get; init; }
    public IList<NexusAddress> nexus_addresses { get; init; }
    public IList<LineItem> line_items { get; init; }
}

public class LineItem
{
    public string id { get; init; }
    public int quantity { get; init; }
    public string product_tax_code { get; init; }
    public float unit_price { get; init; }
    public float discount { get; init; }
}

public class NexusAddress
{
    public string id { get; init; }
    public string country { get; init; }
    public string zip { get; init; }
    public string state { get; init; }
    public string city { get; init; }
    public string street { get; init; }
}