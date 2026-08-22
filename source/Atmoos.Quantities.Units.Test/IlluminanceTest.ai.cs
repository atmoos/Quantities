using Atmoos.Quantities.Units.NonStandard.Illuminance;
using Atmoos.Quantities.Units.Si.Derived.Illuminance;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class IlluminanceTest
{
    [Fact]
    public void LuxToString() => FormattingMatches(v => Illuminance.Of(v, Si<Lux>()), "lx");

    [Fact]
    public void FootCandleToString() => FormattingMatches(v => Illuminance.Of(v, NonStandard<FootCandle>()), "fc");

    [Fact]
    public void FootCandleToLux()
    {
        Illuminance illuminance = Illuminance.Of(1, NonStandard<FootCandle>());
        Illuminance expected = Illuminance.Of(100000000d / 9290304d, Si<Lux>());

        Illuminance actual = illuminance.To(Si<Lux>());

        actual.Matches(expected);
    }

    [Fact]
    public void LuxToFootCandle()
    {
        Illuminance illuminance = Illuminance.Of(100000000d / 9290304d, Si<Lux>());
        Illuminance expected = Illuminance.Of(1, NonStandard<FootCandle>());

        Illuminance actual = illuminance.To(NonStandard<FootCandle>());

        actual.Matches(expected);
    }
}
