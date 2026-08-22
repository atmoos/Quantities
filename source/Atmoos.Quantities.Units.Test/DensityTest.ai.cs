using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class DensityTest
{
    [Fact]
    public void KilogramPerCubicMetreToString() => FormattingMatches(v => Density.Of(v, Si<Kilogram>().Per(Cubic(Si<Metre>()))), "kg/m³");

    [Fact]
    public void GramPerCubicCentiMetreToString() => FormattingMatches(v => Density.Of(v, Metric<Gram>().Per(Cubic(Si<Centi, Metre>()))), "g/cm³");

    [Fact]
    public void GramPerCubicCentiMetreToKilogramPerCubicMetre()
    {
        Density density = Density.Of(1, Metric<Gram>().Per(Cubic(Si<Centi, Metre>())));
        Density expected = Density.Of(1000, Si<Kilogram>().Per(Cubic(Si<Metre>())));

        Density actual = density.To(Si<Kilogram>().Per(Cubic(Si<Metre>())));

        actual.Matches(expected);
    }

    [Fact]
    public void KilogramPerCubicMetreToGramPerCubicCentiMetre()
    {
        Density density = Density.Of(1000, Si<Kilogram>().Per(Cubic(Si<Metre>())));
        Density expected = Density.Of(1, Metric<Gram>().Per(Cubic(Si<Centi, Metre>())));

        Density actual = density.To(Metric<Gram>().Per(Cubic(Si<Centi, Metre>())));

        actual.Matches(expected);
    }
}
