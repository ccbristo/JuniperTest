using System.Net;
using System.Net.Http;
using FluentAssertions;
using TaxCalculator.TaxJar;
using Xunit;

namespace TaxCalculator.Tests;

[Collection(ConfigCollection.Name)]
public class TaxJarIntegrationTests
{
    [Fact, Trait("Category", "IntegrationTest")]
    public async void TaxJarTaxEndpointCanBeCalled()
    {
        var taxJarCalculator = CreateCalculator();
        var taxRate = await taxJarCalculator.GetTaxRateFor("27403");
        taxRate.Should().NotBeNull();
    }
    
    [Fact, Trait("Category", "IntegrationTest")]
    public async void TaxJarRatesEndpointCanBeCalled()
    {
        var taxJarCalculator = CreateCalculator();
        
        var customer = new Customer(1, "Chris", "Bristol");
        var shipFrom = new Address(2, "123 Elm St", "Los Angeles", "CA", "90210", "US");
        var shipTo = new Address(2, "456 Main St", "Greensboro", "NC", "27403", "US");
        var orderAmount = new Money(500F);
        var order = new Order(customer, shipTo, shipFrom, orderAmount);
        
        var taxesForOrder = await taxJarCalculator.CalculateTaxesFor(order);
        taxesForOrder.Should().NotBeNull();
    }

    private static TaxJarTaxCalculator CreateCalculator()
    {
        var taxJarConfig = ConfigFixture.Configuration.GetSection("TaxJar");
        var apiKey = taxJarConfig["ApiKey"];
        var url = taxJarConfig["Url"];

        var httpClient = new HttpClient();
        var taxJarCalculator = new TaxJarTaxCalculator(httpClient, url, apiKey);
        return taxJarCalculator;
    }
}