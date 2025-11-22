using Atmoos.Quantities.Units.Imperial.Force;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test;

public sealed class ForceTest
{
    [Fact]
    public void NewtonToString() => FormattingMatches(v => Force.Of(v, Si<Newton>()), "N");
    [Fact]
    public void KiloNewtonToString() => FormattingMatches(v => Force.Of(v, Si<Kilo, Newton>()), "kN");
    [Fact]
    public void PoundForceToString() => FormattingMatches(v => Force.Of(v, Imperial<PoundForce>()), "lbf");
    [Fact]
    public void PoundForceMatchesDefinitionInNewton()
    {
        const Double valueInNewtons = 4.4482216152605; // from Wikipedia
        Force newton = Force.Of(valueInNewtons, Si<Newton>());
        Force poundForce = Force.Of(1, Imperial<PoundForce>());
        Force convertedPoundForce = poundForce.To(Si<Newton>());

        Assert.Equal(newton, poundForce);
        convertedPoundForce.Matches(newton);
    }
    [Fact]
    public void NewtonFromPressureAndArea()
    {
        Pressure pressure = Pressure.Of(800, Si<Newton>().Per(Square(Si<Metre>())));
        Area area = Area.Of(2, Square(Si<Metre>()));
        Force expected = Force.Of(1600, Si<Newton>());

        Force actual = pressure * area;

        // Newton can be extracted out of Pressure without conversion, so we use Matches to verify equality
        actual.Matches(expected);
    }

    [Fact]
    public void NewtonFromDerivedPressureAndArea()
    {
        Pressure pressure = Pressure.Of(500, Si<Pascal>());
        Area area = Area.Of(2, Square(Si<Metre>()));
        Force expected = Force.Of(1000, Si<Newton>());

        Force actual = pressure * area; // Pa*m²

        // Even though Pascal is defined in terms of Newton, Newton cannot be extracted out of Pascal yet.
        // Hence we compare for equality and not an exact match.
        Assert.Equal(expected, actual);
    }
}
