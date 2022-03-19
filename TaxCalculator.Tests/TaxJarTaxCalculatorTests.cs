using System.Net;
using System.Net.Http;
using FluentAssertions;
using NSubstitute;
using TaxCalculator.TaxJar;
using TaxCalculator.TaxJar.Client;
using Xunit;

namespace TaxCalculator.Tests;

[Collection(ConfigCollection.Name)]
public class GivenARequestForTaxesInZip27403
{
    public class WhenTaxJarReturnsACombinedDistrictRateOf5_75
    {
        private readonly Percentage? _taxRate;

        public WhenTaxJarReturnsACombinedDistrictRateOf5_75()
        {
            var taxesResponse = new TaxRateResponse
            {
                combined_district_rate = 5.75F
            };

            var httpClient = HttpClientBuilder.Build("http://notreal.com/", config =>
            {
                config.ForUrl("/rates/27403")
                    .ReturnsJson(HttpStatusCode.OK, taxesResponse);
            });

            var taxJarTaxCalculator = new TaxJarTaxCalculator(httpClient);
            _taxRate = taxJarTaxCalculator.GetTaxRateFor("27403").ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Fact]
        public void TheTaxRateShouldBe5_75()
        {
            _taxRate.Should().NotBeNull();
            _taxRate.Should().BeEquivalentTo(new Percentage(5.75F));
        }
    }

    public class WhenANonSuccessStatusCodeIsReturned
    {
        private readonly HttpResponseMessage _result;

        public WhenANonSuccessStatusCodeIsReturned()
        {
            var client = HttpClientBuilder.Build("http://notreal.com/", config =>
            {
                config.ForUrl("/rates/27403")
                    .ReturnsJson(HttpStatusCode.InternalServerError);
            });

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient()
                .Returns(client);

            var client2 = httpClientFactory.CreateClient();
            var resultTask = client2.GetAsync("/rates/27403");

            _result = resultTask.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Fact]
        public void TheNonSuccessStatusIsReturned()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            _result.IsSuccessStatusCode.Should().BeFalse();
        }
    }
}