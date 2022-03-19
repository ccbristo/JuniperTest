using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace TaxCalculator.Tests;

public class GivenATaxService
{
    public class WhenTheTaxRateIsRequested
    {
        [Fact]
        public async Task TheTaxServiceIsCalled()
        {
            var taxCalculator = Substitute.For<ITaxCalculator>();
            const string zip = "27403";
            var expected = new Percentage(6.25F);
            taxCalculator.GetTaxRateFor(zip).Returns(expected);

            var taxService = new TaxService(taxCalculator);

            var taxRate = await taxService.GetTaxRateFor(zip);
            taxRate.Should().Be(expected);

            await taxCalculator.Received().GetTaxRateFor(zip);
        }
    }

    public class WhenTaxesAreCalculatedForAnOrder
    {
        [Fact]
        public async Task TheTaxServiceIsCalled()
        {
            var taxCalculator = Substitute.For<ITaxCalculator>();

            var customer = new Customer(1, "Some", "Body");
            var shipFrom = new Address(2, "123 Main St", "Greensboro", "NC", "27403", "US");
            var shipTo = new Address(3, "123 Main St", "Greensboro", "NC", "27403", "US");

            var orderAmount = new Money(50F);
            var order = new Order(customer, shipTo, shipFrom, orderAmount);

            var taxAmount = new Money(10F);
            taxCalculator.CalculateTaxesFor(order).Returns(taxAmount);

            var taxService = new TaxService(taxCalculator);

            var taxRate = await taxService.CalculateTaxesFor(order);
            taxRate.Should().Be(taxAmount);

            await taxCalculator.Received().CalculateTaxesFor(order);
        }   
    }
}