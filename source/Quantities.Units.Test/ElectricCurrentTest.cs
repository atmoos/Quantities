using Quantities.Units.Si.Derived;

namespace Quantities.Units.Test;

public sealed class ElectricCurrentTest
{
    [Fact]
    public void AmpereToString() => FormattingMatches(v => ElectricCurrent.Of(v, Si<Ampere>()), "A");
    [Fact]
    public void KiloAmpereToString() => FormattingMatches(v => ElectricCurrent.Of(v, Si<Kilo, Ampere>()), "kA");
    [Fact]
    public void MicroAmpereToString() => FormattingMatches(v => ElectricCurrent.Of(v, Si<Micro, Ampere>()), "μA");
    [Fact]
    public void OhmsLawInBaseUnits()
    {
        ElectricPotential volts = ElectricPotential.Of(12, Si<Volt>());
        ElectricalResistance ohm = ElectricalResistance.Of(3, Si<Ohm>());
        ElectricCurrent expected = ElectricCurrent.Of(4, Si<Ampere>());

        ElectricCurrent current = volts / ohm;

        current.Equal(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricPotential volts = ElectricPotential.Of(12, Si<Kilo, Volt>());
        ElectricalResistance ohm = ElectricalResistance.Of(3, Si<Mega, Ohm>());
        ElectricCurrent expected = ElectricCurrent.Of(4, Si<Milli, Ampere>());

        ElectricCurrent current = volts / ohm;

        current.Equal(expected);
    }
    [Fact]
    public void PowerLawInBaseUnits()
    {
        Power watts = Power.Of(1380, Si<Watt>());
        ElectricPotential volts = ElectricPotential.Of(230, Si<Volt>());
        ElectricCurrent expected = ElectricCurrent.Of(6, Si<Ampere>());

        ElectricCurrent current = watts / volts;

        current.Equal(expected);
    }
    [Fact]
    public void PowerLawInPrefixedUnits()
    {
        Power watts = Power.Of(9, Si<Mega, Watt>());
        ElectricPotential volts = ElectricPotential.Of(15, Si<Kilo, Volt>());
        ElectricCurrent expected = ElectricCurrent.Of(600, Si<Ampere>());

        ElectricCurrent current = watts / volts;

        current.Equal(expected);
    }
}
