using Quantities.Units.Si.Derived;

namespace Quantities.Test;

public sealed class FrequencyTest
{
    [Fact]
    public void Foo()
    {
        var expected = Time.Of(0.5, Si<Second>());
        var freq = Frequency.Of(2, Si<Hertz>());

        Time actual = 1 / freq;

        actual.Matches(expected);
    }

    [Fact]
    public void Bar()
    {
        var expected = Time.Of(0.5, Si<Milli, Second>());
        var freq = Frequency.Of(2, Si<Kilo, Hertz>());

        Time actual = 1 / freq;

        actual.Matches(expected);
    }
}
