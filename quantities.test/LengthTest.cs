namespace Quantities.Test;

public sealed class LengthTest
{
    private const Double KILOMETRE_IN_MILES = 1d / 1.609344d;

    [Fact]
    public void MetreToKilometre()
    {
        Length metres = Length.Si<Metre>(1000);
        Length kilometres = metres.To<Kilo, Metre>();
        Assert.Equal(1d, kilometres, SiPrecision);
    }

    [Fact]
    public void MetreToMillimetre()
    {
        Length metres = Length.Si<Metre>(1);
        Length millimetres = metres.To<Milli, Metre>();
        Assert.Equal(1000d, millimetres, SiPrecision);
    }

    [Fact]
    public void MillimetreToKilometre()
    {
        Length millimetres = Length.Si<Milli, Metre>(2e6);
        Length kilometres = millimetres.To<Kilo, Metre>();
        Assert.Equal(2d, kilometres, SiPrecision);
    }

    [Fact]
    public void MileToKilometre()
    {
        Length miles = Length.Imperial<Mile>(1);
        Length kilometres = miles.To<Kilo, Metre>();
        Assert.Equal(1.609344, kilometres, SiPrecision);
    }

    [Fact]
    public void KilometreToMile()
    {
        Length kilometres = Length.Si<Kilo, Metre>(1.609344);
        Length miles = kilometres.ToImperial<Mile>();
        Assert.Equal(1d, miles, SiPrecision);
    }
    [Fact]
    public void FootToMile()
    {
        Length feet = Length.Imperial<Foot>(5280);
        Length expected = Length.Imperial<Mile>(1);
        Length actual = feet.ToImperial<Mile>();
        actual.Matches(expected);
    }

    [Fact]
    public void AddMetresToKiloMetres()
    {
        Length kilometres = Length.Si<Kilo, Metre>(10);
        Length metres = Length.Si<Metre>(20);
        Length result = kilometres + metres;
        Assert.Equal(10.02, result, SiPrecision);
    }
    [Fact]
    public void AddKilometresToMiles()
    {
        Length kilometres = Length.Si<Kilo, Metre>(1);
        Length miles = Length.Imperial<Mile>(1);
        Length result = miles + kilometres;
        Assert.Equal(1 + KILOMETRE_IN_MILES, result, SiPrecision);
    }
    [Fact]
    public void AddMilesToKilometres()
    {
        Length kilometres = Length.Si<Kilo, Metre>(1);
        Length miles = Length.Imperial<Mile>(1);
        Length result = kilometres + miles;
        Assert.Equal(2.609344, result, SiPrecision);
    }
    [Fact]
    public void SubtractKilometresFromMetres()
    {
        Length metres = Length.Si<Metre>(2000);
        Length kilometres = Length.Si<Kilo, Metre>(0.5);
        Length result = metres - kilometres;
        Assert.Equal(1500d, result, SiPrecision);
    }

    [Fact]
    public void SubtractMilesFromKilometres()
    {
        Length kilometres = Length.Si<Kilo, Metre>(2.609344);
        Length miles = Length.Imperial<Mile>(1);
        Length result = kilometres - miles;
        Assert.Equal(1d, result, SiPrecision);
    }
    [Fact]
    public void OneMileInYards()
    {
        Length length = Length.Imperial<Mile>(1);
        Length expected = Length.Imperial<Yard>(1760);

        Length actual = length.ToImperial<Yard>();

        actual.Matches(expected);
    }
    [Fact]
    public void SiLengthBySiLengthIsSiArea()
    {
        Length length = Length.Si<Kilo, Metre>(2);
        Length width = Length.Si<Hecto, Metre>(1);
        Area expected = Area.Square<Kilo, Metre>(0.2);

        Area actual = length * width;

        actual.Matches(expected);
    }
    [Fact]
    public void OtherLengthByOtherLengthIsOtherArea()
    {
        Length length = Length.Imperial<Mile>(2);
        Length width = Length.Imperial<Yard>(1760 / 2);
        Area expected = Area.SquareImperial<Mile>(1);

        Area actual = length * width;

        actual.Matches(expected);
    }

    /*
    [Fact]
    public void SiLengthBySiTimeIsVelocity()
    {
        Length distance = Length.Si<Milli, Metre>(100);
        Time duration = Time.Seconds(20);
        Velocity expected = Velocity.Si<Milli, Metre>(5).PerSecond();

        Velocity actual = distance / duration;

        actual.Matches(expected);
    }
    [Fact]
    public void SiLengthByOtherTimeIsVelocity()
    {
        Length distance = Length.Si<Kilo, Metre>(120);
        Time duration = Time.SiAccepted<Hour>(10);
        Velocity expected = Velocity.Si<Kilo, Metre>(12).Per<Hour>();

        Velocity actual = distance / duration;

        actual.Matches(expected);
    }
    [Fact]
    public void OtherLengthByTimeIsVelocity()
    {
        Length distance = Length.Imperial<Mile>(70);
        Time duration = Time.SiAccepted<Hour>(2);
        Velocity expected = Velocity.Imperial<Mile>(35).Per<Hour>();

        Velocity actual = distance / duration;

        actual.Matches(expected);
    }
    */
}
