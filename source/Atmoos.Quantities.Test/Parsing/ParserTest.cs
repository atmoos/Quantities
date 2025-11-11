using Atmoos.Quantities.Parsing;
using Atmoos.Quantities.Serialization;

namespace Atmoos.Quantities.Test.Parsing;

public class ParserTest
{
    private const String si = "si";
    private const String metric = "metric";
    private const String imperial = "imperial";

    [Theory]
    [MemberData(nameof(ScalarGibberishStrings))]
    public void ScalarGibberishFailsToParse(String unit)
    {
        var actual = Parser.Parse(unit);

        Assert.Empty(actual);
    }

    [Theory]
    [MemberData(nameof(ScalarStrings))]
    public void ScalarValuesCanBeParsed(String unit, QuantityModel expected)
    {
        var actual = Parser.Parse(unit).ToArray();

        Assert.Equal([expected], actual);
    }

    public static TheoryData<String, QuantityModel> ScalarStrings() => new() {
                { "m", new() { System = si, Exponent = 1, Unit = "m" } },
                { "m⁻¹", new() { System = si, Exponent = -1, Unit = "m" } },
                { "ft", new() { System = imperial, Exponent = 1, Unit = "ft" } },
                { "ft³", new() { System = imperial, Exponent = 3, Unit = "ft" } },
                { "km", new() { System = si, Exponent = 1, Unit = "m", Prefix = "k" } },
                { "mm", new() { System = si, Exponent = 1, Unit = "m", Prefix = "m" } },
                { "mi", new() { System = imperial, Exponent = 1, Unit = "mi" } },
                { "°C", new() { System = metric, Exponent = 1, Unit = "°C" } },
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
}
