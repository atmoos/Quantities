using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Imperial.Volume;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class VolumeSupportTest : ISerializationTester<Volume>, IInjectedUnitTester<Volume>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Volume volume) => volume.SupportsSerialization();

    public static TheoryData<Volume> Quantities() => [
            Volume.Of(21, Metric<Litre>()),
            Volume.Of(243, Metric<Micro, Litre>()),
            Volume.Of(342, Metric<Hecto, Litre>()),
            Volume.Of(6, Imperial<Pint>()),
            Volume.Of(3, Imperial<FluidOunce>()),
            Volume.Of(9, Imperial<Gallon>()),
            Volume.Of(9, Imperial<Quart>()),
            Volume.Of(-41, Cubic(Si<Metre>())),
            Volume.Of(1.21, Cubic(Si<Pico, Metre>())),
            Volume.Of(121, Cubic(Si<Kilo, Metre>())),
            Volume.Of(95.2, Cubic(Metric<AstronomicalUnit>())),
            Volume.Of(-11, Cubic(Imperial<Yard>())),
            Volume.Of(9, Cubic(Imperial<Foot>())),
        ];

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

    public static TheoryData<Volume> InjectingVolumes() => [
            Volume.Of(1, Metric<Litre>()),
            Volume.Of(2, Metric<Stere>()),
            Volume.Of(3, Metric<Lambda>()),
            Volume.Of(4, Imperial<FluidOunce>()),
            Volume.Of(5, Imperial<Gill>()),
            Volume.Of(6, Imperial<Pint>()),
            Volume.Of(7, Imperial<Quart>()),
            Volume.Of(8, Imperial<Gallon>()),
        ];
}
