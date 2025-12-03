using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Test.Dimensions;

public class ToolsTest
{
    [Fact]
    public void Interfaces_ForLinearDimension_ContainsRootInterfaceOnly()
    {
        var rootInterface = typeof(ILength);
        var result = Tools.Interfaces<Length>().ToList();

        Assert.Equal([rootInterface], result);
    }

    [Fact]
    public void Interfaces_ForQuotientDimension_ContainsDistinctRootAndComponentInterfaces()
    {
        var rootInterface = typeof(IVelocity);
        var result = Tools.Interfaces<Velocity>().ToList();

        Assert.Equal([rootInterface, typeof(ILength), typeof(ITime)], result);
    }

    [Fact]
    public void Interfaces_ForProductDimension_ContainsDistinctRootAndComponentInterfaces()
    {
        var rootInterface = typeof(IElectricCharge);
        var result = Tools.Interfaces<ElectricCharge>().ToList();

        Assert.Equal([rootInterface, typeof(ITime), typeof(IElectricCurrent)], result);
    }

    [Fact]
    public void Interfaces_ForSquaredDimension_ContainsDistinctRootAndLinearBase()
    {
        var linearBase = typeof(ILength);
        var rootInterface = typeof(IArea);
        var result = Tools.Interfaces<Area>().ToList();

        Assert.Equal([rootInterface, linearBase], result);
    }

    [Fact]
    public void Interfaces_ForCubicDimension_ContainsDistinctRootAndLinearBase()
    {
        var linearBase = typeof(ILength);
        var rootInterface = typeof(IVolume);
        var result = Tools.Interfaces<Volume>().ToList();

        Assert.Equal([rootInterface, linearBase], result);
    }

    [Theory]
    [MemberData(nameof(ToExponentData))]
    public void ToExponent_ReturnsExpectedSuperscript(Int32 exp, String expected)
    {
        Assert.Equal(expected, Tools.ToExponent(exp));
    }

    public static TheoryData<Int32, String> ToExponentData()
        => new() {
            { 1, "" },
            { 0, "⁰" },
            { -1, "⁻¹" },
            { 2, "²" },
            { 3, "³" },
            { 4, "⁴" },
            { 5, "⁵" },
            { 10, "¹⁰" },
            { 12, "¹²" },
            { -10, "⁻¹⁰" },
            { 123, "¹²³" },
            { -927829263, "⁻⁹²⁷⁸²⁹²⁶³" },
        };

}

file struct ElectricCharge : IElectricCharge { }
