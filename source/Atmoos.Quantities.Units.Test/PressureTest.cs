using Atmoos.Quantities.Units.Imperial.Force;
using Atmoos.Quantities.Units.NonStandard.Pressure;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

public sealed class PressureTest
{

    [Fact]
    public void HectoPascalToString() => FormattingMatches(p => Pressure.Of(p, Si<Hecto, Pascal>()), "hPa");
    [Fact]
    public void KiloNewtonPerSquareMetreToString() => FormattingMatches(p => Pressure.Of(p, Si<Kilo, Newton>().Per(Square(Si<Metre>()))), "kN/m²");
    [Fact]
    public void PoundForcePerSquareFootToString() => FormattingMatches(p => Pressure.Of(p, Imperial<PoundForce>().Per(Square(Imperial<Foot>()))), "lbf/ft²");
    [Fact]
    public void StandardAtmosphereToString() => FormattingMatches(p => Pressure.Of(p, NonStandard<StandardAtmosphere>()), "atm"); [Fact]
    public void TorrToString() => FormattingMatches(p => Pressure.Of(p, NonStandard<Torr>()), "Torr");

    [Fact]
    public void PascalIsNewtonPerSquareMetre()
    {
        Pressure onePascal = Pressure.Of(1, Si<Pascal>());
        Pressure siDefinition = Pressure.Of(1, Si<Newton>().Per(Square(Si<Metre>())));

        Assert.Equal(onePascal, siDefinition);
    }
    [Fact]
    public void BarConvertsToPascal()
    {
        Pressure pascal = Pressure.Of(1e5, Si<Pascal>());
        Pressure oneBar = Pressure.Of(1, Metric<Bar>());

        Assert.Equal(pascal, oneBar);
    }
    [Fact]
    public void StandardAtmosphereConvertsToPascal()
    {
        Pressure pascal = Pressure.Of(101325, Si<Pascal>());
        Pressure oneStandardAtmosphere = Pressure.Of(1, NonStandard<StandardAtmosphere>());

        Assert.Equal(pascal, oneStandardAtmosphere);
    }
    [Fact]
    public void TorrConvertsToPascal()
    {
        Pressure pascal = Pressure.Of(101325d / 760d, Si<Pascal>());
        Pressure oneTorr = Pressure.Of(1, NonStandard<Torr>());

        Pressure oneTorrInPascals = oneTorr.To(Si<Pascal>());

        oneTorrInPascals.Matches(pascal);
    }
    [Fact]
    public void PsiIsConvertsToPascal()
    {
        const Double valueInPascals = 6894.75729316836133672267; // from WolframAlpha
        Pressure pascals = Pressure.Of(valueInPascals, Si<Pascal>());
        Pressure psi = Pressure.Of(1, Imperial<PoundForce>().Per(Square(Imperial<Inch>())));
        Pressure convertedPsi = psi.To(Si<Pascal>());

        Assert.Equal(pascals, psi);
        convertedPsi.Matches(pascals);
    }
    [Fact]
    public void PressureFromForceDividedByArea()
    {
        Force force = Force.Of(232, in Si<Kilo, Newton>());
        Area area = Area.Of(8, Square(in Si<Metre>()));
        Pressure expected = Pressure.Of(29, Si<Kilo, Newton>().Per(Square(in Si<Metre>())));

        Pressure actual = force / area;

        expected.Matches(actual);
    }
}
