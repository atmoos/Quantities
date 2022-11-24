using Quantities.Units.Si.Accepted;
using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class TimeTest
{
    [Fact]
    public void SecondsToString()
    {
        Time seconds = Time.Seconds(12);

        Assert.Equal("12 s", seconds.ToString());
    }

    [Fact]
    public void SecondsToMinutes()
    {
        Time seconds = Time.Seconds(12);
        Time minutes = seconds.To<Minute>();
        Time expected = Time.In<Minute>(12d / 60);

        minutes.Matches(expected);
    }

    [Fact]
    public void MinutesToHours()
    {
        Time seconds = Time.In<Minute>(12);
        Time hours = seconds.ToSeconds();
        Time expected = Time.Seconds(12 * 60);

        hours.Matches(expected);
    }

    [Fact]
    public void SecondsToMicroSeconds()
    {
        Time seconds = Time.Seconds(12);
        Time microSeconds = seconds.To<Micro, Second>();
        Time expected = Time.Si<Micro, Second>(12d * 1e6);

        microSeconds.Matches(expected);
    }

    [Fact]
    public void WeekToHours()
    {
        Time weeks = Time.In<Week>(2);
        Time hours = weeks.To<Hour>();
        Time expected = Time.In<Hour>(2 * 7 * 24);

        hours.Matches(expected);
    }
    [Fact]
    public void MinuteToMilliSecond()
    {
        Time minutes = Time.In<Minute>(4);
        Time milliSeconds = minutes.To<Milli, Second>();
        Time expected = Time.Si<Milli, Second>(4 * 60 * 1e3);

        milliSeconds.Matches(expected);
    }

    [Fact]
    public void PlusWorks()
    {
        Time sum = Time.In<Hour>(2) + Time.In<Minute>(72) + Time.Seconds(30);
        Time expected = Time.In<Hour>(2d + 72d / 60 + 30d / 3600);

        sum.Matches(expected);
    }

    [Fact]
    public void TimeFromEnergyDividedByPower()
    {
        Power power = Power.Si<Kilo, Watt>(3);
        Energy energy = Energy.Si<Mega, Joule>(2.4);
        Time expected = Time.Seconds(800);

        Time actual = energy / power;

        actual.Matches(expected);
    }
}
