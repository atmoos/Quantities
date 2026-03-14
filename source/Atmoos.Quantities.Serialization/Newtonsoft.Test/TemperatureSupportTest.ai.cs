using Atmoos.Quantities.Units.Imperial.Temperature;
using Atmoos.Quantities.Units.NonStandard.Temperature;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class TemperatureSupportTest : ISerializationTester<Temperature>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Temperature quantity) => quantity.SupportsSerialization();

    public static TheoryData<Temperature> Quantities() => [
            Temperature.Of(21, Si<Kelvin>()),
            Temperature.Of(342, Si<Nano, Kelvin>()),
            Temperature.Of(6, Metric<Celsius>()),
            Temperature.Of(-41, Metric<Milli, Celsius>()),
            Temperature.Of(1.21, Imperial<Fahrenheit>()),
            Temperature.Of(121, Imperial<GasMark>()),
            Temperature.Of(95.2, NonStandard<Delisle>()),
        ];
}
