using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class KinematicsTest
{
    [Fact]
    public void LengthDividedByVelocityYieldsTime()
    {
        Length distance = Length.Of(100, Si<Metre>());
        Velocity speed = Velocity.Of(10, Si<Metre>().Per(Si<Second>()));
        Time expected = Time.Of(10, Si<Second>());

        Time actual = distance / speed;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void VelocityTimesTimeYieldsLength()
    {
        Velocity speed = Velocity.Of(5, Si<Metre>().Per(Si<Second>()));
        Time duration = Time.Of(4, Si<Second>());
        Length expected = Length.Of(20, Si<Metre>());

        Length actual = speed * duration;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void VelocityDividedByAccelerationYieldsTime()
    {
        Velocity speed = Velocity.Of(30, Si<Metre>().Per(Si<Second>()));
        Acceleration deceleration = Acceleration.Of(10, Si<Metre>().Per(Square(Si<Second>())));
        Time expected = Time.Of(3, Si<Second>());

        Time actual = speed / deceleration;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void VelocityTimesForceYieldsPower()
    {
        Velocity speed = Velocity.Of(10, Si<Metre>().Per(Si<Second>()));
        Force force = Force.Of(50, Si<Newton>());
        Power expected = Power.Of(500, Si<Watt>());

        Power actual = speed * force;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AreaTimesPressureYieldsForce()
    {
        Area area = Area.Of(2, Square(Si<Metre>()));
        Pressure pressure = Pressure.Of(100, Si<Pascal>());
        Force expected = Force.Of(200, Si<Newton>());

        Force actual = area * pressure;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TimeTimesPowerYieldsEnergy()
    {
        Time duration = Time.Of(10, Si<Second>());
        Power power = Power.Of(50, Si<Watt>());
        Energy expected = Energy.Of(500, Si<Joule>());

        Energy actual = duration * power;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TimeTimesVelocityYieldsLength()
    {
        Time duration = Time.Of(3, Si<Second>());
        Velocity speed = Velocity.Of(7, Si<Metre>().Per(Si<Second>()));
        Length expected = Length.Of(21, Si<Metre>());

        Length actual = duration * speed;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TimeTimesAccelerationYieldsVelocity()
    {
        Time duration = Time.Of(5, Si<Second>());
        Acceleration acceleration = Acceleration.Of(3, Si<Metre>().Per(Square(Si<Second>())));
        Velocity expected = Velocity.Of(15, Si<Metre>().Per(Si<Second>()));

        Velocity actual = duration * acceleration;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ForceDividedByAreaYieldsPressure()
    {
        Force force = Force.Of(500, Si<Newton>());
        Area area = Area.Of(5, Square(Si<Metre>()));
        Pressure expected = Pressure.Of(100, Si<Pascal>());

        Pressure actual = force / area;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ForceTimesVelocityYieldsPower()
    {
        Force force = Force.Of(25, Si<Newton>());
        Velocity speed = Velocity.Of(4, Si<Metre>().Per(Si<Second>()));
        Power expected = Power.Of(100, Si<Watt>());

        Power actual = force * speed;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PowerDividedByForceYieldsVelocity()
    {
        Power power = Power.Of(200, Si<Watt>());
        Force force = Force.Of(40, Si<Newton>());
        Velocity expected = Velocity.Of(5, Si<Metre>().Per(Si<Second>()));

        Velocity actual = power / force;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PowerDividedByVelocityYieldsForce()
    {
        Power power = Power.Of(300, Si<Watt>());
        Velocity speed = Velocity.Of(6, Si<Metre>().Per(Si<Second>()));
        Force expected = Force.Of(50, Si<Newton>());

        Force actual = power / speed;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PowerTimesTimeYieldsEnergy()
    {
        Power power = Power.Of(100, Si<Watt>());
        Time duration = Time.Of(5, Si<Second>());
        Energy expected = Energy.Of(500, Si<Joule>());

        Energy actual = power * duration;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnergyDividedByTimeYieldsPower()
    {
        Energy energy = Energy.Of(600, Si<Joule>());
        Time duration = Time.Of(3, Si<Second>());
        Power expected = Power.Of(200, Si<Watt>());

        Power actual = energy / duration;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnergyDividedByPowerYieldsTime()
    {
        Energy energy = Energy.Of(1000, Si<Joule>());
        Power power = Power.Of(250, Si<Watt>());
        Time expected = Time.Of(4, Si<Second>());

        Time actual = energy / power;

        Assert.Equal(expected, actual);
    }
}
