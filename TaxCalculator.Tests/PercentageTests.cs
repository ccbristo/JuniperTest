using FluentAssertions;
using Xunit;

namespace TaxCalculator.Tests;

public class PercentageTests
{
    [Theory]
    [InlineData(0F, 0F, true)]
    [InlineData(0F, 1F, false)]
    [InlineData(1F, -1F, false)]
    [InlineData(-1F, 1F, false)]
    [InlineData(1F, 0F, false)]
    [InlineData(float.PositiveInfinity, float.PositiveInfinity, true)]
    [InlineData(float.NegativeInfinity, float.NegativeInfinity, true)]
    [InlineData(float.NegativeInfinity, float.PositiveInfinity, false)]
    [InlineData(float.PositiveInfinity, float.NegativeInfinity, false)]
    [InlineData(.00006F, .00005F, true)] // exactly on the tolerance
    [InlineData(.00005F, .00006F, true)] // exactly on the tolerance
    [InlineData(.00006F, .000049F, false)] // just outside of the tolerance
    [InlineData(.000049F, .00006F, false)] // just outside of the tolerance
    [InlineData(.03125F, .03126F, true)]
    [InlineData(.03125F, .03127F, false)]
    [InlineData(.03125F, .031252F, true)]
    [InlineData(float.NaN, float.NaN, false)]
    [InlineData(float.NaN, 0, false)]
    [InlineData(0, float.NaN, false)]
    [InlineData(float.NaN, float.PositiveInfinity, false)]
    [InlineData(float.NaN, float.NegativeInfinity, false)]
    public void EquivalencyCheck(float a, float b, bool shouldBeEqual)
    {
        var p1 = new Percentage(a);
        var p2 = new Percentage(b);

        if (shouldBeEqual)
            p1.Should().BeEquivalentTo(p2);
        else
            p1.Should().NotBeEquivalentTo(p2);
    }
}