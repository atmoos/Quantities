using Atmoos.Quantities.Units.NonStandard.Length;
using Atmoos.Quantities.Units.NonStandard.Velocity;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

public sealed class AccelerationTest
{
    [Fact]
    public void SiUnitsToString() => FormattingMatches(v => Acceleration.Of(v, Si<Metre>().Per(Square(in Si<Second>()))), "m/s²");
    [Fact]
    public void ImperialUnitsToString() => FormattingMatches(v => Acceleration.Of(v, Imperial<Mile>().Per(Square(in Metric<Day>()))), "mi/d²");
    [Fact]
    public void Create()
    {
        Acceleration speed = Acceleration.Of(9.81, Si<Metre>().Per(Square(Si<Second>())));
        Assert.Equal("9.81 m/s²", speed.ToString());
    }
    [Fact]
    public void AccelerationFromDivisionOfVelocityWithTime()
    {
        Velocity velocity = Velocity.Of(18, Si<Metre>().Per(in Si<Second>()));
        Time time = Time.Of(2, Si<Second>());
        Acceleration expected = Acceleration.Of(9, Si<Metre>().Per(Square(in Si<Second>())));

        Acceleration actual = velocity / time;

        actual.Matches(expected);
    }
    [Fact]
    public void AccelerationFromDivisionOfLengthWithSquareTime()
    {
        Length length = Length.Of(12, Si<Metre>());
        Time time = Time.Of(2, Si<Second>());
        Acceleration expected = Acceleration.Of(3, Si<Metre>().Per(Square(Si<Second>())));

        Acceleration actual = length / time / time;

        actual.Matches(expected);
    }


    [Fact]
    public void VelocityFromDivision_Equal_DirectVelocity()
    {
        Length length = Length.Of(18, Imperial<Mile>());
        Time time = Time.Of(2, Metric<Hour>());
        Velocity expected = Velocity.Of(9, Imperial<Mile>().Per(Metric<Hour>()));

        Velocity actual = length / time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NauticalMilesPerHourToKnots()
    {
        const Double commonValue = 62;
        Velocity expectedKnots = Velocity.Of(commonValue, NonStandard<Knot>());
        Velocity nauticalMiles = Velocity.Of(commonValue, NonStandard<NauticalMile>().Per(Metric<Hour>()));

        Velocity actualKnots = nauticalMiles.To(NonStandard<Knot>());

        Assert.Equal(expectedKnots, actualKnots);
    }

    [Fact]
    public void KnotsToKilometrePerHour()
    {
        const Double knotsValue = 6;
        const Double knotsInKmPerH = 1.852;
        Velocity knots = Velocity.Of(knotsValue, NonStandard<Knot>());
        Velocity kiloMetresPerHour = Velocity.Of(knotsValue * knotsInKmPerH, Si<Kilo, Metre>().Per(Metric<Hour>()));

        Velocity actualKiloMetresPerHour = knots.To(Si<Kilo, Metre>().Per(Metric<Hour>()));

        Assert.Equal(kiloMetresPerHour, actualKiloMetresPerHour);
    }
}
