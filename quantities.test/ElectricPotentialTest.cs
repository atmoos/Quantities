using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class ElectricPotentialTest
{
    [Fact]
    public void VoltToString() => FormattingMatches(v => ElectricPotential.Si<Volt>(v), "V");
    [Fact]
    public void MegaVoltToString() => FormattingMatches(v => ElectricPotential.Si<Mega, Volt>(v), "MV");
    [Fact]
    public void MilliVoltToString() => FormattingMatches(v => ElectricPotential.Si<Milli, Volt>(v), "mV");
    [Fact]
    public void OhmsLawInBaseUnits()
    {
        ElectricalResistance ohm = ElectricalResistance.Si<Ohm>(7);
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(3);
        ElectricPotential expected = ElectricPotential.Si<Volt>(21);

        ElectricPotential potential = ohm * ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricalResistance ohm = ElectricalResistance.Si<Kilo, Ohm>(7);
        ElectricCurrent ampere = ElectricCurrent.Si<Micro, Ampere>(3);
        ElectricPotential expected = ElectricPotential.Si<Milli, Volt>(21);

        ElectricPotential potential = ohm * ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnitsWithInBetweenVirtualPrefix()
    {
        ElectricalResistance ohm = ElectricalResistance.Si<Deca, Ohm>(7);
        ElectricCurrent ampere = ElectricCurrent.Si<Kilo, Ampere>(3);

        // 7e1Ω * 3e3A = 21e4V, since e4 is no prefix: expect 210e3 V
        ElectricPotential expected = ElectricPotential.Si<Kilo, Volt>(210);

        ElectricPotential potential = ohm * ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void PowerLawInBaseUnits()
    {
        Power watts = Power.Si<Watt>(1380);
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(6);
        ElectricPotential expected = ElectricPotential.Si<Volt>(230);

        ElectricPotential potential = watts / ampere;

        potential.Matches(expected);
    }
    [Fact]
    public void PowerLawInPrefixedUnits()
    {
        Power watts = Power.Si<Mega, Watt>(9);
        _ = ElectricPotential.Si<Kilo, Volt>(15);
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(600);

        // ToDo: Implement rounding based on value!
        ElectricPotential expected = ElectricPotential.Si<Kilo, Volt>(15);

        ElectricPotential potential = watts / ampere;

        potential.Matches(expected);
    }
}
