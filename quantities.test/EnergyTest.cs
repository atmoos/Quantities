using Quantities.Units.Si.Accepted;
using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class EnergyTest
{
    [Fact]
    public void JouleToString() => FormattingMatches(v => Energy.Si<Joule>(v), "J");
    [Fact]
    public void TeraJouleToString() => FormattingMatches(v => Energy.Si<Tera, Joule>(v), "TJ");
    [Fact]
    public void FemtoJouleToString() => FormattingMatches(v => Energy.Si<Femto, Joule>(v), "fJ");
    [Fact]
    public void KiloWattHourToString() => FormattingMatches(v => Energy.SiAccepted<Kilo, Watt, Hour>(v), "KW h");
    [Fact]
    public void WattSecondToJoule()
    {
        var energy = Energy.Si<Kilo, Watt, Second>(36);
        var expected = Energy.Si<Kilo, Joule>(36);

        var actual = energy.To<Kilo, Joule>();

        actual.Matches(expected);
    }
    [Fact]
    public void JouleToKiloWattHour()
    {
        var energy = Energy.SiAccepted<Kilo, Watt, Hour>(1);
        var expected = Energy.Si<Kilo, Joule>(3600);

        var actual = energy.To<Kilo, Joule>();

        actual.Matches(expected);
    }

    [Fact]
    public void EnergyFromMultiplicationEqualsDirectCreation()
    {
        var time = Time.In<Hour>(2);
        var power = Power.Si<Kilo, Watt>(3);
        var expected = Energy.SiAccepted<Kilo, Watt, Hour>(6);

        var actual = power * time;

        actual.Matches(expected);
    }
}