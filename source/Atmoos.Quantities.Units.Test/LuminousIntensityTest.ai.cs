namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class LuminousIntensityTest
{
    [Fact]
    public void CandelaToString() => FormattingMatches(v => LuminousIntensity.Of(v, Si<Candela>()), "cd");

    [Fact]
    public void MilliCandelaToString() => FormattingMatches(v => LuminousIntensity.Of(v, Si<Milli, Candela>()), "mcd");

    [Fact]
    public void KiloCandelaToCandela()
    {
        LuminousIntensity intensity = LuminousIntensity.Of(1, Si<Kilo, Candela>());
        LuminousIntensity expected = LuminousIntensity.Of(1000, Si<Candela>());

        LuminousIntensity actual = intensity.To(Si<Candela>());

        actual.Matches(expected);
    }
}
