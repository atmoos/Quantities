using Quantities.Unit.Imperial.Volume;
using Quantities.Unit.Si.Accepted;

namespace Quantities.Test;

public sealed class VolumeTest
{
    [Fact]
    public void AddCubicMetres()
    {
        Volume left = Volume.Cubic<Metre>(20);
        Volume right = Volume.Cubic<Metre>(10);
        Volume result = left + right;
        Assert.Equal(30d, result, SiPrecision);
    }
    [Fact]
    public void FromSiToLitre()
    {
        Volume si = Volume.Cubic<Metre>(1);
        Volume expected = Volume.Si<Litre>(1000);

        Volume actual = si.ToSi<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromSiToHectoLitre()
    {
        Volume si = Volume.Cubic<Metre>(1);
        Volume expected = Volume.Si<Hecto, Litre>(10);

        Volume actual = si.ToSi<Hecto, Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromMilliLitreToCubicCentimetre()
    {
        Volume litre = Volume.Si<Milli, Litre>(1);
        Volume expected = Volume.Cubic<Centi, Metre>(1);

        Volume actual = litre.ToCubic<Centi, Metre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicMillimetreToMicroLitre()
    {
        Volume si = Volume.Cubic<Milli, Metre>(5);
        Volume expected = Volume.Si<Micro, Litre>(5);

        Volume actual = si.ToSi<Micro, Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLitreToSi()
    {
        Volume litre = Volume.Si<Litre>(600);
        Volume expected = Volume.Cubic<Metre>(0.6);

        Volume actual = litre.ToCubic<Metre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicDeciMetreToLitre()
    {
        Volume si = Volume.Cubic<Deci, Metre>(1);
        Volume expected = Volume.Si<Litre>(1);

        Volume actual = si.ToSi<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromGallonToLitre()
    {
        Volume si = Volume.Imperial<Gallon>(1);
        Volume expected = Volume.Si<Litre>(4.54609);

        Volume actual = si.ToSi<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLitreToPint()
    {
        Volume si = Volume.Si<Litre>(0.56826125);
        Volume expected = Volume.Imperial<Pint>(1);

        Volume actual = si.ToImperial<Pint>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicFootToLitre()
    {
        Volume imperial = Volume.CubicImperial<Foot>(1);
        Volume expected = Volume.Si<Litre>(28.316846592);

        Volume actual = imperial.ToSi<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromMillilitreToImperialFluidOunce()
    {
        Volume si = Volume.Si<Milli, Litre>(2 * 28.4130625);
        Volume expected = Volume.Imperial<FluidOunce>(2);

        Volume actual = si.ToImperial<FluidOunce>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void DivideCubicMetreByMetre()
    {
        Volume volume = Volume.Cubic<Metre>(81);
        Length length = Length.Si<Metre>(3);
        Area expected = Area.Square<Metre>(27);

        Area actual = volume / length;

        actual.Matches(expected);
    }
    [Fact]
    public void DividePureVolumeByLength()
    {
        Volume volume = Volume.Si<Hecto, Litre>(300);
        Length length = Length.Si<Metre>(5);
        Area expected = Area.Square<Metre>(6);

        Area actual = volume / length;

        actual.Matches(expected);
    }
}
