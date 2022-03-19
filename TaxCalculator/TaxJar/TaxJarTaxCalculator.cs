using System.Net.Http.Json;
using TaxCalculator.TaxJar.Client;

namespace TaxCalculator.TaxJar;

public class TaxJarTaxCalculator : ITaxCalculator
{
    private readonly HttpClient _client;

    public TaxJarTaxCalculator(HttpClient client, string? taxJarUrl = null, string? apiKey = null)
    {
        _client = client;
        
        if(taxJarUrl != null)
            client.BaseAddress = new Uri(taxJarUrl);
        
        if(apiKey != null)
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }
    
    public async Task<Percentage?> GetTaxRateFor(string zip, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetFromJsonAsync<TaxRateResponse>($"rates/{zip}", cancellationToken);
        return new Percentage(response.combined_district_rate);
    }

    public async Task<Money?> CalculateTaxesFor(Order order, CancellationToken cancellationToken = default)
    {
        var taxesRequest = new TaxesRequest
        {
            amount = order.Amount.Value,
            from_street = order.ShipFrom.Street,
            from_city = order.ShipFrom.City,
            from_state = order.ShipFrom.State,
            from_zip = order.ShipFrom.Zip,
            from_country = order.ShipFrom.Country,
            to_street = order.ShipTo.Street,
            to_city = order.ShipTo.City,
            to_state = order.ShipTo.State,
            to_zip = order.ShipTo.Zip,
            to_country = order.ShipTo.Country,
            shipping = 0
        };
        
        var response = await _client.PostAsJsonAsync("taxes", taxesRequest, cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<TaxesResponse>(cancellationToken: cancellationToken);
        return new Money(content.amount_to_collect);
    }
}