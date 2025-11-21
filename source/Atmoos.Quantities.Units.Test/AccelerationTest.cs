using System.ComponentModel;
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
    [Description("Knots can be divided by hours resolving issue #89.")]
    public void AccelerationFromKnots()
    {
        Time time = Time.Of(2, Metric<Hour>());
        Velocity velocityInKnots = Velocity.Of(10, NonStandard<Knot>());
        Acceleration expected = Acceleration.Of(5, NonStandard<NauticalMile>().Per(Square(in Metric<Hour>())));

        Acceleration actual = velocityInKnots / time;

        Assert.Equal(expected, actual);
        Assert.Equal("5 kn/h", actual.ToString());
    }
}
