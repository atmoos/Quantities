using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class PowerTest
{
    [Fact]
    public void WattToString() => FormattingMatches(v => Power.Si<Watt>(v), "W");
    [Fact]
    public void KiloWattToString() => FormattingMatches(v => Power.Si<Kilo, Watt>(v), "KW");
    [Fact]
    public void MicroWattToString() => FormattingMatches(v => Power.Si<Micro, Watt>(v), "μW");
    [Fact]
    public void PowerLawInBaseUnits()
    {
        ElectricPotential volts = ElectricPotential.Si<Volt>(12);
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(3);
        Power expected = Power.Si<Watt>(36);

        Power power = volts * ampere;

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricPotential volts = ElectricPotential.Si<Kilo, Volt>(70);
        ElectricCurrent ampere = ElectricCurrent.Si<Milli, Ampere>(300);
        Power expected = Power.Si<Kilo, Watt>(21);

        Power power = ampere * volts;

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawSquarePotentialPerResistance()
    {
        ElectricPotential volts = ElectricPotential.Si<Kilo, Volt>(0.6);
        ElectricalResistance ohm = ElectricalResistance.Si<Kilo, Ohm>(3);
        Power expected = Power.Si<Watt>(120);

        Power power = volts * (volts / ohm);

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawSquareCurrentTimesResistance()
    {
        ElectricCurrent ampere = ElectricCurrent.Si<Kilo, Ampere>(8);
        ElectricalResistance ohm = ElectricalResistance.Si<Milli, Ohm>(2);
        Power expected = Power.Si<Kilo, Watt>(128);

        Power power = ohm * ampere * ampere;

        power.Matches(expected);
    }
}
