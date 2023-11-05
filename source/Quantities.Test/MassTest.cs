using Quantities.Units.Imperial.Mass;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class MassTest
{
    private const Double gramsInPound = 453.59237;
    private static readonly Mass onePound = Mass.Of(1, Imperial<Pound>());
    [Fact]
    public void KilogramToString() => FormattingMatches(v => Mass.Of(v, Si<Kilogram>()), "kg");
    [Fact]
    public void GramToString() => FormattingMatches(v => Mass.Of(v, Metric<Gram>()), "g");
    [Fact]
    public void TonneToString() => FormattingMatches(v => Mass.Of(v, Metric<Tonne>()), "t");
    [Fact]
    public void KiloGramToString() => FormattingMatches(v => Mass.Of(v, Metric<Kilo, Gram>()), "Kg");
    [Fact]
    public void MicroGramToString() => FormattingMatches(v => Mass.Of(v, Metric<Micro, Gram>()), "μg");
    [Fact]
    public void KiloTonneToString() => FormattingMatches(v => Mass.Of(v, Metric<Kilo, Tonne>()), "Kt");
    [Fact]
    public void MegaTonneToString() => FormattingMatches(v => Mass.Of(v, Metric<Mega, Tonne>()), "Mt");
    [Fact]
    public void KilogramKiloGramEquivalence()
    {
        Mass siKilogram = Mass.Of(0.3251, Si<Kilogram>());
        Mass pseudoKiloGram = Mass.Of(0.3251, Metric<Kilo, Gram>());

        Assert.Equal(siKilogram, pseudoKiloGram);
    }
    [Fact]
    public void GramToKilogram()
    {
        Mass mass = Mass.Of(1600, Metric<Gram>());
        Mass expected = Mass.Of(1.6, Si<Kilogram>());

        Mass actual = mass.To(Si<Kilogram>());

        actual.Matches(expected);
    }
    [Fact]
    public void KilogramToGram()
    {
        Mass mass = Mass.Of(0.8, Si<Kilogram>());
        Mass expected = Mass.Of(800, Metric<Gram>());

        Mass actual = mass.To(Metric<Gram>());

        actual.Matches(expected);
    }
    [Fact]
    public void TonneToKilogram()
    {
        Mass mass = Mass.Of(0.2, Metric<Tonne>());
        Mass expected = Mass.Of(200, Si<Kilogram>());

        Mass actual = mass.To(Si<Kilogram>());

        actual.Matches(expected);
    }
    [Fact]
    public void KilogramToTonne()
    {
        Mass mass = Mass.Of(1200, Si<Kilogram>());
        Mass expected = Mass.Of(1.2, Metric<Tonne>());

        Mass actual = mass.To(Metric<Tonne>());

        actual.Matches(expected);
    }
    [Fact]
    public void GramToTonne()
    {
        Mass mass = Mass.Of(1500, Metric<Kilo, Gram>());
        Mass expected = Mass.Of(1.5, Metric<Tonne>());

        Mass actual = mass.To(Metric<Tonne>());

        actual.Matches(expected);
    }
    [Fact]
    public void TonneToGram()
    {
        Mass mass = Mass.Of(0.003, Metric<Tonne>());
        Mass expected = Mass.Of(3000, Metric<Gram>());

        Mass actual = mass.To(Metric<Gram>());

        actual.Matches(expected);
    }
    [Fact]
    public void GramToPound()
    {
        Mass mass = Mass.Of(3d * gramsInPound, Metric<Gram>());
        Mass expected = Mass.Of(3, Imperial<Pound>());

        Mass actual = mass.To(Imperial<Pound>());

        actual.Matches(expected);
    }
    [Fact]
    public void PoundToGram()
    {
        Mass mass = Mass.Of(2, Imperial<Pound>());
        Mass expected = Mass.Of(2d * gramsInPound, Metric<Gram>());

        Mass actual = mass.To(Metric<Gram>());

        actual.Matches(expected);
    }
    [Fact]
    public void PoundToStone()
    {
        Mass mass = Mass.Of(28, Imperial<Pound>());
        Mass expected = Mass.Of(2, Imperial<Stone>());

        Mass actual = mass.To(Imperial<Stone>());

        actual.Matches(expected);
    }
    [Fact]
    public void StoneToPound()
    {
        Mass mass = Mass.Of(0.5, Imperial<Stone>());
        Mass expected = Mass.Of(7, Imperial<Pound>());

        Mass actual = mass.To(Imperial<Pound>());

        actual.Matches(expected);
    }
    [Fact]
    public void PoundToOunce()
    {
        Mass mass = Mass.Of(4, Imperial<Pound>());
        Mass expected = Mass.Of(64, Imperial<Ounce>());

        Mass actual = mass.To(Imperial<Ounce>());

        actual.Matches(expected);
    }
    [Fact]
    public void OunceToPound()
    {
        Mass mass = Mass.Of(4, Imperial<Ounce>());
        Mass expected = Mass.Of(0.25, Imperial<Pound>());

        Mass actual = mass.To(Imperial<Pound>());

        actual.Matches(expected);
    }
    [Fact]
    public void DefinitionOfGrainHolds()
    {
        Assert.Equal(onePound, Mass.Of(7000, Imperial<Grain>()));
    }
    [Fact]
    public void DefinitionOfDrachmHolds()
    {
        Assert.Equal(onePound, Mass.Of(256, Imperial<Drachm>()));
    }
    [Fact]
    public void DefinitionOfOunceHolds()
    {
        Assert.Equal(onePound, Mass.Of(16, Imperial<Ounce>()));
    }
    [Fact]
    public void DefinitionOfPoundHolds()
    {
        Assert.Equal(Mass.Of(1, Si<Kilogram>()), Mass.Of(1000d / gramsInPound, Imperial<Pound>()));
    }
    [Fact]
    public void DefinitionOfStoneHolds()
    {
        Assert.Equal(Mass.Of(14, Imperial<Pound>()), Mass.Of(1, Imperial<Stone>()));
    }
    [Fact]
    public void DefinitionOfQuarterHolds()
    {
        Assert.Equal(Mass.Of(28, Imperial<Pound>()), Mass.Of(1, Imperial<Quarter>()));
    }
    [Fact]
    public void DefinitionOfHundredweightHolds()
    {
        Assert.Equal(Mass.Of(112, Imperial<Pound>()), Mass.Of(1, Imperial<Hundredweight>()));
    }
    [Fact]
    public void DefinitionOfTonHolds()
    {
        Assert.Equal(Mass.Of(2240, Imperial<Pound>()), Mass.Of(1, Imperial<Ton>()));
    }
    [Fact]
    public void DefinitionOfSlugHolds()
    {
        Assert.Equal(Mass.Of(14.59390294, Si<Kilogram>()), Mass.Of(1, Imperial<Slug>()));
    }
}
