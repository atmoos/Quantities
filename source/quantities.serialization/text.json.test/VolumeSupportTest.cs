using Quantities.Units.Imperial.Length;
using Quantities.Units.Imperial.Volume;

namespace Quantities.Serialization.Text.Json.Text;

public class VolumeSupportTest : ISerializationTester<Volume>, IInjectedUnitTester<Volume>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Volume volume) => volume.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
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


    [Theory]
    [MemberData(nameof(InjectingVolumes))]
    public void DeserializationSupportsInjectedUnits(Volume quantity)
    {
        // when injected units are not respected during deserialization,
        // division results in very different values when using
        // a volume created with the standard API or one that was
        // created using deserialization.
        var someArea = Area.Of(2).Square.Si<Metre>();
        var expectedLength = quantity / someArea;

        var deserializedVolume = quantity.SerializeRoundRobin();
        var actualLength = deserializedVolume / someArea;

        Assert.Equal(expectedLength, actualLength);
    }

    public static IEnumerable<Object[]> InjectingVolumes()
    {
        static IEnumerable<Volume> Interesting()
        {
            yield return Volume.Of(1).Metric<Litre>();
            yield return Volume.Of(2).Metric<Stere>();
            yield return Volume.Of(3).Metric<Lambda>();
            yield return Volume.Of(4).Imperial<FluidOunce>();
            yield return Volume.Of(5).Imperial<Gill>();
            yield return Volume.Of(6).Imperial<Pint>();
            yield return Volume.Of(7).Imperial<Quart>();
            yield return Volume.Of(8).Imperial<Gallon>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
