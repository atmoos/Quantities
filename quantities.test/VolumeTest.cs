using Quantities.Units.Imperial.Volume;
using Quantities.Units.Si.Metric;

using static Quantities.Test.Convenience;

namespace Quantities.Test;

public sealed class VolumeTest
{
    [Fact]
    public void AddCubicMetres()
    {
        Volume left = Volume.Of(20).Cubic.Si<Metre>();
        Volume right = Volume.Of(10).Cubic.Si<Metre>();
        Volume expected = Volume.Of(30).Cubic.Si<Metre>();

        Volume actual = left + right;

        actual.Matches(expected);
    }
    [Fact]
    public void FromSiToLitre()
    {
        Volume si = Volume.Of(1).Cubic.Si<Metre>();
        Volume expected = Volume.Of(1000).Metric<Litre>();

        Volume actual = si.To.Metric<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromSiToHectoLitre()
    {
        Volume si = Volume.Of(1).Cubic.Si<Metre>();
        Volume expected = Volume.Of(10).Metric<Hecto, Litre>();

        Volume actual = si.To.Metric<Hecto, Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromComputedSiToHectoLitre()
    {
        Volume computedSi = Area.Of(0.5).Square.Si<Metre>() * Length.Of(2).Si<Metre>();
        Volume expected = Volume.Of(10).Metric<Hecto, Litre>();

        Volume actual = computedSi.To.Metric<Hecto, Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromMilliLitreToCubicCentimetre()
    {
        Volume litre = Volume.Of(1).Metric<Milli, Litre>();
        Volume expected = Volume.Of(1).Cubic.Si<Centi, Metre>();

        Volume actual = litre.To.Cubic.Si<Centi, Metre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicMillimetreToMicroLitre()
    {
        Volume si = Volume.Of(5).Cubic.Si<Milli, Metre>();
        Volume expected = Volume.Of(5).Metric<Micro, Litre>();

        Volume actual = si.To.Metric<Micro, Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLitreToSi()
    {
        Volume litre = Volume.Of(600).Metric<Litre>();
        Volume expected = Volume.Of(0.6).Cubic.Si<Metre>();

        Volume actual = litre.To.Cubic.Si<Metre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicDeciMetreToLitre()
    {
        Volume si = Volume.Of(1).Cubic.Si<Deci, Metre>();
        Volume expected = Volume.Of(1).Metric<Litre>();

        Volume actual = si.To.Metric<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromGallonToLitre()
    {
        Volume si = Volume.Of(1).Imperial<Gallon>();
        Volume expected = Volume.Of(4.54609).Metric<Litre>();

        Volume actual = si.To.Metric<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLitreToPint()
    {
        Volume si = Volume.Of(0.56826125).Metric<Litre>();
        Volume expected = Volume.Of(1).Imperial<Pint>();

        Volume actual = si.To.Imperial<Pint>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromCubicFootToLitre()
    {
        Volume imperial = Volume.Of(1).Cubic.Imperial<Foot>();
        Volume expected = Volume.Of(28.316846592).Metric<Litre>();

        Volume actual = imperial.To.Metric<Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromMillilitreToImperialFluidOunce()
    {
        Volume si = Volume.Of(2 * 28.4130625).Metric<Milli, Litre>();
        Volume expected = Volume.Of(2).Imperial<FluidOunce>();

        Volume actual = si.To.Imperial<FluidOunce>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromStereToCubicMetre()
    {
        Volume stere = Volume.Of(3).Metric<Stere>();
        Volume expected = Volume.Of(3).Cubic.Si<Metre>();

        Volume actual = stere.To.Cubic.Si<Metre>();
        actual.Matches(expected);
    }
    [Fact]
    public void FromLambdaToMicroLitre()
    {
        Volume lambda = Volume.Of(2).Metric<Lambda>();
        Volume expected = Volume.Of(2).Metric<Micro, Litre>();

        Volume actual = lambda.To.Metric<Micro, Litre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void FromLambdaToCubicMilliMetre()
    {
        Volume lambda = Volume.Of(5).Metric<Lambda>();
        Volume expected = Volume.Of(5).Cubic.Si<Milli, Metre>();

        Volume actual = lambda.To.Cubic.Si<Milli, Metre>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void DivideCubicMetreByMetre()
    {
        Volume volume = Volume.Of(81).Cubic.Si<Metre>();
        Length length = Length.Of(3).Si<Metre>();
        Area expected = Area.Of(27).Square.Si<Metre>();

        Area actual = volume / length;

        actual.Matches(expected);
    }
    [Fact(Skip = WorkOnDimensionalityNeeded)]
    public void DividePureVolumeByLength()
    {
        Volume volume = Volume.Of(300).Metric<Hecto, Litre>();
        Length length = Length.Of(5).Si<Metre>();
        Area expected = Area.Of(6).Square.Si<Metre>();

        Area actual = volume / length;

        actual.Matches(expected);
    }
}
