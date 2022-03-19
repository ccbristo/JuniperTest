using System.Net;
using System.Net.Http;
using FluentAssertions;
using NSubstitute;
using TaxCalculator.TaxJar;
using Xunit;

namespace TaxCalculator.Tests;

[Collection(ConfigCollection.Name)]
public class GivenARequestForTaxRatesIn27403
{
    public class WhenTaxesAreRequestedForAnOrder
    {
        private Money? _result;

        public WhenTaxesAreRequestedForAnOrder()
        {
            var taxRates = new TaxJar.Client.TaxesResponse
            {
                amount_to_collect = 10F
            };
            
            var httpClient = HttpClientBuilder.Build("http://notreal.com", config =>
            {
                config.ForUrl("/taxes")
                    .ReturnsJson(HttpStatusCode.OK, taxRates);
            });

            var taxJarTaxCalculator = new TaxJarTaxCalculator(httpClient);
            var customer = new Customer(1, "Chris", "Bristol");
            var shipFrom = new Address(2, "123 Elm St", "Los Angeles", "CA", "90210", "US");
            var shipTo = new Address(2, "456 Main St", "Greensboro", "NC", "27403", "US");
            var orderAmount = new Money(500F);
            
            var order = new Order(customer, shipTo, shipFrom, orderAmount);
            _result = taxJarTaxCalculator.CalculateTaxesFor(order)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Fact]
        public void TheAmountOfTaxToCollectShouldBe10()
        {
            _result.Should().Be(new Money(10F));
        }
    }
}