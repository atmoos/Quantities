using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class EnergyTest
{
    [Fact]
    public void JouleToString() => FormattingMatches(v => Energy.Of(v).Si<Joule>(), "J");
    [Fact]
    public void TeraJouleToString() => FormattingMatches(v => Energy.Of(v).Si<Tera, Joule>(), "TJ");
    [Fact]
    public void FemtoJouleToString() => FormattingMatches(v => Energy.Of(v).Si<Femto, Joule>(), "fJ");
    [Fact]
    public void KiloWattHourToString() => FormattingMatches(v => Energy.Of(v).Metric<Kilo, Watt, Hour>(), "KW h");
    [Fact]
    public void WattSecondToJoule()
    {
        var energy = Energy.Of(36).Si<Kilo, Watt, Second>();
        var expected = Energy.Of(36).Si<Kilo, Joule>();

        var actual = energy.To.Si<Kilo, Joule>();

        actual.Matches(expected);
    }
    [Fact]
    public void JouleToKiloWattHour()
    {
        var energy = Energy.Of(1).Metric<Kilo, Watt, Hour>();
        var expected = Energy.Of(3600).Si<Kilo, Joule>();

        var actual = energy.To.Si<Kilo, Joule>();

        actual.Matches(expected);
    }

    [Fact]
    public void DefinitionOfKiloWattHour()
    {
        var oneKiloWattHourInJoule = Energy.Of(3.6).Si<Mega, Joule>();
        var expected = Energy.Of(1).Metric<Kilo, Watt, Hour>();

        var actual = oneKiloWattHourInJoule.To.Metric<Kilo, Watt, Hour>();

        actual.Matches(expected);
    }

    [Fact]
    public void EnergyFromMultiplicationEqualsDirectCreation()
    {
        var time = Time.Of(2).Metric<Hour>();
        var power = Power.Of(3).Si<Kilo, Watt>();
        var expected = Energy.Of(6).Metric<Kilo, Watt, Hour>();

        var actual = power * time;

        actual.Matches(expected);
    }
}