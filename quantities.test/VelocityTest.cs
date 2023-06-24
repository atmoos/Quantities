using Quantities.Units.NonStandard.Length;
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
    public void SiVelocityCanBeDeserialized()
    {
        Velocity.Of(-4.2).Si<Kilo, Metre>().Per.Metric<Hour>().CanBeSerialized();
    }

    [Fact]
    public void ImperialVelocityCanBeDeserialized()
    {
        Velocity.Of(2.4).Imperial<Mile>().Per.Metric<Hour>().CanBeSerialized();
    }
    [Theory]
    [MemberData(nameof(Velocities))]
    public void VelocitySupportsSerialization(Velocity velocity) => velocity.CanBeSerialized();

    public static IEnumerable<Object[]> Velocities()
    {
        static IEnumerable<Velocity> Interesting()
        {
            yield return Velocity.Of(21).Si<Metre>().Per.Si<Second>();
            yield return Velocity.Of(342).Si<Kilo, Metre>().Per.Metric<Hour>();
            yield return Velocity.Of(6).Imperial<Mile>().Per.Metric<Hour>();
            yield return Velocity.Of(-41).Imperial<Foot>().Per.Si<Second>();
            yield return Velocity.Of(1.21).Metric<Ångström>().Per.Si<Micro, Second>();
            yield return Velocity.Of(0.125).Metric<AstronomicalUnit>().Per.Metric<Minute>();
            yield return Velocity.Of(0.000292002383).NonStandard<LightYear>().Per.Metric<Week>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
