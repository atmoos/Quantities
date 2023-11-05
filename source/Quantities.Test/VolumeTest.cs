using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units.Imperial.Volume;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class VolumeTest
{
    private static readonly Alias<Litre> litre = AliasOf<ILength>.Metric<Litre>();
    private static readonly Alias<Litre> milliLitre = AliasOf<ILength>.Metric<Milli, Litre>();
    private static readonly Alias<Litre> hectoLitre = AliasOf<ILength>.Metric<Hecto, Litre>();
    [Fact]
    public void AddCubicMetres()
    {
        Volume left = Volume.Of(20, Cubic(Si<Metre>()));
        Volume right = Volume.Of(10, Cubic(Si<Metre>()));
        Volume expected = Volume.Of(30, Cubic(Si<Metre>()));

        Volume actual = left + right;

        actual.Matches(expected);
    }
    [Fact]
    public void FromSiToLitre()
    {
        Volume si = Volume.Of(1, Cubic(Si<Metre>()));
        Volume expected = Volume.Of(1000, litre);

        Volume actual = si.To(litre);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromSiToHectoLitre()
    {
        Volume si = Volume.Of(1, Cubic(Si<Metre>()));
        Volume expected = Volume.Of(10, hectoLitre);

        Volume actual = si.To(hectoLitre);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromComputedSiToHectoLitre()
    {
        Volume computedSi = Area.Of(0.5, Square(Si<Metre>())) * Length.Of(2, Si<Metre>());
        Volume expected = Volume.Of(10, hectoLitre);

        Volume actual = computedSi.To(hectoLitre);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromMilliLitreToCubicCentimetre()
    {
        Volume litre = Volume.Of(1, milliLitre);
        Volume expected = Volume.Of(1, Cubic(Si<Centi, Metre>()));

        Volume actual = litre.To(Cubic(Si<Centi, Metre>()));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicMillimetreToMicroLitre()
    {
        var microLitre = AliasOf<ILength>.Metric<Micro, Litre>();
        Volume si = Volume.Of(5, Cubic(Si<Milli, Metre>()));
        Volume expected = Volume.Of(5, microLitre);

        Volume actual = si.To(microLitre);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLitreToSi()
    {
        Volume litres = Volume.Of(600, litre);
        Volume expected = Volume.Of(0.6, Cubic(Si<Metre>()));

        Volume actual = litres.To(Cubic(Si<Metre>()));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicDeciMetreToLitre()
    {
        Volume si = Volume.Of(1, Cubic(Si<Deci, Metre>()));
        Volume expected = Volume.Of(1, litre);

        Volume actual = si.To(litre);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromGallonToLitre()
    {
        Volume si = Volume.Of(1, AliasOf<ILength>.Imperial<Gallon>());
        Volume expected = Volume.Of(4.54609, litre);

        Volume actual = si.To(litre);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLitreToPint()
    {
        Volume si = Volume.Of(0.56826125, litre);
        Volume expected = Volume.Of(1, AliasOf<ILength>.Imperial<Pint>());

        Volume actual = si.To(AliasOf<ILength>.Imperial<Pint>());
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicFootToLitre()
    {
        Volume imperial = Volume.Of(1, Cubic(Imperial<Foot>()));
        Volume expected = Volume.Of(28.316846592, litre);

        Volume actual = imperial.To(litre);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromMillilitreToImperialFluidOunce()
    {
        Volume si = Volume.Of(2 * 28.4130625, milliLitre);
        Volume expected = Volume.Of(2, AliasOf<ILength>.Imperial<FluidOunce>());

        Volume actual = si.To(AliasOf<ILength>.Imperial<FluidOunce>());
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromStereToCubicMetre()
    {
        Volume stere = Volume.Of(3, AliasOf<ILength>.Metric<Stere>());
        Volume expected = Volume.Of(3, Cubic(Si<Metre>()));

        Volume actual = stere.To(Cubic(Si<Metre>()));
        actual.Matches(expected);
    }
    [Fact]
    public void FromLambdaToMicroLitre()
    {
        Volume lambda = Volume.Of(2, AliasOf<ILength>.Metric<Lambda>());
        Volume expected = Volume.Of(2, AliasOf<ILength>.Metric<Micro, Litre>());

        Volume actual = lambda.To(AliasOf<ILength>.Metric<Micro, Litre>());
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLambdaToCubicMilliMetre()
    {
        Volume lambda = Volume.Of(5, AliasOf<ILength>.Metric<Lambda>());
        Volume expected = Volume.Of(5, Cubic(Si<Milli, Metre>()));

        Volume actual = lambda.To(Cubic(Si<Milli, Metre>()));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void DivideCubicMetreByMetre()
    {
        Volume volume = Volume.Of(81, Cubic(Si<Metre>()));
        Length length = Length.Of(3, Si<Metre>());
        Area expected = Area.Of(27, Square(Si<Metre>()));

        Area actual = volume / length;

        actual.Matches(expected);
    }
    [Fact]
    public void DividePureVolumeByLength()
    {
        Volume volume = Volume.Of(300, hectoLitre);
        Length length = Length.Of(5, Si<Metre>());
        Area expected = Area.Of(6, Square(Si<Metre>()));

        Area actual = volume / length;

        actual.Matches(expected);
    }
}
