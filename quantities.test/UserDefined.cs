using Quantities.Dimensions;
using Quantities.Units;
using Quantities.Units.NonStandard;

namespace Quantities.Test;

public class UserDefined
{
    [Fact]
    public void PrefixesWork()
    {
        Length equivalentToFourMetres = Length.Of(1).Si<MyPrefixFour, Metre>();

        Length fourActualSiMetres = equivalentToFourMetres.To.Si<Metre>();

        Assert.Equal(4d, fourActualSiMetres);
    }

    [Fact]
    public void PrefixesRepresentCorrectly()
    {
        Length oneFourMetre = Length.Of(1).Si<MyPrefixFour, Metre>();

        Assert.Equal("1 2e2m", oneFourMetre.ToString());
    }

    [Fact]
    public void UnitsWork()
    {
        Length equivalentToOneMetre = Length.Of(2).NonStandard<MyWhackWhackUnit>();

        Length oneActualSiMetres = equivalentToOneMetre.To.Si<Metre>();

        Assert.Equal(1d, oneActualSiMetres);
    }

    [Fact]
    public void UnitsRepresentCorrectly()
    {
        Length oneWhackWhack = Length.Of(1).NonStandard<MyWhackWhackUnit>();

        Assert.Equal("1 Whack!", oneWhackWhack.ToString());
    }

    private sealed class MyPrefixFour : IPrefix, IMetricPrefix
    {
        public static String Representation => "2e2";
        public static Double FromSi(in Double value) => value / 4d;
        public static Double ToSi(in Double self) => 4d * self;
    }

    private sealed class MyWhackWhackUnit : IUnit, INoSystemUnit, ILength
    {
        public static String Representation => "Whack!";
        public static Double FromSi(in Double value) => 2d * value;
        public static Double ToSi(in Double self) => self / 2d;
    }
}
