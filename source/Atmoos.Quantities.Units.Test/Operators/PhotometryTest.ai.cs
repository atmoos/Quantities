using Atmoos.Quantities;
using Atmoos.Quantities.Units.Si.Derived.Illuminance;
using Atmoos.Quantities.Units.Si.Derived.LuminousFlux;

namespace Atmoos.Quantities.Units.Test.Operators;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class PhotometryTest
{
    [Fact]
    public void LuminousFluxDividedByAreaYieldsIlluminance()
    {
        LuminousFlux flux = LuminousFlux.Of(12, Si<Lumen>());
        Area area = Area.Of(3, Square(Si<Metre>()));
        Illuminance expected = Illuminance.Of(4, Si<Lux>());

        Illuminance actual = flux / area;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IlluminanceTimesAreaYieldsLuminousFlux()
    {
        Illuminance illuminance = Illuminance.Of(4, Si<Lux>());
        Area area = Area.Of(3, Square(Si<Metre>()));
        LuminousFlux expected = LuminousFlux.Of(12, Si<Lumen>());

        LuminousFlux actual = illuminance * area;

        Assert.Equal(expected, actual);
    }
}
