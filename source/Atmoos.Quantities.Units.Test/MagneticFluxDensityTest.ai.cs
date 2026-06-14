using Atmoos.Quantities.Units.NonStandard.MagneticFluxDensity;
using Atmoos.Quantities.Units.Si.Derived.MagneticFluxDensity;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class MagneticFluxDensityTest
{
    [Fact]
    public void TeslaToString() => FormattingMatches(v => MagneticFluxDensity.Of(v, Si<Tesla>()), "T");

    [Fact]
    public void GaussToString() => FormattingMatches(v => MagneticFluxDensity.Of(v, NonStandard<Gauss>()), "G");

    [Fact]
    public void GaussToTesla()
    {
        MagneticFluxDensity density = MagneticFluxDensity.Of(10000, NonStandard<Gauss>());
        MagneticFluxDensity expected = MagneticFluxDensity.Of(1, Si<Tesla>());

        MagneticFluxDensity actual = density.To(Si<Tesla>());

        actual.Matches(expected);
    }

    [Fact]
    public void TeslaToGauss()
    {
        MagneticFluxDensity density = MagneticFluxDensity.Of(1, Si<Tesla>());
        MagneticFluxDensity expected = MagneticFluxDensity.Of(10000, NonStandard<Gauss>());

        MagneticFluxDensity actual = density.To(NonStandard<Gauss>());

        actual.Matches(expected);
    }
}
