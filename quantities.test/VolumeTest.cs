using Quantities.Units.Imperial.Volume;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class VolumeTest
{
    [Fact]
    public void AddCubicMetres()
    {
        Volume left = Volume.Of(20).Cubic.Si<Metre>();
        Volume right = Volume.Of(10).Cubic.Si<Metre>();
        Volume result = left + right;
        PrecisionIsBounded(30d, result);
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
    public void DivideCubicMetreByMetre()
    {
        Volume volume = Volume.Of(81).Cubic.Si<Metre>();
        Length length = Length.Of(3).Si<Metre>();
        Area expected = Area.Of(27).Square.Si<Metre>();

        Area actual = volume / length;

        actual.Matches(expected);
    }
    [Fact]
    public void DividePureVolumeByLength()
    {
        Volume volume = Volume.Of(300).Metric<Hecto, Litre>();
        Length length = Length.Of(5).Si<Metre>();
        Area expected = Area.Of(6).Square.Si<Metre>();

        Area actual = volume / length;

        actual.Matches(expected);
    }
    [Theory]
    [MemberData(nameof(Volumes))]
    public void VolumeSupportsSerialization(Volume volume) => volume.SupportsSerialization().Quant.HasSameMeasure(volume.Quant);

    public static IEnumerable<Object[]> Volumes()
    {
        static IEnumerable<Volume> Interesting()
        {
            yield return Volume.Of(21).Metric<Litre>();
            yield return Volume.Of(243).Metric<Micro, Litre>();
            yield return Volume.Of(342).Metric<Hecto, Litre>();
            yield return Volume.Of(6).Imperial<Pint>();
            yield return Volume.Of(3).Imperial<FluidOunce>();
            yield return Volume.Of(9).Imperial<Gallon>();
            yield return Volume.Of(9).Imperial<Quart>();
            yield return Volume.Of(-41).Cubic.Si<Metre>();
            yield return Volume.Of(1.21).Cubic.Si<Pico, Metre>();
            yield return Volume.Of(121).Cubic.Si<Kilo, Metre>();
            yield return Volume.Of(95.2).Cubic.Metric<AstronomicalUnit>();
            yield return Volume.Of(-11).Cubic.Imperial<Yard>();
            yield return Volume.Of(9).Cubic.Imperial<Foot>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
