using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

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
}
