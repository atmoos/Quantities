using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class FrequencyTest
{
    [Fact]
    public void HertzToString() => FormattingMatches(v => Frequency.Of(v, Si<Hertz>()), "Hz");
    [Fact]
    public void KiloHertzToString() => FormattingMatches(v => Frequency.Of(v, Si<Kilo, Hertz>()), "kHz");
    [Fact]
    public void CentiHertzToString() => FormattingMatches(v => Frequency.Of(v, Si<Centi, Hertz>()), "cHz");
    [Fact]
    public void InvertedFrequencyIsTime()
    {
        var expected = Time.Of(0.5, Si<Second>());
        var freq = Frequency.Of(2, Si<Hertz>());

        Time actual = 1 / freq;

        actual.Matches(expected);
    }

    [Fact]
    // ToDo: To enable correct time prefix requires issue #5 to be resolved
    public void InvertedPrefixedFrequencyIsTimeWithoutPrefix()
    {
        var expected = Time.Of(500, Si<Micro, Second>());
        var freq = Frequency.Of(2, Si<Kilo, Hertz>());

        Time actual = 1 / freq;

        // ToDo: actual.Matches(expected);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FrequencyTimesTimeIsScalar()
    {
        const Double expected = 1;
        var time = Time.Of(0.5, Si<Milli, Second>());
        var frequency = Frequency.Of(2, Si<Kilo, Hertz>());

        Double actual = frequency * time;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TimeTimesFrequencyIsScalar()
    {
        const Double expected = 4e3;
        var time = Time.Of(2, Si<Milli, Second>());
        var frequency = Frequency.Of(2, Si<Mega, Hertz>());

        Double actual = time * frequency;

        Assert.Equal(expected, actual);
    }
}
