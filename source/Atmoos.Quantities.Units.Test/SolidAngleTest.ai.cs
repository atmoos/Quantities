using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class SolidAngleTest
{
    [Fact]
    public void SteradianToString() => FormattingMatches(v => SolidAngle.Of(v, Si<Steradian>()), "sr");

    [Fact]
    public void SteradianConvertsToSteradian()
    {
        SolidAngle solidAngle = SolidAngle.Of(2.5, Si<Steradian>());
        SolidAngle expected = SolidAngle.Of(2.5, Si<Steradian>());

        SolidAngle actual = solidAngle.To(Si<Steradian>());

        actual.Matches(expected);
    }

    [Fact]
    public void EqualSteradianSolidAnglesHaveEqualHashCode()
    {
        SolidAngle left = SolidAngle.Of(1, Si<Steradian>());
        SolidAngle right = SolidAngle.Of(1, Si<Steradian>());

        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }
}
