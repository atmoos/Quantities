﻿using Quantities.Unit.Si.Accepted;

namespace Quantities.Test;

public sealed class VelocityTest
{
    [Fact]
    public void Create()
    {
        Velocity speed = Velocity.Si<Metre>(5).PerSecond();
        Assert.Equal("5 m/s", speed.ToString());
    }
    [Fact]
    public void KilometrePerHourToMetrePerSecond()
    {
        Velocity speed = Velocity.Si<Kilo, Metre>(36).Per<Hour>();
        Velocity expected = Velocity.Si<Metre>(10).PerSecond();

        Velocity actual = speed.To<Metre>().PerSecond();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MetrePerSecondToKilometrePerHour()
    {
        Velocity speed = Velocity.Si<Metre>(2).PerSecond();
        Velocity expected = Velocity.Si<Kilo, Metre>(7.2).Per<Hour>();

        Velocity actual = speed.To<Kilo, Metre>().Per<Hour>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void TrivialTransform()
    {
        Velocity speed = Velocity.Si<Metre>(200).PerSecond();
        Velocity expected = Velocity.Si<Metre>(0.2).Per<Milli, Second>();

        Velocity actual = speed.To<Metre>().Per<Milli, Second>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Transform()
    {
        Velocity speed = Velocity.Si<Centi, Metre>(4).PerSecond();
        Velocity expected = Velocity.Si<Milli, Metre>(40).PerSecond();

        Velocity actual = speed.To<Milli, Metre>().PerSecond();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MilesPerHourToKilometresPerHour()
    {
        Velocity speed = Velocity.Imperial<Mile>(4).Per<Hour>();
        Velocity expected = Velocity.Si<Kilo, Metre>(4 * 1.609344).Per<Hour>();

        Velocity actual = speed.To<Kilo, Metre>().Per<Hour>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MetresPerSecondToMilesPerHourTo()
    {
        Velocity speed = Velocity.Si<Metre>(0.44704).PerSecond();
        Velocity expected = Velocity.Imperial<Mile>(1).Per<Hour>();

        Velocity actual = speed.ToImperial<Mile>().Per<Hour>();

        actual.Matches(expected);
    }
}