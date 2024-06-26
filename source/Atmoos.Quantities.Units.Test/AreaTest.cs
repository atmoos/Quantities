﻿using Atmoos.Quantities.Units.Imperial.Area;
using Atmoos.Quantities.Units.NonStandard.Area;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

public class AreaTest
{
    [Fact]
    public void AddSquareMetres()
    {
        Area left = Area.Of(20, Square(Si<Metre>()));
        Area right = Area.Of(10, Square(Si<Metre>()));
        Area result = left + right;
        PrecisionIsBounded(30d, result);
    }
    [Fact]
    public void AddSquareHectoMetresToSquareKiloMetres()
    {
        Area left = Area.Of(2, Square(Si<Kilo, Metre>()));
        Area right = Area.Of(50, Square(Si<Hecto, Metre>()));
        Area result = left + right;
        PrecisionIsBounded(2.5d, result);
    }
    [Fact]
    public void SquareMetresToSquareKilometres()
    {
        Area squareMetres = Area.Of(1000, Square(Si<Metre>()));
        Area squareKilometres = squareMetres.To(Square(Si<Kilo, Metre>()));
        PrecisionIsBounded(1e-3d, squareKilometres);
    }
    [Fact]
    public void SquareMilesToSquareKilometres()
    {
        Area squareMiles = Area.Of(2, Square(Imperial<Mile>()));
        Area expected = Area.Of(5.179976220672, Square(Si<Kilo, Metre>()));

        Area actual = squareMiles.To(Square(Si<Kilo, Metre>()));

        actual.Matches(expected);
    }

    [Fact]
    public void SquareYardToSquareFeet()
    {
        Area squareYards = Area.Of(3, Square(Imperial<Yard>()));
        Area expected = Area.Of(27, Square(Imperial<Foot>()));

        Area actual = squareYards.To(Square(Imperial<Foot>()));

        actual.Matches(expected);
    }

    [Fact]
    public void SquareMetresDividedByMetre()
    {
        Area area = Area.Of(48.40, Square(Si<Deca, Metre>()));
        Length length = Length.Of(605, Si<Metre>());
        Length expected = Length.Of(0.8, Si<Deca, Metre>());

        Length actual = area / length;

        actual.Matches(expected);
    }
    [Fact]
    public void PureArealDimensionDividedByLength()
    {
        Area area = Area.Of(2, Imperial<Acre>());
        Length length = Length.Of(1815, Imperial<Foot>());
        Length expected = Length.Of(16, Imperial<Yard>());

        Length actual = area / length;

        actual.Matches(expected);
    }
    [Fact]
    public void SquareYardsDividedByFeet()
    {
        Area area = Area.Of(27, Square(Imperial<Foot>()));
        Length length = Length.Of(1, Imperial<Yard>());
        Length expected = Length.Of(9, Imperial<Foot>());

        Length actual = area / length;

        actual.Matches(expected);
    }

    [Fact]
    public void AcreDividedBySquareFeet()
    {
        Area acres = Area.Of(2, Imperial<Acre>());
        Area squareFeet = Area.Of(2 * 43560, Square(Imperial<Foot>()));

        Assert.Equal(acres, squareFeet);
    }
    [Fact]
    public void SquareMetersTimesMetres()
    {
        Area area = Area.Of(27, Square(Si<Metre>()));
        Length length = Length.Of(30, Si<Deci, Metre>());
        Volume expected = Volume.Of(81, Cubic(Si<Metre>()));

        Volume actual = area * length;

        actual.Matches(expected);
    }
    [Fact]
    public void SquareFeetTimesYards()
    {
        Area area = Area.Of(27, Square(Imperial<Foot>()));
        Length length = Length.Of(2, Imperial<Yard>());
        Volume expected = Volume.Of(162, Cubic(Imperial<Foot>()));

        Volume actual = area * length;

        actual.Matches(expected);
    }

    [Fact]
    public void AreTimesMeterIsCubicMetre()
    {
        Area area = Area.Of(1, Metric<Are>());
        Length length = Length.Of(10, Si<Metre>());
        Volume expected = Volume.Of(10 * 10 * 10, Cubic(Si<Metre>()));

        Volume actual = area * length;

        actual.Matches(expected);
    }

    [Fact]
    public void AreToSiDefinition()
    {
        Area area = Area.Of(1, Metric<Are>());
        Area expected = Area.Of(100, Square(Si<Metre>()));

        Area actual = area.To(Square(Si<Metre>()));

        actual.Matches(expected);
    }
    [Fact]
    public void AreToHectare()
    {
        Area area = Area.Of(100, Metric<Are>());
        Area expected = Area.Of(1, Metric<Hecto, Are>());

        Area actual = area.To(Metric<Hecto, Are>());

        actual.Matches(expected);
    }
    [Fact]
    public void MorgenToHectare()
    {
        Area morgen = Area.Of(4, NonStandard<Morgen>());
        Area expected = Area.Of(1, Metric<Hecto, Are>());

        Area actual = morgen.To(Metric<Hecto, Are>());

        actual.Matches(expected);
    }
    [Fact]
    public void MorgenToSquareMetre()
    {
        Area morgen = Area.Of(2, NonStandard<Morgen>());
        Area expected = Area.Of(5000, Square(Si<Metre>()));

        Area actual = morgen.To(Square(Si<Metre>()));

        actual.Matches(expected);
    }
    [Fact]
    public void RoodToPerches()
    {
        Area rood = Area.Of(1, Imperial<Rood>());
        Area expected = Area.Of(40, Imperial<Perch>());

        Area actual = rood.To(Imperial<Perch>());

        actual.Matches(expected);
    }
}
