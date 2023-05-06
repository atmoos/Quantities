﻿using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

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
        ElectricPotential volts = ElectricPotential.Of(12).Si<Volt>();
        ElectricCurrent ampere = ElectricCurrent.Of(3).Si<Ampere>();
        Power expected = Power.Si<Watt>(36);

        Power power = volts * ampere;

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawInPrefixedUnits()
    {
        ElectricPotential volts = ElectricPotential.Of(70).Si<Kilo, Volt>();
        ElectricCurrent ampere = ElectricCurrent.Of(300).Si<Milli, Ampere>();
        Power expected = Power.Si<Kilo, Watt>(21);

        Power power = ampere * volts;

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawSquarePotentialPerResistance()
    {
        ElectricPotential volts = ElectricPotential.Of(0.6).Si<Kilo, Volt>();
        ElectricalResistance ohm = ElectricalResistance.Of(3).Si<Kilo, Ohm>();
        Power expected = Power.Si<Watt>(120);

        Power power = volts * (volts / ohm);

        power.Matches(expected);
    }
    [Fact]
    public void OhmsLawSquareCurrentTimesResistance()
    {
        ElectricCurrent ampere = ElectricCurrent.Of(8).Si<Kilo, Ampere>();
        ElectricalResistance ohm = ElectricalResistance.Of(2).Si<Milli, Ohm>();
        Power expected = Power.Si<Kilo, Watt>(128);

        Power power = ohm * ampere * ampere;

        power.Matches(expected);
    }

    [Fact]
    public void DefinitionOfMetricHorsepower()
    {
        Power metricHorsePower = Power.Metric<HorsePower>(1);
        Power expected = Power.Si<Watt>(735.49875);

        Power watt = metricHorsePower.To<Watt>();

        watt.Matches(expected);
    }
    [Fact]
    public void DefinitionOfImperialHorsepower()
    {
        Power imperialHorsePower = Power.Imperial<Units.Imperial.Power.HorsePower>(1);
        Power expected = Power.Si<Watt>(745.699871515585);

        Power watt = imperialHorsePower.To<Watt>();

        watt.Matches(expected);
    }
    [Fact]
    public void ImperialHorsePowerIsNotEqualToMetricHorsePower()
    {
        Power metricHorsePower = Power.Metric<HorsePower>(1);
        Power imperialHorsePower = Power.Imperial<Units.Imperial.Power.HorsePower>(1);

        Assert.NotEqual(metricHorsePower, imperialHorsePower);
    }
    [Fact]
    public void ImperialAndMetricHorsePowerUseSameRepresentation()
    {
        Power metricHorsePower = Power.Metric<HorsePower>(1);
        Power imperialHorsePower = Power.Imperial<Units.Imperial.Power.HorsePower>(1);

        Assert.Equal(metricHorsePower.ToString(), imperialHorsePower.ToString());
    }
    [Fact]
    public void PowerFromEnergyDividedByTime()
    {
        Energy energy = Energy.Metric<Giga, Watt, Hour>(48);
        Time time = Time.Of(200).Metric<Day>();
        Power expected = Power.Si<Mega, Watt>(10);

        Power actual = energy / time;

        actual.Matches(expected);
    }
}
