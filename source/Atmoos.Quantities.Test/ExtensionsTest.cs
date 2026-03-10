using Atmoos.Quantities.Physics;
using Atmoos.Quantities.Units.Si.Metric;
using static System.Math;
using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Test;

public class ExtensionsTest
{
    [Fact]
    public void DeconstructionOfScalarQuantities()
    {
        Double expectedValue = Tau;
        Length length = Length.Of(in expectedValue, in Si<Kilo, Metre>());

        (Double value, String unit) = length;

        Assert.Equal(expectedValue, value);
        Assert.Equal("km", unit);
    }

    [Theory]
    [MemberData(nameof(Velocities))]
    public void DeconstructionOfCompoundQuotientQuantities(Velocity velocity, String expectedUnit)
    {
        (Double value, String unit) = velocity;

        Assert.Equal(velocity.Value, value);
        Assert.Equal(expectedUnit, unit);
    }

    [Theory]
    [MemberData(nameof(Volumes))]
    public void DeconstructionOfCompoundMultiplicativeQuantities(Volume volume, String expectedUnit)
    {
        (Double value, String unit) = volume;

        Assert.Equal(volume.Value, value);
        Assert.Equal(expectedUnit, unit);
    }

    public static TheoryData<Velocity, String> Velocities()
        => new() {
            { Velocity.Of(E, Si<Metre>().Per(Si<Second>())), "m/s" },
            { Velocity.Of(PI, Si<Kilo, Metre>().Per(Metric<Hour>())), "km/h" },
            { Velocity.Of(Tau, Si<Deci, Metre>().Per(Metric<Minute>())), "dm/min" },
            { Velocity.Of(E / PI, Si<Milli, Metre>().Per(Metric<Hour>())), "mm/h" },
            { Velocity.Of(E / Tau, Imperial<Mile>().Per(Metric<Hour>())), "mi/h" }
        };

    public static TheoryData<Volume, String> Volumes()
        => new() {
            { Volume.Of(E, Cubic(in Si<Metre>())), "m³" },
            { Volume.Of(PI, Cubic(in Si<Kilo, Metre>())), "km³" },
            { Volume.Of(E / Tau, Cubic(in Imperial<Foot>())), "ft³" },
            { Area.Of(3, Square(Si<Metre>())) * Length.Of(2, Imperial<Foot>()), "m³" }
        };

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void ValueOfReturnsConversionFactor()
    {
        Double actual = ValueOf<Kilo>();

        Assert.Equal(1000.0, actual);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void ValueOfWithExponentReturnsScaledFactor()
    {
        Double actual = ValueOf<Kilo>(2);

        Assert.Equal(1_000_000.0, actual);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void ValueOfWithNegativeExponentReturnsInverse()
    {
        Double actual = ValueOf<Kilo>(-1);

        Assert.Equal(0.001, actual, precision: 15);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void ToStringUsesInvariantCulture()
    {
        Length length = Length.Of(1234.5, Si<Metre>());
        IFormattable formattable = length;

        String actual = formattable.ToString("G4");

        Assert.DoesNotContain(",", actual);
        Assert.Contains("1234", actual);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void NotImplementedContainsTypeName()
    {
        var instance = new Object();

        NotImplementedException exception = NotImplemented(instance);

        Assert.Contains("Object", exception.Message);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void GenericNotImplementedContainsTypeName()
    {
        NotImplementedException exception = NotImplemented<Length>();

        Assert.Contains("Length", exception.Message);
    }

    [Fact]
    [Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
    public void DeconstructionOfNegativeValuePreservesSign()
    {
        Double expectedValue = -42.5;
        Length length = Length.Of(in expectedValue, in Si<Metre>());

        (Double value, String unit) = length;

        Assert.Equal(expectedValue, value);
        Assert.Equal("m", unit);
    }
}
