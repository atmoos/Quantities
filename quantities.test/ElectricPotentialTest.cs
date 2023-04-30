using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class ElectricPotentialTest
{
    [Fact]
    public void VoltToString() => FormattingMatches(v => ElectricPotential.Of(v).Si<Volt>(), "V");
    [Fact]
    public void MegaVoltToString() => FormattingMatches(v => ElectricPotential.Of(v).Si<Mega, Volt>(), "MV");
    [Fact]
    public void MilliVoltToString() => FormattingMatches(v => ElectricPotential.Of(v).Si<Milli, Volt>(), "mV");
    [Fact]
    public void OhmsLawInBaseUnits()
    {
        ElectricalResistance ohm = ElectricalResistance.Si<Ohm>(7);
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(3);
        ElectricPotential expected = ElectricPotential.Of(21).Si<Volt>();

        ElectricPotential potential = ohm * ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricalResistance ohm = ElectricalResistance.Si<Kilo, Ohm>(7);
        ElectricCurrent ampere = ElectricCurrent.Si<Micro, Ampere>(3);
        ElectricPotential expected = ElectricPotential.Of(21).Si<Milli, Volt>();

        ElectricPotential potential = ohm * ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnitsWithInBetweenVirtualPrefix()
    {
        ElectricalResistance ohm = ElectricalResistance.Si<Deca, Ohm>(7);
        ElectricCurrent ampere = ElectricCurrent.Si<Kilo, Ampere>(3);

        // 7e1Ω * 3e3A = 21e4V, since e4 is no prefix: expect 210e3 V
        ElectricPotential expected = ElectricPotential.Of(210).Si<Kilo, Volt>();

        ElectricPotential potential = ohm * ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void PowerLawInBaseUnits()
    {
        Power watts = Power.Si<Watt>(1380);
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(6);
        ElectricPotential expected = ElectricPotential.Of(230).Si<Volt>();

        ElectricPotential potential = watts / ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void PowerLawInPrefixedUnits()
    {
        Power watts = Power.Si<Mega, Watt>(9);
        _ = ElectricPotential.Of(15).Si<Kilo, Volt>();
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(600);

        // ToDo: Implement rounding based on value!
        ElectricPotential expected = ElectricPotential.Of(15).Si<Kilo, Volt>();

        ElectricPotential potential = watts / ampere;

        potential.Matches(expected);
    }
}
