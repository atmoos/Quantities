using Quantities.Unit.Imperial.Area;
namespace Quantities.Test;
public class AreaTest
{
    private const Double SQUARE_MILE_IN_SQUARE_KILOMETRES = (Double)(1.609344m * 1.609344m);

    [Fact]
    public void AddSquareMetres()
    {
        Area left = Area.Square<Metre>(20);
        Area right = Area.Square<Metre>(10);
        Area result = left + right;
        Assert.Equal(30d, result, SiPrecision);
    }
    [Fact]
    public void AddSquareHectoMetresToSquareKiloMetres()
    {
        Area left = Area.Square<Kilo, Metre>(2);
        Area right = Area.Square<Hecto, Metre>(50);
        Area result = left + right;
        Assert.Equal(2.5d, result, SiPrecision);
    }
    [Fact]
    public void SquareMetresToSquareKilometers()
    {
        Area squareMetres = Area.Square<Metre>(1000);
        Area squarekilometres = squareMetres.ToSquare<Kilo, Metre>();
        Assert.Equal(1e-3d, squarekilometres, SiPrecision);
    }
    [Fact]
    public void SquareMilesToSquareKilometers()
    {
        Area squareMiles = Area.SquareImperial<Mile>(2);
        Area expected = Area.Square<Kilo, Metre>(2 * SQUARE_MILE_IN_SQUARE_KILOMETRES);

        Area actual = squareMiles.ToSquare<Kilo, Metre>();

        actual.Matches(expected);
    }

    [Fact]
    public void SquareYardToSquareFeet()
    {
        Area squareYards = Area.SquareImperial<Yard>(3);
        Area expected = Area.SquareImperial<Foot>(27);

        Area actual = squareYards.ToSquareImperial<Foot>();

        actual.Matches(expected, ImperialPrecision);
    }

    [Fact]
    public void SquareMetresDividedByMetre()
    {
        Area area = Area.Square<Deca, Metre>(48.40);
        Length length = Length.Si<Metre>(605);
        Length expected = Length.Si<Deca, Metre>(0.8);

        Length actual = area / length;

        actual.Matches(expected);
    }
    [Fact]
    public void PureArealDimensionDividedByLength()
    {
        Area area = Area.Imperial<Acre>(2);
        Length length = Length.Imperial<Foot>(1815);
        Length expected = Length.Imperial<Yard>(16);

        Length actual = area / length;

        actual.Matches(expected);
    }
    [Fact]
    public void SquareYardsDividedByFeet()
    {
        Area area = Area.SquareImperial<Foot>(27);
        Length length = Length.Imperial<Yard>(1);
        Length expected = Length.Imperial<Foot>(9);

        Length actual = area / length;

        actual.Matches(expected);
    }

    [Fact]
    public void AcreDividedBySquareFeet()
    {
        Area acres = Area.Imperial<Acre>(2);
        Area squareFeet = Area.SquareImperial<Foot>(2 * 43560);

        Assert.Equal(acres, squareFeet);
    }
    [Fact]
    public void SquareMetersTimesMetres()
    {
        Area area = Area.Square<Metre>(27);
        Length length = Length.Si<Deci, Metre>(30);
        Volume expected = Volume.Cubic<Metre>(81);

        Volume actual = area * length;

        actual.Matches(expected);
    }
    [Fact]
    public void SquareFeetTimesYards()
    {
        Area area = Area.SquareImperial<Foot>(27);
        Length length = Length.Imperial<Yard>(2);
        Volume expected = Volume.CubicImperial<Foot>(162);

        Volume actual = area * length;

        actual.Matches(expected);
    }
}
