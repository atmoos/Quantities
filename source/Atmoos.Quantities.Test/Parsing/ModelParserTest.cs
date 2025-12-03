using Atmoos.Quantities.Core.Construction;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Parsing;

using static Atmoos.Quantities.Test.Convenience;

namespace Atmoos.Quantities.Test.Parsing;

public abstract class ModelParserTest
{
    protected static readonly UnitRepository repository = UnitRepository.Create();
    protected const String si = "si";
    protected const String metric = "metric";
    protected const String imperial = "imperial";

    protected static readonly QuantityModel s = new() { System = si, Exponent = 1, Unit = "s" };
    protected static readonly QuantityModel h = new() { System = metric, Exponent = 1, Unit = "h" };
    protected static readonly QuantityModel μs = new() { System = si, Exponent = 1, Unit = "s", Prefix = "μ" };
    protected static readonly QuantityModel m = new() { System = si, Exponent = 1, Unit = "m" };
    protected static readonly QuantityModel ft = new() { System = imperial, Exponent = 1, Unit = "ft" };
    protected static readonly QuantityModel mi = new() { System = imperial, Exponent = 1, Unit = "mi" };
    protected static readonly QuantityModel km = new() { System = si, Exponent = 1, Unit = "m", Prefix = "k" };
    protected static String Mul(String left, String right) => Join(left, right, '\u200C');
    protected static String Join(String left, String right, Char joiner) => $"{left}{joiner}{right}";
    private protected static ModelParser CreateParser<TDimension>()
         where TDimension : IDimension
         => new(repository.Subset<TDimension>());

    public sealed class TemperatureChange : IProduct<ITemperature, IDimension<ITime, Negative<One>>> { }
    public sealed class ElectricCapacity : IProduct<IElectricCurrent, ITime> { }
}

public abstract class ModelParserTest<TDimension> : ModelParserTest
    where TDimension : IDimension
{
    private readonly ModelParser parser = CreateParser<TDimension>();
    protected void TestScalarParsing(String unit, QuantityModel expected)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Equal([expected], actual);
    }
    protected void TestCompoundParsing(String unit, IEnumerable<QuantityModel> expected)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Equal(expected, actual);
    }
    protected void ConfirmParsingFails(String unit)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Empty(actual);
    }
}

public sealed class LengthParsingTest : ModelParserTest<Length>
{
    [Theory]
    [MemberData(nameof(Lengths))]
    public void LengthCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    [Theory]
    [MemberData(nameof(NonLengths))]
    public void NonLengthCannotBeParsed(String unit) => ConfirmParsingFails(unit);
    public static TheoryData<String, QuantityModel> Lengths()
        => new() {
                { "m", m },
                { "ft", ft },
                { "km", km },
                { "mm", m with {Prefix = "m"}},
                { "mi", mi },
            };
    public static TheoryData<String> NonLengths() => ToTheoryData("mK", "s", "h", "kg");
}

public sealed class VolumeParsingTest : ModelParserTest<Volume>
{
    [Theory]
    [MemberData(nameof(Volumes))]
    public void VolumeCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    [Theory]
    [MemberData(nameof(NonVolumes))]
    public void NonVolumeCannotBeParsed(String unit) => ConfirmParsingFails(unit);
    public static TheoryData<String, QuantityModel> Volumes()
        => new() {
                { "m³", m with {Exponent = 3} },
                { "ft³", ft with {Exponent = 3} },
                { "km³", km with {Exponent = 3} },
                { "ℓ", new() { System = metric, Exponent = 1, Unit = "ℓ" } },
            };
    public static TheoryData<String> NonVolumes() => ToTheoryData("Pa", "m/s", "kg", "°C");
}

public sealed class VelocityParsingTest : ModelParserTest<Velocity>
{
    [Theory]
    [MemberData(nameof(Velocities))]
    public void VelocityCanBeParsed(String unit, IEnumerable<QuantityModel> expected) => TestCompoundParsing(unit, expected);
    [Theory]
    [MemberData(nameof(NonVelocities))]
    public void NonVelocityCannotBeParsed(String unit) => ConfirmParsingFails(unit);
    public static TheoryData<String, IEnumerable<QuantityModel>> Velocities()
        => new() {
                { "m/s", [m, s with {Exponent = -1} ]},
                { Mul("m","s⁻¹"), [m, s with {Exponent = -1}]},
                { "ft/μs", [ft, new() { System = si, Exponent = -1, Unit = "s", Prefix = "μ" } ]},
                { "km/ms", [km, s with {Exponent = -1, Prefix = "m"} ]},
                { "km/h", [km, h with {Exponent = -1}]},
                { "mi/h", [mi, h with {Exponent = -1}]}
            };
    public static TheoryData<String> NonVelocities() => ToTheoryData("kg", "°C", "A", "μK");
}

public sealed class MassParsingTest : ModelParserTest<Mass>
{
    [Theory]
    [MemberData(nameof(Masses))]
    public void MassCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    [Theory]
    [MemberData(nameof(NonMasses))]
    public void NonMassCannotBeParsed(String unit) => ConfirmParsingFails(unit);
    public static TheoryData<String, QuantityModel> Masses()
        => new() {
                { "kg", new() { System = si, Exponent =1, Unit = "kg" } },
                { "lb", new() { System = imperial, Exponent =1, Unit = "lb"} }
            };
    public static TheoryData<String> NonMasses() => ToTheoryData("J", "mK", "s", "°C", "m/s");
}

