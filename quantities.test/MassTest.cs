﻿using Quantities.Units.Imperial.Mass;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class MassTest
{
    private const Double gramsInPound = 453.59237;
    private static readonly Mass onePound = Mass.Imperial<Pound>(1);
    [Fact]
    public void KilogramToString() => FormattingMatches(v => Mass.Si<Kilogram>(v), "kg");
    [Fact]
    public void GramToString() => FormattingMatches(v => Mass.Metric<Gram>(v), "g");
    [Fact]
    public void TonneToString() => FormattingMatches(v => Mass.Metric<Tonne>(v), "t");
    [Fact]
    public void KiloGramToString() => FormattingMatches(v => Mass.Metric<Kilo, Gram>(v), "Kg");
    [Fact]
    public void MicroGramToString() => FormattingMatches(v => Mass.Metric<Micro, Gram>(v), "μg");
    [Fact]
    public void KiloTonneToString() => FormattingMatches(v => Mass.Metric<Kilo, Tonne>(v), "Kt");
    [Fact]
    public void MegaTonneToString() => FormattingMatches(v => Mass.Metric<Mega, Tonne>(v), "Mt");
    [Fact]
    public void KilogramKiloGramEquivalence()
    {
        Mass siKilogram = Mass.Si<Kilogram>(0.3251);
        Mass pseudoKiloGram = Mass.Metric<Kilo, Gram>(0.3251);

        Assert.Equal(siKilogram, pseudoKiloGram);
    }
    [Fact]
    public void GramToKilogram()
    {
        Mass mass = Mass.Metric<Gram>(1600);
        Mass expected = Mass.Si<Kilogram>(1.6);

        Mass actual = mass.To<Kilogram>();

        actual.Matches(expected);
    }
    [Fact]
    public void KilogramToGram()
    {
        Mass mass = Mass.Si<Kilogram>(0.8);
        Mass expected = Mass.Metric<Gram>(800);

        Mass actual = mass.ToMetric<Gram>();

        actual.Matches(expected);
    }
    [Fact]
    public void TonneToKilogram()
    {
        Mass mass = Mass.Metric<Tonne>(0.2);
        Mass expected = Mass.Si<Kilogram>(200);

        Mass actual = mass.To<Kilogram>();

        actual.Matches(expected);
    }
    [Fact]
    public void KilogramToTonne()
    {
        Mass mass = Mass.Si<Kilogram>(1200);
        Mass expected = Mass.Metric<Tonne>(1.2);

        Mass actual = mass.ToMetric<Tonne>();

        actual.Matches(expected);
    }
    [Fact]
    public void GramToTonne()
    {
        Mass mass = Mass.Metric<Kilo, Gram>(1500);
        Mass expected = Mass.Metric<Tonne>(1.5);

        Mass actual = mass.ToMetric<Tonne>();

        actual.Matches(expected);
    }
    [Fact]
    public void TonneToGram()
    {
        Mass mass = Mass.Metric<Tonne>(0.003);
        Mass expected = Mass.Metric<Gram>(3000);

        Mass actual = mass.ToMetric<Gram>();

        actual.Matches(expected);
    }
    [Fact]
    public void GramToPound()
    {
        Mass mass = Mass.Metric<Gram>(3d * gramsInPound);
        Mass expected = Mass.Imperial<Pound>(3);

        Mass actual = mass.ToImperial<Pound>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void PoundToGram()
    {
        Mass mass = Mass.Imperial<Pound>(2);
        Mass expected = Mass.Metric<Gram>(2d * gramsInPound);

        Mass actual = mass.ToMetric<Gram>();

        actual.Matches(expected);
    }
    [Fact]
    public void PoundToStone()
    {
        Mass mass = Mass.Imperial<Pound>(28);
        Mass expected = Mass.Imperial<Stone>(2);

        Mass actual = mass.ToImperial<Stone>();

        actual.Matches(expected);
    }
    [Fact]
    public void StoneToPound()
    {
        Mass mass = Mass.Imperial<Stone>(0.5);
        Mass expected = Mass.Imperial<Pound>(7);

        Mass actual = mass.ToImperial<Pound>();

        actual.Matches(expected);
    }
    [Fact]
    public void PoundToOunce()
    {
        Mass mass = Mass.Imperial<Pound>(4);
        Mass expected = Mass.Imperial<Ounce>(64);

        Mass actual = mass.ToImperial<Ounce>();

        actual.Matches(expected);
    }
    [Fact]
    public void OunceToPound()
    {
        Mass mass = Mass.Imperial<Ounce>(4);
        Mass expected = Mass.Imperial<Pound>(0.25);

        Mass actual = mass.ToImperial<Pound>();

        actual.Matches(expected);
    }
    [Fact]
    public void DefinitionOfGrainHolds()
    {
        Assert.Equal(onePound, Mass.Imperial<Grain>(7000));
    }
    [Fact]
    public void DefinitionOfDrachmHolds()
    {
        Assert.Equal(onePound, Mass.Imperial<Drachm>(256));
    }
    [Fact]
    public void DefinitionOfOunceHolds()
    {
        Assert.Equal(onePound, Mass.Imperial<Ounce>(16));
    }
    [Fact]
    public void DefinitionOfPoundHolds()
    {
        Assert.Equal(Mass.Si<Kilogram>(1), Mass.Imperial<Pound>(1000d / gramsInPound));
    }
    [Fact]
    public void DefinitionOfStoneHolds()
    {
        Assert.Equal(Mass.Imperial<Pound>(14), Mass.Imperial<Stone>(1));
    }
    [Fact]
    public void DefinitionOfQuarterHolds()
    {
        Assert.Equal(Mass.Imperial<Pound>(28), Mass.Imperial<Quarter>(1));
    }
    [Fact]
    public void DefinitionOfHundredweightHolds()
    {
        Assert.Equal(Mass.Imperial<Pound>(112), Mass.Imperial<Hundredweight>(1));
    }
    [Fact]
    public void DefinitionOfTonHolds()
    {
        Assert.Equal(Mass.Imperial<Pound>(2240), Mass.Imperial<Ton>(1));
    }
    [Fact]
    public void DefinitionOfSlugHolds()
    {
        Assert.Equal(Mass.Si<Kilogram>(14.59390294), Mass.Imperial<Slug>(1));
    }
}
