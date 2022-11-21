using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class ElectricalResistanceTest
{
    [Fact]
    public void OhmToString() => FormattingMatches(v => ElectricalResistance.Si<Ohm>(v), "Ω");
    [Fact]
    public void KiloOhmToString() => FormattingMatches(v => ElectricalResistance.Si<Kilo, Ohm>(v), "KΩ");
    [Fact]
    public void MilliOhmToString() => FormattingMatches(v => ElectricalResistance.Si<Milli, Ohm>(v), "mΩ");
    [Fact]
    public void OhmsLawInBaseUnits()
    {
        ElectricPotential volts = ElectricPotential.Si<Volt>(12);
        ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(3);
        ElectricalResistance expected = ElectricalResistance.Si<Ohm>(4);

        ElectricalResistance resistance = volts / ampere;

        resistance.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricPotential volts = ElectricPotential.Si<Milli, Volt>(12);
        ElectricCurrent ampere = ElectricCurrent.Si<Micro, Ampere>(3);
        ElectricalResistance expected = ElectricalResistance.Si<Kilo, Ohm>(4);

        ElectricalResistance resistance = volts / ampere;

        resistance.Matches(expected);
    }
}
