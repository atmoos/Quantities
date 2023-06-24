using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class ElectricalResistanceTest
{
    [Fact]
    public void OhmToString() => FormattingMatches(v => ElectricalResistance.Of(v).Si<Ohm>(), "Ω");
    [Fact]
    public void KiloOhmToString() => FormattingMatches(v => ElectricalResistance.Of(v).Si<Kilo, Ohm>(), "KΩ");
    [Fact]
    public void MilliOhmToString() => FormattingMatches(v => ElectricalResistance.Of(v).Si<Milli, Ohm>(), "mΩ");
    [Fact]
    public void OhmsLawInBaseUnits()
    {
        ElectricPotential volts = ElectricPotential.Of(12).Si<Volt>();
        ElectricCurrent ampere = ElectricCurrent.Of(3).Si<Ampere>();
        ElectricalResistance expected = ElectricalResistance.Of(4).Si<Ohm>();

        ElectricalResistance resistance = volts / ampere;

        resistance.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricPotential volts = ElectricPotential.Of(12).Si<Milli, Volt>();
        ElectricCurrent ampere = ElectricCurrent.Of(3).Si<Micro, Ampere>();
        ElectricalResistance expected = ElectricalResistance.Of(4).Si<Kilo, Ohm>();

        ElectricalResistance resistance = volts / ampere;

        resistance.Matches(expected);
    }
    [Theory]
    [MemberData(nameof(Resistances))]
    public void ElectricalResistanceSupportsSerialization(ElectricalResistance resistance) => resistance.SupportsSerialization();

    public static IEnumerable<Object[]> Resistances()
    {
        static IEnumerable<ElectricalResistance> Interesting()
        {
            yield return ElectricalResistance.Of(21).Si<Ohm>();
            yield return ElectricalResistance.Of(342).Si<Pico, Ohm>();
            yield return ElectricalResistance.Of(6).Si<Mega, Ohm>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
