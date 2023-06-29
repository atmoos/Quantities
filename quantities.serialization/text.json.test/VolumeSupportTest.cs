using Quantities.Units.Imperial.Length;
using Quantities.Units.Imperial.Volume;

namespace Quantities.Serialization.Text.Json.Text;

public class VolumeSupportTest : ISerializationTester<Volume>
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
}
