using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class EnergyTest
{
    [Fact]
    public void JouleToString() => FormattingMatches(v => Energy.Of(v).Linear.Si<Joule>(), "J");
    [Fact]
    public void TeraJouleToString() => FormattingMatches(v => Energy.Of(v).Linear.Si<Tera, Joule>(), "TJ");
    [Fact]
    public void FemtoJouleToString() => FormattingMatches(v => Energy.Of(v).Linear.Si<Femto, Joule>(), "fJ");
    [Fact]
    public void KiloWattHourToString() => FormattingMatches(v => Energy.Of(v).Si<Kilo, Watt>().Times.Metric<Hour>(), "KW h");
    [Fact]
    public void WattSecondToJoule()
    {
        var energy = Energy.Of(36).Si<Kilo, Watt>().Times.Si<Second>();
        var expected = Energy.Of(36).Linear.Si<Kilo, Joule>();

        var actual = energy.To.Linear.Si<Kilo, Joule>();

        actual.Matches(expected);
    }
    [Fact]
    public void JouleToKiloWattHour()
    {
        var energy = Energy.Of(1).Si<Kilo, Watt>().Times.Metric<Hour>();
        var expected = Energy.Of(3600).Linear.Si<Kilo, Joule>();

        var actual = energy.To.Linear.Si<Kilo, Joule>();

        actual.Matches(expected);
    }

    [Fact]
    public void WattHourToJoule()
    {
        var energy = Energy.Of(1).Si<Watt>().Times.Metric<Hour>();
        var expected = Energy.Of(3600).Linear.Si<Joule>();

        var actual = energy.To.Linear.Si<Joule>();

        actual.Matches(expected);
    }

    [Fact]
    public void DefinitionOfKiloWattHour()
    {
        var oneKiloWattHourInJoule = Energy.Of(3.6).Linear.Si<Mega, Joule>();
        var expected = Energy.Of(1).Si<Kilo, Watt>().Times.Metric<Hour>();

        var actual = oneKiloWattHourInJoule.To.Si<Kilo, Watt>().Times.Metric<Hour>();

        actual.Matches(expected);
    }

    [Fact]
    public void EnergyFromMultiplicationEqualsDirectCreation()
    {
        var time = Time.Of(2).Metric<Hour>();
        var power = Power.Of(3).Si<Kilo, Watt>();
        var expected = Energy.Of(6).Si<Kilo, Watt>().Times.Metric<Hour>();

        var actual = power * time;

        actual.Matches(expected);
    }
}