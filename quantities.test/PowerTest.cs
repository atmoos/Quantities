using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class PowerTest
{
    [Fact]
    public void WattToString() => FormattingMatches(v => Power.Of(v).Si<Watt>(), "W");
    [Fact]
    public void KiloWattToString() => FormattingMatches(v => Power.Of(v).Si<Kilo, Watt>(), "KW");
    [Fact]
    public void MicroWattToString() => FormattingMatches(v => Power.Of(v).Si<Micro, Watt>(), "μW");
    [Fact]
    public void PowerLawInBaseUnits()
    {
        ElectricPotential volts = ElectricPotential.Of(12).Si<Volt>();
        ElectricCurrent ampere = ElectricCurrent.Of(3).Si<Ampere>();
        Power expected = Power.Of(36).Si<Watt>();

        Power power = volts * ampere;

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricPotential volts = ElectricPotential.Of(70).Si<Kilo, Volt>();
        ElectricCurrent ampere = ElectricCurrent.Of(300).Si<Milli, Ampere>();
        Power expected = Power.Of(21).Si<Kilo, Watt>();

        Power power = ampere * volts;

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawSquarePotentialPerResistance()
    {
        ElectricPotential volts = ElectricPotential.Of(0.6).Si<Kilo, Volt>();
        ElectricalResistance ohm = ElectricalResistance.Of(3).Si<Kilo, Ohm>();
        Power expected = Power.Of(120).Si<Watt>();

        Power power = volts * (volts / ohm);

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawSquareCurrentTimesResistance()
    {
        ElectricCurrent ampere = ElectricCurrent.Of(8).Si<Kilo, Ampere>();
        ElectricalResistance ohm = ElectricalResistance.Of(2).Si<Milli, Ohm>();
        Power expected = Power.Of(128).Si<Kilo, Watt>();

        Power power = ohm * ampere * ampere;

        power.Matches(expected);
    }

    [Fact]
    public void DefinitionOfMetricHorsepower()
    {
        Power metricHorsePower = Power.Of(1).Metric<HorsePower>();
        Power expected = Power.Of(735.49875).Si<Watt>();

        Power watt = metricHorsePower.To.Si<Watt>();

        watt.Matches(expected);
    }
    [Fact]
    public void DefinitionOfImperialHorsepower()
    {
        Power imperialHorsePower = Power.Of(1).Imperial<Units.Imperial.Power.HorsePower>();
        Power expected = Power.Of(745.699871515585).Si<Watt>();

        Power watt = imperialHorsePower.To.Si<Watt>();

        watt.Matches(expected);
    }
    [Fact]
    public void ImperialHorsePowerIsNotEqualToMetricHorsePower()
    {
        Power metricHorsePower = Power.Of(1).Metric<HorsePower>();
        Power imperialHorsePower = Power.Of(1).Imperial<Units.Imperial.Power.HorsePower>();

        Assert.NotEqual(metricHorsePower, imperialHorsePower);
    }
    [Fact]
    public void ImperialAndMetricHorsePowerUseSameRepresentation()
    {
        Power metricHorsePower = Power.Of(1).Metric<HorsePower>();
        Power imperialHorsePower = Power.Of(1).Imperial<Units.Imperial.Power.HorsePower>();

        Assert.Equal(metricHorsePower.ToString(), imperialHorsePower.ToString());
    }
    [Fact]
    public void PowerFromEnergyDividedByTime()
    {
        Energy energy = Energy.Of(48).Metric<Giga, Watt, Hour>();
        Time time = Time.Of(200).Metric<Day>();
        Power expected = Power.Of(10).Si<Mega, Watt>();

        Power actual = energy / time;

        actual.Matches(expected);
    }
    [Theory]
    [MemberData(nameof(Powers))]
    public void PowerSupportsSerialization(Power power) => power.CanBeSerialized();

    public static IEnumerable<Object[]> Powers()
    {
        static IEnumerable<Power> Interesting()
        {
            yield return Power.Of(21).Si<Watt>();
            yield return Power.Of(342).Si<Milli, Watt>();
            yield return Power.Of(6).Si<Peta, Watt>();
            yield return Power.Of(-41).Metric<HorsePower>();
            yield return Power.Of(-11).Imperial<Units.Imperial.Power.HorsePower>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
