namespace TaxCalculator;

public struct Money
{
    public float Value { get; }

    public Money(float value)
    {
        Value = value;
    }
    
    public static explicit operator Money(float value)
    {
        return new Money(value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}