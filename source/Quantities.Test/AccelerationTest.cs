using Quantities.Units.NonStandard.Acceleration;
using Quantities.Units.NonStandard.Length;
using Quantities.Units.NonStandard.Velocity;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public class AccelerationTest
{
    [Fact]
    public void MetrePerSquareSecondToString() => FormattingMatches(v => Acceleration.Of(v, Si<Metre>().Per(Square(Si<Second>()))), "m/s²");

    [Fact]
    public void KnotPerHourIsSameAsNauticalMilePerSquareHour()
    {
        Velocity knots = Velocity.Of(6, NonStandard<Knot>());
        Time hours = Time.Of(2, Metric<Hour>());
        Acceleration expected = Acceleration.Of(3, NonStandard<NauticalMile>().Per(Square(Metric<Hour>())));

        Acceleration actual = knots / hours;

        actual.Equal(expected);
    }

    [Fact]
    public void OneStandardGravityInMetersPerSecondSquared()
    {
        var standardGravity = Acceleration.Of(1, NonStandard<StandardGravity>());
        var expected = Acceleration.Of(9.80665, Si<Metre>().Per(Square(Si<Second>())));

        var actual = standardGravity.To(Si<Metre>().Per(Square(Si<Second>())));

        actual.Matches(expected);
    }
}
