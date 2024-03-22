using Quantities.Units.Si.Metric;
using Quantities.Units.Imperial.Area;
using Quantities.Units.Imperial.Mass;

using nArea = UnitsNet.Area;
using nLength = UnitsNet.Length;
using nMass = UnitsNet.Mass;
using nVolume = UnitsNet.Volume;

namespace Quantities.Test.Compare;

public class QuantitiesIsMoreAccurateThanUnitsNet
{
    [Fact]
    public void SquareMilesToSquareKilometres()
    {
        Double expectedSqKiloMetre = 5.179976220672; // 2×1.609344×1.609344 using full precision math.

        // Quantities
        Area squareMiles = Area.Of(2, Square(Imperial<Mile>()));
        Area actual = squareMiles.To(Square(Si<Kilo, Metre>()));

        // UnitsNet
        nArea nSquareMiles = nArea.FromSquareMiles(2);
        nArea nActual = nSquareMiles.ToUnit(UnitsNet.Units.AreaUnit.SquareKilometer);

        // Quantities is marginally more accurate that UnitsNet
        Assert.Equal(expectedSqKiloMetre, actual);
        Assert.Equal(expectedSqKiloMetre, nActual.Value, 14); // two digits less accurate.

        // Sanity check
        Double roundRobinSqMiles = actual.To(Square(Imperial<Mile>()));
        Double nRoundRobinSqMiles = nActual.ToUnit(UnitsNet.Units.AreaUnit.SquareMile).Value;
        Assert.Equal(2d, roundRobinSqMiles);
        Assert.Equal(2d, nRoundRobinSqMiles);
    }

    [Fact]
    public void SquareFeetTimesYards()
    {
        const Double expectedCubicFeet = 162;

        // Quantities
        Area area = Area.Of(27, Square(Imperial<Foot>()));
        Length length = Length.Of(2, Imperial<Yard>());

        Volume actual = area * length;

        // UnitsNet
        nArea nA = nArea.FromSquareFeet(27);
        nLength nL = nLength.FromYards(2);

        nVolume nActualInCubicMetres = nA * nL; // results in m³
        // need to convert to cubic feet first...
        nVolume nActual = nActualInCubicMetres.ToUnit(UnitsNet.Units.VolumeUnit.CubicFoot);

        // Quantities is marginally more accurate that UnitsNet
        Assert.Equal(expectedCubicFeet, actual);
        Assert.Equal(expectedCubicFeet, nActual.Value, 13);

        // Benefit of the doubt, let's check UnitsNet default answer: m³
        const Double expectedCubicMetre = 4.587329147904; // 162×(0.3048×0.3048×0.3048) using full precision math
        Volume cubicMetre = actual.To(Cubic(Si<Metre>()));
        Assert.Equal(expectedCubicMetre, cubicMetre); // is accurate
        Assert.Equal(expectedCubicMetre, nActualInCubicMetres.Value); // is also accurate
    }

    [Fact]
    public void PureArealDimensionDividedByLength()
    {
        const Double expectedYards = 16;

        // Quantities
        Area area = Area.Of(2, Imperial<Acre>());
        Length length = Length.Of(1815, Imperial<Foot>());

        Length actual = area / length;

        // UnitsNet
        nArea nA = nArea.FromAcres(2);
        nLength nL = nLength.FromFeet(1815);

        nLength nActualInMeters = nA / nL; // is in m
        // need to convert to yards first
        nLength nActual = nActualInMeters.ToUnit(UnitsNet.Units.LengthUnit.Yard);

        // Quantities is marginally more accurate that UnitsNet
        Assert.Equal(expectedYards, actual);
        Assert.Equal(expectedYards, nActual.Value, 14);

        // Benefit of the doubt, let's check UnitsNet default answer: m
        const Double expectedMetre = 14.6304; // 16×0.9144 using full precision math
        Length metres = actual.To(Si<Metre>());
        Assert.Equal(expectedMetre, metres); // is accurate
        Assert.Equal(expectedMetre, nActualInMeters.Value, 14); // is less accurate
    }

    [Fact]
    public void GramToPound()
    {
        const Double expectedPounds = 3d;
        const Double gramsInPound = 453.59237; // this is the definition of the pound in grams...

        // Quantities
        Mass mass = Mass.Of(expectedPounds * gramsInPound, Metric<Gram>());
        Mass actual = mass.To(Imperial<Pound>());

        // UnitsNet
        nMass nMass = nMass.FromGrams(expectedPounds * gramsInPound);
        nMass nActual = nMass.ToUnit(UnitsNet.Units.MassUnit.Pound);

        // Quantities is marginally more accurate that UnitsNet
        Assert.Equal(expectedPounds, actual);
        Assert.Equal(expectedPounds, nActual.Value, 15);
    }
}
