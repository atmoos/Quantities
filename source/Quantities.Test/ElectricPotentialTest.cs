using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class ElectricPotentialTest
{
    [Fact]
    public void VoltToString() => FormattingMatches(v => ElectricPotential.Of(v, Si<Volt>()), "V");
    [Fact]
    public void MegaVoltToString() => FormattingMatches(v => ElectricPotential.Of(v, Si<Mega, Volt>()), "MV");
    [Fact]
    public void MilliVoltToString() => FormattingMatches(v => ElectricPotential.Of(v, Si<Milli, Volt>()), "mV");
    [Fact]
    public void OhmsLawInBaseUnits()
    {
        ElectricalResistance ohm = ElectricalResistance.Of(7, Si<Ohm>());
        ElectricCurrent ampere = ElectricCurrent.Of(3, Si<Ampere>());
        ElectricPotential expected = ElectricPotential.Of(21, Si<Volt>());

        ElectricPotential potential = ohm * ampere;

        potential.Equals(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricalResistance ohm = ElectricalResistance.Of(7, Si<Kilo, Ohm>());
        ElectricCurrent ampere = ElectricCurrent.Of(3, Si<Micro, Ampere>());
        ElectricPotential expected = ElectricPotential.Of(21, Si<Milli, Volt>());

        ElectricPotential potential = ohm * ampere;

        potential.Equals(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnitsWithInBetweenVirtualPrefix()
    {
        ElectricalResistance ohm = ElectricalResistance.Of(7, Si<Deca, Ohm>());
        ElectricCurrent ampere = ElectricCurrent.Of(3, Si<Kilo, Ampere>());

        // 7e1Ω * 3e3A = 21e4V, since e4 is no prefix: expect 210e3 V
        ElectricPotential expected = ElectricPotential.Of(210, Si<Kilo, Volt>());

        ElectricPotential potential = ohm * ampere;

        potential.Equals(expected);
    }
    [Fact]
    public void PowerLawInBaseUnits()
    {
        Power watts = Power.Of(1380).Si<Watt>();
        ElectricCurrent ampere = ElectricCurrent.Of(6, Si<Ampere>());
        ElectricPotential expected = ElectricPotential.Of(230, Si<Volt>());

        ElectricPotential potential = watts / ampere;

        potential.Equals(expected);
    }
    [Fact]
    public void PowerLawInPrefixedUnits()
    {
        Power watts = Power.Of(9).Si<Mega, Watt>();
        _ = ElectricPotential.Of(15, Si<Kilo, Volt>());
        ElectricCurrent ampere = ElectricCurrent.Of(600, Si<Ampere>());

        // ToDo: Implement rounding based on value!
        ElectricPotential expected = ElectricPotential.Of(15, Si<Kilo, Volt>());

        ElectricPotential potential = watts / ampere;

        potential.Equals(expected);
    }
}
