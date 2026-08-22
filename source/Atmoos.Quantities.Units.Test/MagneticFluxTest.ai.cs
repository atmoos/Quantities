using Atmoos.Quantities.Units.NonStandard.MagneticFlux;
using Atmoos.Quantities.Units.Si.Derived.MagneticFlux;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class MagneticFluxTest
{
    [Fact]
    public void WeberToString() => FormattingMatches(v => MagneticFlux.Of(v, Si<Weber>()), "Wb");

    [Fact]
    public void MaxwellToString() => FormattingMatches(v => MagneticFlux.Of(v, NonStandard<Maxwell>()), "Mx");

    [Fact]
    public void MaxwellToWeber()
    {
        MagneticFlux flux = MagneticFlux.Of(100000000, NonStandard<Maxwell>());
        MagneticFlux expected = MagneticFlux.Of(1, Si<Weber>());

        MagneticFlux actual = flux.To(Si<Weber>());

        actual.Matches(expected);
    }

    [Fact]
    public void WeberToMaxwell()
    {
        MagneticFlux flux = MagneticFlux.Of(1, Si<Weber>());
        MagneticFlux expected = MagneticFlux.Of(100000000, NonStandard<Maxwell>());

        MagneticFlux actual = flux.To(NonStandard<Maxwell>());

        actual.Matches(expected);
    }
}
