using System.Diagnostics;

namespace TaxCalculator;

[DebuggerDisplay("{Value}")]
public struct Percentage : IFormattable
{
    public float Value { get; }

    public Percentage(float value)
    {
        Value = value;
    }

    public static explicit operator Percentage(float value)
    {
        return new Percentage(value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Percentage other)
            return false;
        
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        // this comparison is necessary to check for +/- infinities
        // otherwise, you'll attempt things like -inf - +inf which results in NaN
        // fortunately, if either value is NaN, == will return false
        if (this.Value == other.Value)
            return true;
        
        // since we expect all Percentages in our system to have four or fewer decimal places
        // we'll assume that values within .00001 (1 / 10,0000th) of each other are equal
        const float tolerance = .00001f;
        return Math.Abs(this.Value - other.Value) <= tolerance;
    }

    public override string ToString() => ToString(null, null);

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return Value.ToString(format, formatProvider);
    }
}