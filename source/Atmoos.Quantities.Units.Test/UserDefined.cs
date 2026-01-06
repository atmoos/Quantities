using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Test;

public class UserDefined
{
    [Fact]
    public void PrefixesWork()
    {
        Length equivalentToFourMetres = Length.Of(1, Si<MyPrefixFour, Metre>());

        Length fourActualSiMetres = equivalentToFourMetres.To(Si<Metre>());
        var (value, unit) = fourActualSiMetres;

        Assert.Equal((4d, "m"), (value, unit));
    }

    [Fact]
    public void PrefixesRepresentCorrectly()
    {
        Length oneFourMetre = Length.Of(1, Si<MyPrefixFour, Metre>());

        Assert.Equal("1 2e2m", oneFourMetre.ToString());
    }

    [Fact]
    public void UnitsWork()
    {
        Length equivalentToOneMetre = Length.Of(2, NonStandard<MyWhackWhackUnit>());

        Length oneActualSiMetres = equivalentToOneMetre.To(Si<Metre>());
        var (value, unit) = oneActualSiMetres;

        Assert.Equal((1d, "m"), (value, unit));
    }

    [Fact]
    public void UnitsRepresentCorrectly()
    {
        Length oneWhackWhack = Length.Of(1, NonStandard<MyWhackWhackUnit>());

        Assert.Equal("1 Whack!", oneWhackWhack.ToString());
    }

    private sealed class MyPrefixFour : IPrefix, IMetricPrefix
    {
        public static String Representation => "2e2";

        public static Transformation ToSi(Transformation self) => 4d * self;
    }

    private sealed class MyWhackWhackUnit : IUnit, INonStandardUnit, ILength
    {
        public static String Representation => "Whack!";

        public static Transformation ToSi(Transformation self) => self / 2d;
    }
}
