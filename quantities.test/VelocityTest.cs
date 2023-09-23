using Quantities.units.NonStandard.Length;
using Quantities.Units.NonStandard.Velocity;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class VelocityTest
{
    [Fact]
    public void Create()
    {
        Velocity speed = Velocity.Of(5).Si<Metre>().Per.Si<Second>();
        Assert.Equal("5 m/s", speed.ToString());
    }
    [Fact]
    public void KilometrePerHourToMetrePerSecond()
    {
        Velocity speed = Velocity.Of(36).Si<Kilo, Metre>().Per.Metric<Hour>();
        Velocity expected = Velocity.Of(10).Si<Metre>().Per.Si<Second>();

        Velocity actual = speed.To.Si<Metre>().Per.Si<Second>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MetrePerSecondToKilometrePerHour()
    {
        Velocity speed = Velocity.Of(2).Si<Metre>().Per.Si<Second>();
        Velocity expected = Velocity.Of(7.2).Si<Kilo, Metre>().Per.Metric<Hour>();

        Velocity actual = speed.To.Si<Kilo, Metre>().Per.Metric<Hour>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void TrivialTransform()
    {
        Velocity speed = Velocity.Of(200).Si<Metre>().Per.Si<Second>();
        Velocity expected = Velocity.Of(0.2).Si<Metre>().Per.Si<Milli, Second>();

        Velocity actual = speed.To.Si<Metre>().Per.Si<Milli, Second>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Transform()
    {
        Velocity speed = Velocity.Of(4).Si<Centi, Metre>().Per.Si<Second>();
        Velocity expected = Velocity.Of(40).Si<Milli, Metre>().Per.Si<Second>();

        Velocity actual = speed.To.Si<Milli, Metre>().Per.Si<Second>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MilesPerHourToKilometresPerHour()
    {
        Velocity speed = Velocity.Of(4).Imperial<Mile>().Per.Metric<Hour>();
        Velocity expected = Velocity.Of(4 * 1.609344).Si<Kilo, Metre>().Per.Metric<Hour>();

        Velocity actual = speed.To.Si<Kilo, Metre>().Per.Metric<Hour>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MetresPerSecondToMilesPerHourTo()
    {
        Velocity speed = Velocity.Of(0.44704).Si<Metre>().Per.Si<Second>();
        Velocity expected = Velocity.Of(1).Imperial<Mile>().Per.Metric<Hour>();

        Velocity actual = speed.To.Imperial<Mile>().Per.Metric<Hour>();

        actual.Matches(expected);
    }


    [Fact]
    public void VelocityFromDivisionOfLengthWithTime_SiUnits()
    {
        Length length = Length.Of(12).Si<Metre>();
        Time time = Time.Of(2).Si<Second>();
        Velocity expected = Velocity.Of(6).Si<Metre>().Per.Si<Second>();

        Velocity actual = length / time;

        actual.Matches(expected);
    }
    [Fact]
    public void VelocityFromDivisionOfLengthWithTime_ImperialUnits()
    {
        Length length = Length.Of(18).Imperial<Mile>();
        Time time = Time.Of(2).Metric<Hour>();
        Velocity expected = Velocity.Of(9).Imperial<Mile>().Per.Metric<Hour>();

        Velocity actual = length / time;

        actual.Matches(expected);
    }


    [Fact]
    public void VelocityFromDivision_Equal_DirectVelocity()
    {
        Length length = Length.Of(18).Imperial<Mile>();
        Time time = Time.Of(2).Metric<Hour>();
        Velocity expected = Velocity.Of(9).Imperial<Mile>().Per.Metric<Hour>();

        Velocity actual = length / time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NauticalMilesPerHourToKnots()
    {
        const Double commonValue = 62;
        Velocity expectedKnots = Velocity.Of(commonValue).Linear.NonStandard<Knot>();
        Velocity nauticalMiles = Velocity.Of(commonValue).NonStandard<NauticalMile>().Per.Metric<Hour>();

        Velocity actualKnots = nauticalMiles.To.Linear.NonStandard<Knot>();

        Assert.Equal(expectedKnots, actualKnots);
    }

    [Fact]
    public void KnotsToKilometrePerHour()
    {
        const Double knotsValue = 6;
        const Double knotsInKmPerH = 1.852;
        Velocity knots = Velocity.Of(knotsValue).Linear.NonStandard<Knot>();
        Velocity kiloMetresPerHour = Velocity.Of(knotsValue * knotsInKmPerH).Si<Kilo, Metre>().Per.Metric<Hour>();

        Velocity actualKiloMetresPerHour = knots.To.Si<Kilo, Metre>().Per.Metric<Hour>();

        Assert.Equal(kiloMetresPerHour, actualKiloMetresPerHour);
    }
}
