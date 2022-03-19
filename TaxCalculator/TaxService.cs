namespace TaxCalculator;

public class TaxService
{
    private readonly ITaxCalculator _taxCalculator;

    public TaxService(ITaxCalculator taxCalculator)
    {
        _taxCalculator = taxCalculator;
    }

    public async Task<Percentage?> GetTaxRateFor(string zip, CancellationToken cancellationToken = default)
    {
        return await _taxCalculator.GetTaxRateFor(zip, cancellationToken);
    }

    public async Task<Money?> CalculateTaxesFor(Order order, CancellationToken cancellationToken = default)
    {
        return await _taxCalculator.CalculateTaxesFor(order, cancellationToken);
    }
}