using Atmoos.Quantities;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test.Operators;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class FluidDynamicsTest
{
    [Fact]
    public void MassDividedByVolumeYieldsDensity()
    {
        Mass mass = Mass.Of(12, Si<Kilogram>());
        Volume volume = Volume.Of(3, Cubic(Si<Metre>()));
        Density expected = Density.Of(4, Si<Kilogram>().Per(Cubic(Si<Metre>())));

        Density actual = mass / volume;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DensityTimesVolumeYieldsMass()
    {
        Density density = Density.Of(4, Si<Kilogram>().Per(Cubic(Si<Metre>())));
        Volume volume = Volume.Of(3, Cubic(Si<Metre>()));
        Mass expected = Mass.Of(12, Si<Kilogram>());

        Mass actual = density * volume;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MassDividedByTimeYieldsMassFlowRate()
    {
        Mass mass = Mass.Of(12, Si<Kilogram>());
        Time time = Time.Of(3, Si<Second>());
        MassFlowRate expected = MassFlowRate.Of(4, Si<Kilogram>().Per(Si<Second>()));

        MassFlowRate actual = mass / time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MassFlowRateTimesTimeYieldsMass()
    {
        MassFlowRate flowRate = MassFlowRate.Of(4, Si<Kilogram>().Per(Si<Second>()));
        Time time = Time.Of(3, Si<Second>());
        Mass expected = Mass.Of(12, Si<Kilogram>());

        Mass actual = flowRate * time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void VolumeDividedByTimeYieldsVolumetricFlowRate()
    {
        Volume volume = Volume.Of(12, Cubic(Si<Metre>()));
        Time time = Time.Of(3, Si<Second>());
        VolumetricFlowRate expected = VolumetricFlowRate.Of(4, Cubic(Si<Metre>()).Per(Si<Second>()));

        VolumetricFlowRate actual = volume / time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void VolumetricFlowRateTimesTimeYieldsVolume()
    {
        VolumetricFlowRate flowRate = VolumetricFlowRate.Of(4, Cubic(Si<Metre>()).Per(Si<Second>()));
        Time time = Time.Of(3, Si<Second>());
        Volume expected = Volume.Of(12, Cubic(Si<Metre>()));

        Volume actual = flowRate * time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DensityTimesVolumetricFlowRateYieldsMassFlowRate()
    {
        Density density = Density.Of(4, Si<Kilogram>().Per(Cubic(Si<Metre>())));
        VolumetricFlowRate flowRate = VolumetricFlowRate.Of(3, Cubic(Si<Metre>()).Per(Si<Second>()));
        MassFlowRate expected = MassFlowRate.Of(12, Si<Kilogram>().Per(Si<Second>()));

        MassFlowRate actual = density * flowRate;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MassFlowRateDividedByDensityYieldsVolumetricFlowRate()
    {
        MassFlowRate flowRate = MassFlowRate.Of(12, Si<Kilogram>().Per(Si<Second>()));
        Density density = Density.Of(4, Si<Kilogram>().Per(Cubic(Si<Metre>())));
        VolumetricFlowRate expected = VolumetricFlowRate.Of(3, Cubic(Si<Metre>()).Per(Si<Second>()));

        VolumetricFlowRate actual = flowRate / density;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PressureTimesTimeYieldsDynamicViscosity()
    {
        Pressure pressure = Pressure.Of(4, Si<Pascal>());
        Time time = Time.Of(3, Si<Second>());
        DynamicViscosity expected = DynamicViscosity.Of(12, Si<Pascal>().Times(Si<Second>()));

        DynamicViscosity actual = pressure * time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DynamicViscosityDividedByTimeYieldsPressure()
    {
        DynamicViscosity viscosity = DynamicViscosity.Of(12, Si<Pascal>().Times(Si<Second>()));
        Time time = Time.Of(3, Si<Second>());
        Pressure expected = Pressure.Of(4, Si<Pascal>());

        Pressure actual = viscosity / time;

        Assert.Equal(expected, actual);
    }
}
