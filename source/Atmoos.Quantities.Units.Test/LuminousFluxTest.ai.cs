using Atmoos.Quantities.Units.Si.Derived.LuminousFlux;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class LuminousFluxTest
{
    [Fact]
    public void LumenToString() => FormattingMatches(v => LuminousFlux.Of(v, Si<Lumen>()), "lm");

    [Fact]
    public void KiloLumenToString() => FormattingMatches(v => LuminousFlux.Of(v, Si<Kilo, Lumen>()), "klm");

    [Fact]
    public void KiloLumenToLumen()
    {
        LuminousFlux flux = LuminousFlux.Of(1, Si<Kilo, Lumen>());
        LuminousFlux expected = LuminousFlux.Of(1000, Si<Lumen>());

        LuminousFlux actual = flux.To(Si<Lumen>());

        actual.Matches(expected);
    }
}
