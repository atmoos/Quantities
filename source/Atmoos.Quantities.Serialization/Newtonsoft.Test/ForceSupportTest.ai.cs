using Atmoos.Quantities.Units.Imperial.Force;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

public class ForceSupportTest : ISerializationTester<Force>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Force quantity) => quantity.SupportsSerialization();

    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Force> Interesting()
        {
            yield return Force.Of(21, Si<Newton>());
            yield return Force.Of(342, Si<Kilo, Newton>());
            yield return Force.Of(6, Si<Milli, Newton>());
            yield return Force.Of(-41, Imperial<PoundForce>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
