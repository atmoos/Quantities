using Atmoos.Quantities.Parsing;
using Atmoos.Quantities.Units.Si.Metric;

using static Atmoos.Quantities.Test.Convenience;

namespace Atmoos.Quantities.Test.Parsing;

public class ParserTest
{
    private static readonly UnitRepository repository = UnitRepository.Create();
    private readonly IParser<Length> lengthParser = Parser.Create<Length>(repository);
    private readonly IParser<Volume> volumeParser = Parser.Create<Volume>(repository);
    private readonly IParser<Velocity> velocityParser = Parser.Create<Velocity>(repository);
    private readonly IParser<Acceleration> accelerationParser = Parser.Create<Acceleration>(repository);

    [Fact]
    public void ParseFullQuantity_ErrorContainsMisrepresentationAndQuantityName()
    {
        const String noLength = "3 s";
        var error = Assert.Throws<FormatException>(() => this.lengthParser.Parse(noLength));

        Assert.Contains($"input '{noLength}'", error.Message);
        Assert.Contains($"'{nameof(Length)}'", error.Message);
    }

    [Fact]
    public void ParseUnitOnly_ErrorContainsMisrepresentationAndQuantityName()
    {
        const String noLengthUnit = "m/s";
        var error = Assert.Throws<FormatException>(() => this.lengthParser.Parse(3, noLengthUnit));

        Assert.Contains($"unit '{noLengthUnit}'", error.Message);
        Assert.Contains($"'{nameof(Length)}'", error.Message);
    }

    [Theory]
    [MemberData(nameof(ScalarGibberishStrings))]
    public void ScalarGibberishFailsToParse(String unit)
    {
        var actual = this.lengthParser.TryParse(unit, out _);

        Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(ScalarQuantities))]
    public void ScalarValuesCanBeParsed(Length length)
    {
        var text = length.ToString("R");

        var actual = this.lengthParser.Parse(text);

        Assert.Equal(length, actual);
    }

    [Theory]
    [MemberData(nameof(QuotientsStrings))]
    public void QuotientsCanBeParsed(Velocity velocity)
    {
        var text = velocity.ToString("R");

        var actual = this.velocityParser.Parse(text);

        Assert.Equal(velocity, actual);
    }

    [Theory]
    [MemberData(nameof(ExponentialStrings))]
    public void ExponentialsCanBeParsed(Volume volume)
    {
        var text = volume.ToString("R");

        var actual = this.volumeParser.Parse(text);

        Assert.Equal(volume, actual);
    }

    [Theory]
    [MemberData(nameof(CompoundGibberishStrings))]
    public void CompoundGibberishValuesFailParsing(String unit)
    {
        var actual = this.velocityParser.TryParse(unit, out _);

        Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(AlmostAcceleration))]
    public void UnitExponentsMustMatchExactly(String unitOnly)
    {
        var unit = $"{3.14} {unitOnly}";
        var actual = this.accelerationParser.TryParse(unit, out _);

        Assert.False(actual, $"The unit '{unitOnly}' is not a valid acceleration unit.");
    }

    private static String Mul(String left, String right) => Join(left, right, '\u200C');
    private static String Join(String left, String right, Char joiner) => $"{left}{joiner}{right}";
    public static TheoryData<Length> ScalarQuantities() => new() {
        { Length.Of(Math.PI, in Si<Metre>()) },
        { Length.Of(Math.Tau, in Imperial<Foot>()) },
        { Length.Of(Math.E, in Si<Kilo,Metre>()) },
        { Length.Of(-1 * Math.PI/Math.E, in Si<Milli,Metre>()) },
        { Length.Of(Math.E /Math.Tau, in Imperial<Mile>()) }
    };
    public static TheoryData<Velocity> QuotientsStrings() => new() {
        { Velocity.Of(Math.PI, Si<Metre>().Per( Si<Second>())) },
        { Velocity.Of(Math.Tau, Si<Centi,Metre>().Per(Si<Milli,Second>())) },
        { Velocity.Of(Math.E, Imperial<Mile>().Per( Si<Second>())) }
    };
    public static TheoryData<Volume> ExponentialStrings() => new() {
        { Volume.Of(Math.PI, Cubic(Si<Metre>())) },
        { Volume.Of(Math.Tau,  Cubic(Si<Kilo,Metre>())) },
        { Volume.Of(Math.Tau,  Cubic(Imperial<Foot>())) },
        { Volume.Of(Math.E, Metric<Litre>()) }
    };
    public static TheoryData<String> ScalarGibberishStrings()
        => ToTheoryData("4 m3", "4.32 m⁻⁻¹", "43 ftft", "123.432 ft³", "Km 3112", "", "3432.423m");
    public static TheoryData<String> CompoundGibberishStrings()
        => ToTheoryData("2 m*m", "23 3/s", "43 ft//s", $"3 {Mul("m", "s")}", "2 m/", $"3 {Mul("s", "")}");
    public static TheoryData<String> AlmostAcceleration()
        => ToTheoryData("s", "m/s³", "m/s", "m", "m²/s", "m/s⁴", "s²/m");
}
