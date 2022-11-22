using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class ElectricCurrentTest
{
    [Fact]
    public void AmpereToString() => FormattingMatches(v => ElectricCurrent.Si<Ampere>(v), "A");
    [Fact]
    public void KiloAmpereToString() => FormattingMatches(v => ElectricCurrent.Si<Kilo, Ampere>(v), "KA");
    [Fact]
    public void MicroAmpereToString() => FormattingMatches(v => ElectricCurrent.Si<Micro, Ampere>(v), "μA");
    [Fact]
    public void OhmsLawInBaseUnits()
    {
        ElectricPotential volts = ElectricPotential.Si<Volt>(12);
        ElectricalResistance ohm = ElectricalResistance.Si<Ohm>(3);
        ElectricCurrent expected = ElectricCurrent.Si<Ampere>(4);

        ElectricCurrent current = volts / ohm;

        current.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricPotential volts = ElectricPotential.Si<Kilo, Volt>(12);
        ElectricalResistance ohm = ElectricalResistance.Si<Mega, Ohm>(3);
        ElectricCurrent expected = ElectricCurrent.Si<Milli, Ampere>(4);

        ElectricCurrent current = volts / ohm;

        current.Matches(expected);
    }
    [Fact]
    public void PowerLawInBaseUnits()
    {
        Power watts = Power.Si<Watt>(1380);
        ElectricPotential volts = ElectricPotential.Si<Volt>(230);
        ElectricCurrent expected = ElectricCurrent.Si<Ampere>(6);

        ElectricCurrent current = watts / volts;

        current.Matches(expected);
    }
    [Fact]
    public void PowerLawInPrefixedUnits()
    {
        Power watts = Power.Si<Mega, Watt>(9);
        ElectricPotential volts = ElectricPotential.Si<Kilo, Volt>(15);
        ElectricCurrent expected = ElectricCurrent.Si<Ampere>(600);

        ElectricCurrent current = watts / volts;

        current.Matches(expected);
    }
}
