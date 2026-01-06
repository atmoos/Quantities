using Atmoos.Quantities.Units.Imperial.Temperature;
using Atmoos.Quantities.Units.NonStandard.Temperature;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class TemperatureSupportTest : ISerializationTester<Temperature>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Temperature quantity) => quantity.SupportsSerialization();

    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Temperature> Interesting()
        {
            yield return Temperature.Of(21, Si<Kelvin>());
            yield return Temperature.Of(342, Si<Nano, Kelvin>());
            yield return Temperature.Of(6, Metric<Celsius>());
            yield return Temperature.Of(-41, Metric<Milli, Celsius>());
            yield return Temperature.Of(1.21, Imperial<Fahrenheit>());
            yield return Temperature.Of(121, Imperial<GasMark>());
            yield return Temperature.Of(95.2, NonStandard<Delisle>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
