namespace TaxCalculator;

public interface ITaxCalculator
{
    Task<Percentage?> GetTaxRateFor(string zip, CancellationToken cancellationToken = default);
    Task<Money?> CalculateTaxesFor(Order order, CancellationToken cancellationToken = default);
}