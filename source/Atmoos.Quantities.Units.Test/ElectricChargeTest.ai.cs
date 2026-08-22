using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class ElectricChargeTest
{
    [Fact]
    public void CoulombToString() => FormattingMatches(v => ElectricCharge.Of(v, Si<Coulomb>()), "C");

    [Fact]
    public void KiloCoulombToString() => FormattingMatches(v => ElectricCharge.Of(v, Si<Kilo, Coulomb>()), "kC");

    [Fact]
    public void MicroCoulombToString() => FormattingMatches(v => ElectricCharge.Of(v, Si<Micro, Coulomb>()), "μC");

    [Fact]
    public void CoulombRoundTripViaAmpereSecond()
    {
        ElectricCharge coulombs = ElectricCharge.Of(42, Si<Coulomb>());
        ElectricCharge expected = ElectricCharge.Of(42, Si<Ampere>().Times(Si<Second>()));

        ElectricCharge actual = coulombs.To(Si<Ampere>().Times(Si<Second>()));

        actual.Matches(expected);
    }

    [Fact]
    public void KiloCoulombToMilliCoulomb()
    {
        ElectricCharge kiloCoulombs = ElectricCharge.Of(1, Si<Kilo, Coulomb>());
        ElectricCharge expected = ElectricCharge.Of(1e6, Si<Milli, Coulomb>());

        ElectricCharge actual = kiloCoulombs.To(Si<Milli, Coulomb>());

        actual.Matches(expected);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void CoulombToAmpereHour()
    {
        ElectricCharge coulombs = ElectricCharge.Of(3600, Si<Coulomb>());
        ElectricCharge expected = ElectricCharge.Of(1, Si<Ampere>().Times(Metric<Hour>()));

        ElectricCharge actual = coulombs.To(Si<Ampere>().Times(Metric<Hour>()));

        actual.Matches(expected);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void AmpereHourToCoulomb()
    {
        ElectricCharge ampereHours = ElectricCharge.Of(2, Si<Ampere>().Times(Metric<Hour>()));
        ElectricCharge expected = ElectricCharge.Of(7200, Si<Coulomb>());

        ElectricCharge actual = ampereHours.To(Si<Coulomb>());

        actual.Matches(expected);
    }
}
