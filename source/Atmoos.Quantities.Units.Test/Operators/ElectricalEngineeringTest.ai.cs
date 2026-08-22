using Atmoos.Quantities;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Derived.Capacitance;
using Atmoos.Quantities.Units.Si.Derived.MagneticFlux;
using Atmoos.Quantities.Units.Si.Derived.MagneticFluxDensity;

namespace Atmoos.Quantities.Units.Test.Operators;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class ElectricalEngineeringTest
{
    [Fact]
    public void CurrentTimesTimeYieldsCharge()
    {
        ElectricCurrent current = ElectricCurrent.Of(3, Si<Ampere>());
        Time time = Time.Of(4, Si<Second>());
        ElectricCharge expected = ElectricCharge.Of(12, Si<Coulomb>());

        ElectricCharge actual = current * time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChargeDividedByPotentialYieldsCapacitance()
    {
        ElectricCharge charge = ElectricCharge.Of(12, Si<Coulomb>());
        ElectricPotential potential = ElectricPotential.Of(3, Si<Volt>());
        Capacitance expected = Capacitance.Of(4, Si<Farad>());

        Capacitance actual = charge / potential;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CapacitanceTimesPotentialYieldsCharge()
    {
        Capacitance capacitance = Capacitance.Of(4, Si<Farad>());
        ElectricPotential potential = ElectricPotential.Of(3, Si<Volt>());
        ElectricCharge expected = ElectricCharge.Of(12, Si<Coulomb>());

        ElectricCharge actual = capacitance * potential;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PotentialTimesTimeYieldsMagneticFlux()
    {
        ElectricPotential potential = ElectricPotential.Of(3, Si<Volt>());
        Time time = Time.Of(4, Si<Second>());
        MagneticFlux expected = MagneticFlux.Of(12, Si<Weber>());

        MagneticFlux actual = potential * time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MagneticFluxDividedByTimeYieldsPotential()
    {
        MagneticFlux flux = MagneticFlux.Of(12, Si<Weber>());
        Time time = Time.Of(4, Si<Second>());
        ElectricPotential expected = ElectricPotential.Of(3, Si<Volt>());

        ElectricPotential actual = flux / time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MagneticFluxDividedByAreaYieldsMagneticFluxDensity()
    {
        MagneticFlux flux = MagneticFlux.Of(12, Si<Weber>());
        Area area = Area.Of(3, Square(Si<Metre>()));
        MagneticFluxDensity expected = MagneticFluxDensity.Of(4, Si<Tesla>());

        MagneticFluxDensity actual = flux / area;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MagneticFluxDensityTimesAreaYieldsMagneticFlux()
    {
        MagneticFluxDensity density = MagneticFluxDensity.Of(4, Si<Tesla>());
        Area area = Area.Of(3, Square(Si<Metre>()));
        MagneticFlux expected = MagneticFlux.Of(12, Si<Weber>());

        MagneticFlux actual = density * area;

        Assert.Equal(expected, actual);
    }
}
