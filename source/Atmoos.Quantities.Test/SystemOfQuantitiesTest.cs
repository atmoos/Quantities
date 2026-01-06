using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Test.Dimensions;
using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Test;

public class SystemOfQuantitiesTest
{
    [Fact]
    public void TimeIsBaseQuantity() => Quantity<Time>.IsBaseQuantity(nameof(Time));

    [Fact]
    public void LengthIsBaseQuantity() => Quantity<Length>.IsBaseQuantity(nameof(Length));

    [Fact]
    public void MassIsBaseQuantity() => Quantity<Mass>.IsBaseQuantity(nameof(Mass));

    [Fact]
    public void ElectricCurrentIsBaseQuantity() => Quantity<ElectricCurrent>.IsBaseQuantity(nameof(ElectricCurrent));

    [Fact]
    public void TemperatureIsBaseQuantity() => Quantity<Temperature>.IsBaseQuantity(nameof(Temperature));

    [Fact]
    public void AreaIsDerivedFromLength() => Quantity<Area>.IsDerivedFrom(Dim<Length>.Pow(2));

    [Fact]
    public void VolumeIsDerivedFromLength() => Quantity<Volume>.IsDerivedFrom(Dim<Length>.Pow(3));

    [Fact]
    public void VelocityIsDerivedFromLengthPerTime() => Quantity<Velocity>.IsDerivedFrom(Dim<Length>.Per<Time>());

    [Fact]
    public void ForceIsDerivedFromLengthTimeAndMass()
    {
        var expected = Dim<Mass>.Value * Dim<Length>.Value * Dim<Time>.Pow(-2);
        Quantity<Force>.IsDerivedFrom(expected);
    }

    [Fact]
    public void AccelerationIsDerivedFromLengthPerTimeSquared()
    {
        var expected = Dim<Length>.Value * Dim<Time>.Pow(-2);
        Quantity<Acceleration>.IsDerivedFrom(expected);
    }

    [Fact]
    public void PressureIsDerivedFromLengthTimeAndMass()
    {
        var expected = Dim<Mass>.Value * Dim<Length>.Pow(-1) * Dim<Time>.Pow(-2);
        Quantity<Pressure>.IsDerivedFrom(expected);
    }
}

file static class Quantity<TActual>
    where TActual : IDimension
{
    public static void IsBaseQuantity(String name)
    {
        Is<IBaseQuantity>();
        Assert.IsAssignableFrom<Scalar>(TActual.D);
        Assert.Equal(1, TActual.D.E);
        Assert.Equal(name, TActual.D.ToString());
    }

    public static void IsDerivedFrom(Dimension expected)
    {
        Is<IDerivedQuantity>();
        DimAssert.Equal(expected, TActual.D);
    }

    private static void Is<TExpected>()
    {
        var msg = $"{NameOf<TActual>()} does not implement {NameOf<TExpected>()}.";
        Assert.True(typeof(TActual).IsAssignableTo(typeof(TExpected)), msg);
    }
}
