using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Imperial.Volume;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class VolumeSupportTest : ISerializationTester<Volume>
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
}
