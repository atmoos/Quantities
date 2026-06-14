using Atmoos.Quantities.Units.NonStandard.DynamicViscosity;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class DynamicViscosityTest
{
    [Fact]
    public void PascalSecondToString() => FormattingMatches(v => DynamicViscosity.Of(v, Si<Pascal>().Times(Si<Second>())), Join("Pa", "s"));

    [Fact]
    public void PoiseToString() => FormattingMatches(v => DynamicViscosity.Of(v, NonStandard<Poise>()), "P");

    [Fact]
    public void PoiseToPascalSecond()
    {
        DynamicViscosity viscosity = DynamicViscosity.Of(10, NonStandard<Poise>());
        DynamicViscosity expected = DynamicViscosity.Of(1, Si<Pascal>().Times(Si<Second>()));

        DynamicViscosity actual = viscosity.To(Si<Pascal>().Times(Si<Second>()));

        actual.Matches(expected);
    }

    [Fact]
    public void PascalSecondToPoise()
    {
        DynamicViscosity viscosity = DynamicViscosity.Of(1, Si<Pascal>().Times(Si<Second>()));
        DynamicViscosity expected = DynamicViscosity.Of(10, NonStandard<Poise>());

        DynamicViscosity actual = viscosity.To(NonStandard<Poise>());

        actual.Matches(expected);
    }
}
