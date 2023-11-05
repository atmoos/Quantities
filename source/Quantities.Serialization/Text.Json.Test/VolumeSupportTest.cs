using Quantities.Units.Imperial.Length;
using Quantities.Units.Imperial.Volume;
using In = Quantities.AliasOf<Quantities.Dimensions.ILength>;
namespace Quantities.Serialization.Text.Json.Test;

public class VolumeSupportTest : ISerializationTester<Volume>, IInjectedUnitTester<Volume>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Volume volume) => volume.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Volume> Interesting()
        {
            yield return Volume.Of(21, In.Metric<Litre>());
            yield return Volume.Of(243, In.Metric<Micro, Litre>());
            yield return Volume.Of(342, In.Metric<Hecto, Litre>());
            yield return Volume.Of(6, In.Imperial<Pint>());
            yield return Volume.Of(3, In.Imperial<FluidOunce>());
            yield return Volume.Of(9, In.Imperial<Gallon>());
            yield return Volume.Of(9, In.Imperial<Quart>());
            yield return Volume.Of(-41, Cubic(Si<Metre>()));
            yield return Volume.Of(1.21, Cubic(Si<Pico, Metre>()));
            yield return Volume.Of(121, Cubic(Si<Kilo, Metre>()));
            yield return Volume.Of(95.2, Cubic(Metric<AstronomicalUnit>()));
            yield return Volume.Of(-11, Cubic(Imperial<Yard>()));
            yield return Volume.Of(9, Cubic(Imperial<Foot>()));
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
        var someArea = Area.Of(2, Square(Si<Metre>()));
        var expectedLength = quantity / someArea;

        var deserializedVolume = quantity.SerializeRoundRobin();
        var actualLength = deserializedVolume / someArea;

        Assert.Equal(expectedLength, actualLength);
    }

    public static IEnumerable<Object[]> InjectingVolumes()
    {
        static IEnumerable<Volume> Interesting()
        {
            yield return Volume.Of(1, In.Metric<Litre>());
            yield return Volume.Of(2, In.Metric<Stere>());
            yield return Volume.Of(3, In.Metric<Lambda>());
            yield return Volume.Of(4, In.Imperial<FluidOunce>());
            yield return Volume.Of(5, In.Imperial<Gill>());
            yield return Volume.Of(6, In.Imperial<Pint>());
            yield return Volume.Of(7, In.Imperial<Quart>());
            yield return Volume.Of(8, In.Imperial<Gallon>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
