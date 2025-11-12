using Atmoos.Quantities.Parsing;
using Atmoos.Quantities.Serialization;

namespace Atmoos.Quantities.Test.Parsing;

public class ParserTest
{
    private readonly Parser parser = new(Quantities.Parsing.Systems.Create());
    private const String si = "si";
    private const String metric = "metric";
    private const String imperial = "imperial";

    private static readonly QuantityModel s = new() { System = si, Exponent = 1, Unit = "s" };
    private static readonly QuantityModel h = new() { System = metric, Exponent = 1, Unit = "h" };
    private static readonly QuantityModel μs = new() { System = si, Exponent = 1, Unit = "s", Prefix = "μ" };
    private static readonly QuantityModel m = new() { System = si, Exponent = 1, Unit = "m" };
    private static readonly QuantityModel ft = new() { System = imperial, Exponent = 1, Unit = "ft" };
    private static readonly QuantityModel mi = new() { System = imperial, Exponent = 1, Unit = "mi" };
    private static readonly QuantityModel km = new() { System = si, Exponent = 1, Unit = "m", Prefix = "k" };

    [Theory]
    [MemberData(nameof(ScalarGibberishStrings))]
    public void ScalarGibberishFailsToParse(String unit)
    {
        var actual = this.parser.Parse(unit);

        Assert.Empty(actual);
    }

    [Theory]
    [MemberData(nameof(ScalarStrings))]
    public void ScalarValuesCanBeParsed(String unit, QuantityModel expected)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Equal([expected], actual);
    }

    [Theory]
    [MemberData(nameof(CompoundStrings))]
    public void CompoundValuesCanBeParsed(String unit, IEnumerable<QuantityModel> expected)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(CompoundGibberishStrings))]
    public void CompoundGibberishValuesFailParsing(String unit)
    {
        var actual = this.parser.Parse(unit).ToArray();

        Assert.Equal([], actual);
    }

    private static String Mul(String left, String right) => Join(left, right, '\u200C');
    private static String Join(String left, String right, Char joiner) => $"{left}{joiner}{right}";

    public static TheoryData<String, QuantityModel> ScalarStrings() => new() {
                { "m", m },
                { "m⁻¹", m with {Exponent = -1} },
                { "ft", ft },
                { "ft³", ft with {Exponent = 3} },
                { "km", km },
                { "s", s },
                { "μs", μs },
                { "mm", m with {Prefix = "m"}},
                { "mi", mi },
                { "kg", new() { System = si, Exponent = 1, Unit = "kg" } },
                { "°C", new() { System = metric, Exponent = 1, Unit = "°C" } },
            };
    public static TheoryData<String, IEnumerable<QuantityModel>> CompoundStrings() => new() {
                { "m/s", [m, s with {Exponent = -1} ]},
                { Mul("m","s⁻¹"), [m, s with {Exponent = -1}]},
                { "ft/μs", [ft, μs with {Exponent = -1}]},
                { "km/s³", [km, s with {Exponent = -3} ]},
                { Mul("A","h"), [new() { System = si, Exponent = 1, Unit = "A" }, h ]},
                { "°C/s", [new() { System = metric, Exponent = 1, Unit = "°C" } , s with {Exponent=-1}]},
            };
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
