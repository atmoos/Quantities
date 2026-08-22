namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class AmountOfSubstanceTest
{
    [Fact]
    public void MoleToString() => FormattingMatches(v => AmountOfSubstance.Of(v, Si<Mole>()), "mol");

    [Fact]
    public void MilliMoleToString() => FormattingMatches(v => AmountOfSubstance.Of(v, Si<Milli, Mole>()), "mmol");

    [Fact]
    public void KiloMoleToMole()
    {
        AmountOfSubstance amount = AmountOfSubstance.Of(1, Si<Kilo, Mole>());
        AmountOfSubstance expected = AmountOfSubstance.Of(1000, Si<Mole>());

        AmountOfSubstance actual = amount.To(Si<Mole>());

        actual.Matches(expected);
    }
}
