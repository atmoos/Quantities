using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Parsing;
using Atmoos.Quantities.Serialization;

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

public abstract class ScalarModelParserTest<TDimension> : ModelParserTest
    where TDimension : IDimension
{
    private readonly ModelParser parser = CreateParser<TDimension>();
    public abstract void ScalarValuesCanBeParsed(String unit, QuantityModel expected);
    protected void TestScalarParsing(String unit, QuantityModel expected)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Equal([expected], actual);
    }
}

public abstract class CompoundModelParserTest<TDimension> : ModelParserTest
    where TDimension : IDimension
{
    private readonly ModelParser parser = CreateParser<TDimension>();
    public abstract void CompoundValuesCanBeParsed(String unit, IEnumerable<QuantityModel> expected);
    protected void TestCompoundParsing(String unit, IEnumerable<QuantityModel> expected)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Equal(expected, actual);
    }
}

public sealed class LengthParsingTest : ScalarModelParserTest<Length>
{
    [Theory]
    [MemberData(nameof(ScalarLengths))]
    public override void ScalarValuesCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);

    public static TheoryData<String, QuantityModel> ScalarLengths()
        => new() {
                { "m", m },
                { "ft", ft },
                { "km", km },
                { "mm", m with {Prefix = "m"}},
                { "mi", mi },
            };
}

public sealed class VolumeParsingTest : ScalarModelParserTest<Volume>
{
    [Theory]
    [MemberData(nameof(Volumes))]
    public override void ScalarValuesCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    public static TheoryData<String, QuantityModel> Volumes()
        => new() {
                { "m³", m with {Exponent = 3} },
                { "ft³", ft with {Exponent = 3} },
                { "km³", km with {Exponent = 3} },
                { "ℓ", new() { System = metric, Exponent = 1, Unit = "ℓ" } },
            };
}

public sealed class VelocityParsingTest : CompoundModelParserTest<Velocity>
{
    [Theory]
    [MemberData(nameof(Velocities))]
    public override void CompoundValuesCanBeParsed(String unit, IEnumerable<QuantityModel> expected) => TestCompoundParsing(unit, expected);
    public static TheoryData<String, IEnumerable<QuantityModel>> Velocities()
        => new() {
                { "m/s", [m, s with {Exponent = -1} ]},
                { Mul("m","s⁻¹"), [m, s with {Exponent = -1}]},
                { "ft/μs", [ft, new() { System = si, Exponent = -1, Unit = "s", Prefix = "μ" } ]},
                { "km/ms", [km, s with {Exponent = -1, Prefix = "m"} ]},
                { "km/h", [km, h with {Exponent = -1}]},
                { "mi/h", [mi, h with {Exponent = -1}]}
            };
}

public sealed class MassParsingTest : ScalarModelParserTest<Mass>
{
    [Theory]
    [MemberData(nameof(Masses))]
    public override void ScalarValuesCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    public static TheoryData<String, QuantityModel> Masses()
        => new() {
                { "kg", new() { System = si, Exponent =1, Unit = "kg" } },
                { "lb", new() { System = imperial, Exponent =1, Unit = "lb"} }
            };
}

public sealed class TimeParsingTest : ScalarModelParserTest<Time>
{
    [Theory]
    [MemberData(nameof(Times))]
    public override void ScalarValuesCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    public static TheoryData<String, QuantityModel> Times()
        => new() {
                { "s", s },
                { "μs", μs },
                { "h", h }
            };
}

public sealed class TemperatureParsingTest : ScalarModelParserTest<Temperature>
{
    [Theory]
    [MemberData(nameof(Temperatures))]
    public override void ScalarValuesCanBeParsed(String unit, QuantityModel expected) => TestScalarParsing(unit, expected);
    public static TheoryData<String, QuantityModel> Temperatures()
        => new() {
                { "°C", new() { System = metric, Exponent = 1, Unit = "°C" } },
                { "K", new() { System = si, Exponent = 1, Unit = "K" } },
                { "mK", new() { System = si, Exponent = 1, Unit = "K" , Prefix = "m"} }
            };
}

public sealed class AccelerationParsingTest : CompoundModelParserTest<Acceleration>
{
    [Theory]
    [MemberData(nameof(Accelerations))]
    public override void CompoundValuesCanBeParsed(String unit, IEnumerable<QuantityModel> expected) => TestCompoundParsing(unit, expected);
    public static TheoryData<String, IEnumerable<QuantityModel>> Accelerations()
        => new() {
                { "m/s²", [m, s with {Exponent = -2} ]},
                { Mul("m","s⁻²"), [m, s with {Exponent = -2}]},
                { "ft/μs²", [ft, s with {Exponent = -2, Prefix = "μ" } ]},
                { "mi/h²", [mi, h with {Exponent = -2} ]},
            };
}

public sealed class ElectricCapacityParsingTest : CompoundModelParserTest<ModelParserTest.ElectricCapacity>
{
    [Theory]
    [MemberData(nameof(ElectricCapacities))]
    public override void CompoundValuesCanBeParsed(String unit, IEnumerable<QuantityModel> expected) => TestCompoundParsing(unit, expected);
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
