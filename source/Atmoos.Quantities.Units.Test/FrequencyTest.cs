using System.Globalization;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test;

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

        Time actual = 1d / freq;

        actual.Matches(expected);
    }

    [Fact]
    // ToDo #5: Enable correct time prefix
    public void InvertedPrefixedFrequencyIsTimeWithoutPrefix()
    {
        var expected = Time.Of(500, Si<Micro, Second>());
        var freq = Frequency.Of(2, Si<Kilo, Hertz>());

        Time actual = 1d / freq;

        // ToDo #5: actual.Matches(expected);
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

    [Fact]
    public void InvertedTimeIsFrequency()
    {
        const Double hertz = 4;
        var time = Time.Of(1 / hertz, Si<Second>());
        var frequency = Frequency.Of(hertz, Si<Hertz>());

        Frequency actual = 1d / time;

        // we'll get 4 s⁻¹. Hence, not comparing for matching units!
        Assert.Equal(frequency, actual);
        Assert.Equal("4 s⁻¹", actual.ToString());
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void FrequencyCanConvertAcrossUnitsUsingTo()
    {
        var kiloHertz = Frequency.Of(2.5, Si<Kilo, Hertz>());
        var expected = Frequency.Of(2500, Si<Hertz>());

        var actual = kiloHertz.To(Si<Hertz>());

        actual.Matches(expected);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void EqualFrequenciesHaveEqualHashCode()
    {
        var left = Frequency.Of(1000, Si<Hertz>());
        var right = Frequency.Of(1000, Si<Hertz>());

        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void FrequencyDoesNotEqualObjectOfDifferentType()
    {
        var frequency = Frequency.Of(10, Si<Hertz>());
        Object other = Time.Of(0.1, Si<Second>());

        Assert.False(frequency.Equals(other));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void FrequencyEqualsMatchingObjectType()
    {
        var frequency = Frequency.Of(10, Si<Hertz>());
        Object other = Frequency.Of(10, Si<Hertz>());

        Assert.True(frequency.Equals(other));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void FrequencyObjectEqualityCoversBothOutcomes()
    {
        var left = Frequency.Of(8, Si<Hertz>());
        Object equal = Frequency.Of(8, Si<Hertz>());
        Object different = Frequency.Of(9, Si<Hertz>());

        Assert.True(left.Equals(equal));
        Assert.False(left.Equals(different));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void FrequencyDoesNotEqualDifferentMatchingObjectType()
    {
        var frequency = Frequency.Of(10, Si<Hertz>());
        Object other = Frequency.Of(11, Si<Hertz>());

        Assert.False(frequency.Equals(other));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void FormattedFrequencyToStringMatchesExpected()
    {
        var frequency = Frequency.Of(1.23456789, Si<Kilo, Hertz>());

        var actual = frequency.ToString("f2", CultureInfo.InvariantCulture);

        Assert.Equal("1.23 kHz", actual);
    }
}
