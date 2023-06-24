using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class TimeTest
{
    [Fact]
    public void SecondsToString()
    {
        Time seconds = Time.Of(12).Si<Second>();

        Assert.Equal("12 s", seconds.ToString());
    }

    [Fact]
    public void SecondsToMinutes()
    {
        Time seconds = Time.Of(12).Si<Second>();
        Time minutes = seconds.To.Metric<Minute>();
        Time expected = Time.Of(12d / 60).Metric<Minute>();

        minutes.Matches(expected);
    }

    [Fact]
    public void MinutesToHours()
    {
        Time seconds = Time.Of(12).Metric<Minute>();
        Time hours = seconds.To.Si<Second>();
        Time expected = Time.Of(12 * 60).Si<Second>();

        hours.Matches(expected);
    }

    [Fact]
    public void SecondsToMicroSeconds()
    {
        Time seconds = Time.Of(12).Si<Second>();
        Time microSeconds = seconds.To.Si<Micro, Second>();
        Time expected = Time.Of(12d * 1e6).Si<Micro, Second>();

        microSeconds.Matches(expected);
    }

    [Fact]
    public void WeekToHours()
    {
        Time weeks = Time.Of(2).Metric<Week>();
        Time hours = weeks.To.Metric<Hour>();
        Time expected = Time.Of(2 * 7 * 24).Metric<Hour>();

        hours.Matches(expected);
    }
    [Fact]
    public void MinuteToMilliSecond()
    {
        Time minutes = Time.Of(4).Metric<Minute>();
        Time milliSeconds = minutes.To.Si<Milli, Second>();
        Time expected = Time.Of(4 * 60 * 1e3).Si<Milli, Second>();

        milliSeconds.Matches(expected);
    }

    [Fact]
    public void PlusWorks()
    {
        Time sum = Time.Of(2).Metric<Hour>() + Time.Of(72).Metric<Minute>() + Time.Of(30).Si<Second>();
        Time expected = Time.Of(2d + 72d / 60 + 30d / 3600).Metric<Hour>();

        sum.Matches(expected);
    }

    [Fact]
    public void TimeFromEnergyDividedByPower()
    {
        Power power = Power.Of(3).Si<Kilo, Watt>();
        Energy energy = Energy.Of(2.4).Si<Mega, Joule>();
        Time expected = Time.Of(800).Si<Second>();

        Time actual = energy / power;

        actual.Matches(expected);
    }
    [Theory]
    [MemberData(nameof(Times))]
    public void TimeSupportsSerialization(Time time) => time.SupportsSerialization();

    public static IEnumerable<Object[]> Times()
    {
        static IEnumerable<Time> Interesting()
        {
            yield return Time.Of(21).Si<Second>();
            yield return Time.Of(342).Si<Ronto, Second>();
            yield return Time.Of(6).Si<Deci, Second>();
            yield return Time.Of(-41).Metric<Minute>();
            yield return Time.Of(1.21).Metric<Hour>();
            yield return Time.Of(121).Metric<Day>();
            yield return Time.Of(95.2).Metric<Week>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