public sealed class TimeParsingTest : ModelParserTest<Time>
{
    [Theory]
    [MemberData(nameof(Times))]
    public void TimeCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    [Theory]
    [MemberData(nameof(NonTimes))]
    public void NonTimeCannotBeParsed(String unit) => ConfirmParsingFails(unit);
    public static TheoryData<String, QuantityModel> Times()
        => new() {
                { "s", s },
                { "μs", μs },
                { "h", h }
            };
    public static TheoryData<String> NonTimes() => ToTheoryData("kg", "°C", "mol", "pt", "N");
}

public sealed class TemperatureParsingTest : ModelParserTest<Temperature>
{
    [Theory]
    [MemberData(nameof(Temperatures))]
    public void TemperatureCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    [Theory]
    [MemberData(nameof(NonTemperatures))]
    public void NonTemperatureCannotBeParsed(String unit) => ConfirmParsingFails(unit);
    public static TheoryData<String, QuantityModel> Temperatures()
        => new() {
                { "°C", new() { System = metric, Exponent = 1, Unit = "°C" } },
                { "K", new() { System = si, Exponent = 1, Unit = "K" } },
                { "mK", new() { System = si, Exponent = 1, Unit = "K" , Prefix = "m"} }
            };
    public static TheoryData<String> NonTemperatures() => ToTheoryData("mol", "m", "s", "kg", "m/s", "°C/s", "°C·s", "m/s²");
}

public sealed class AccelerationParsingTest : ModelParserTest<Acceleration>
{
    [Theory]
    [MemberData(nameof(Accelerations))]
    public void AccelerationCanBeParsed(String unit, IEnumerable<QuantityModel> expected) => TestCompoundParsing(unit, expected);
    [Theory]
    [MemberData(nameof(NonAccelerations))]
    public void NonAccelerationCannotBeParsed(String unit) => ConfirmParsingFails(unit);
    public static TheoryData<String, IEnumerable<QuantityModel>> Accelerations()
        => new() {
                { "m/s²", [m, s with {Exponent = -2} ]},
                { Mul("m","s⁻²"), [m, s with {Exponent = -2}]},
                { "ft/μs²", [ft, s with {Exponent = -2, Prefix = "μ" } ]},
                { "mi/h²", [mi, h with {Exponent = -2} ]},
            };
    // ToDo: At the moment the exponent is entirely passive. Hence, "s", "m/s³" & "m/s" can all be parsed here.
    //       Consider letting this fail here by checking exponents in the parser.
    public static TheoryData<String> NonAccelerations() => ToTheoryData("kg", "K", "pt", "mK");
}

public sealed class ElectricCapacityParsingTest : ModelParserTest<ModelParserTest.ElectricCapacity>
{
    [Theory]
    [MemberData(nameof(ElectricCapacities))]
    public void ElectricCapacityCanBeParsed(String unit, IEnumerable<QuantityModel> expected) => TestCompoundParsing(unit, expected);
    public static TheoryData<String, IEnumerable<QuantityModel>> ElectricCapacities()
        => new() {
                { Mul("A","h"), [new() { System = si, Exponent = 1, Unit = "A" }, h ]},
                { Mul("kA","h"), [new() { System = si, Exponent = 1, Unit = "A", Prefix = "k" }, h ]}
            };
}

public sealed class GibberishParsingTest : ModelParserTest
{
    private static readonly ModelParser[] parsers = [.. Systems().Select(s => new ModelParser(s))];

    [Theory]
    [MemberData(nameof(ScalarGibberishStrings))]
    public void ScalarGibberishFailsToParse(String unit)
    {
        var actual = Parse(unit);

        Assert.Empty(actual);
    }

    [Theory]
    [MemberData(nameof(CompoundGibberishStrings))]
    public void CompoundGibberishValuesFailParsing(String unit)
    {
        var actual = Parse(unit).ToArray();

        Assert.Equal([], actual);
    }

    private static IEnumerable<ISystems> Systems()
    {
        yield return repository.Subset<Length>();
        yield return repository.Subset<Volume>();
        yield return repository.Subset<Velocity>();
        yield return repository.Subset<Mass>();
        yield return repository.Subset<Time>();
        yield return repository.Subset<Temperature>();
        yield return repository.Subset<Acceleration>();
        yield return repository.Subset<TemperatureChange>();
    }

    private static QuantityModel[] Parse(String unit)
    {
        foreach (var parser in parsers) {
            var models = parser.Parse(unit).ToArray();
            if (models.Length > 0) {
                return models;
            }
        }
        return [];
    }
    public static TheoryData<String> ScalarGibberishStrings() => new() {
                { "m3"},
                { "m⁻⁻¹"},
                { "ftft"},
                { "sqft"},
                { "KKm"},
                { "z"},
                { "-3m"},
                { "°°C"},
            };
    public static TheoryData<String> CompoundGibberishStrings() => new() {
                { "m*m"},
                { "/s"},
                { "ft//s"},
                { Mul("","s")},
                { "m/"},
                { Mul("s","")},
                { Mul(Mul("kA","h"),"m")},
                { "km/s/kg"},
            };
}
