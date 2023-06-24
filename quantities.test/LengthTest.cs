using Quantities.Units.NonStandard.Length;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public sealed class LengthTest
{
    private const Double miles_in_kilometre = 1.609344d;
    private const Double kilometre_in_miles = 1d / miles_in_kilometre;

    [Fact]
    public void MetreToKilometre()
    {
        Length metres = Length.Of(1000).Si<Metre>();
        Length kilometres = metres.To.Si<Kilo, Metre>();
        PrecisionIsBounded(1d, kilometres);
    }

    [Fact]
    public void MetreToMillimetre()
    {
        Length metres = Length.Of(1).Si<Metre>();
        Length millimetres = metres.To.Si<Milli, Metre>();
        PrecisionIsBounded(1000d, millimetres);
    }

    [Fact]
    public void ÅngströmToNanoMetre()
    {
        Length ångström = Length.Of(10).Metric<Ångström>();
        Length nanoMetres = ångström.To.Si<Nano, Metre>();
        PrecisionIsBounded(1d, nanoMetres);
    }

    [Fact]
    public void MillimetreToKilometre()
    {
        Length millimetres = Length.Of(2e6).Si<Milli, Metre>();
        Length kilometres = millimetres.To.Si<Kilo, Metre>();
        PrecisionIsBounded(2d, kilometres);
    }

    [Fact]
    public void MileToKilometre()
    {
        Length miles = Length.Of(1).Imperial<Mile>();
        Length kilometres = miles.To.Si<Kilo, Metre>();
        PrecisionIsBounded(1.609344, kilometres);
    }

    [Fact]
    public void KilometreToMile()
    {
        Length kilometres = Length.Of(1.609344).Si<Kilo, Metre>();
        Length miles = kilometres.To.Imperial<Mile>();
        PrecisionIsBounded(1d, miles);
    }
    [Fact]
    public void FootToMile()
    {
        Length feet = Length.Of(5280).Imperial<Foot>();
        Length expected = Length.Of(1).Imperial<Mile>();
        Length actual = feet.To.Imperial<Mile>();
        actual.Matches(expected);
    }

    [Fact]
    public void AddMetresToKiloMetres()
    {
        Length kilometres = Length.Of(10).Si<Kilo, Metre>();
        Length metres = Length.Of(20).Si<Metre>();
        Length result = kilometres + metres;
        PrecisionIsBounded(10.02, result);
    }
    [Fact]
    public void AddKilometresToMiles()
    {
        Length kilometres = Length.Of(1).Si<Kilo, Metre>();
        Length miles = Length.Of(1).Imperial<Mile>();
        Length result = miles + kilometres;
        PrecisionIsBounded(1 + kilometre_in_miles, result);
    }
    [Fact]
    public void AddMilesToKilometres()
    {
        Length kilometres = Length.Of(1).Si<Kilo, Metre>();
        Length miles = Length.Of(1).Imperial<Mile>();
        Length result = kilometres + miles;
        PrecisionIsBounded(2.609344, result);
    }
    [Fact]
    public void SubtractKilometresFromMetres()
    {
        Length metres = Length.Of(2000).Si<Metre>();
        Length kilometres = Length.Of(0.5).Si<Kilo, Metre>();
        Length result = metres - kilometres;
        PrecisionIsBounded(1500d, result);
    }
    [Fact]
    public void SubtractMilesFromKilometres()
    {
        Length kilometres = Length.Of(2.609344).Si<Kilo, Metre>();
        Length miles = Length.Of(1).Imperial<Mile>();
        Length result = kilometres - miles;
        PrecisionIsBounded(1d, result);
    }
    [Fact]
    public void OneMileInYards()
    {
        Length length = Length.Of(1).Imperial<Mile>();
        Length expected = Length.Of(1760).Imperial<Yard>();

        Length actual = length.To.Imperial<Yard>();

        actual.Matches(expected);
    }
    [Fact]
    public void SiLengthBySiLengthIsSiArea()
    {
        Length length = Length.Of(2).Si<Kilo, Metre>();
        Length width = Length.Of(1).Si<Hecto, Metre>();
        Area expected = Area.Of(0.2).Square.Si<Kilo, Metre>();

        Area actual = length * width;

        actual.Matches(expected);
    }
    [Fact]
    public void ImperialLengthByImperialLengthIsImperialArea()
    {
        Length length = Length.Of(2).Imperial<Mile>();
        Length width = Length.Of(1760 / 2).Imperial<Yard>();
        Area expected = Area.Of(1).Square.Imperial<Mile>();

        Area actual = length * width;

        actual.Matches(expected);
    }
    [Fact]
    public void LengthByDivisionIsEqualToLengthByConstruction()
    {
        Volume volume = Volume.Of(300).Metric<Hecto, Litre>();
        Area area = Area.Of(6).Square.Si<Metre>();
        Length expected = Length.Of(5).Si<Metre>();

        Length actual = volume / area;

        actual.Matches(expected);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void SiLengthBySiTimeIsVelocity()
    {
        Length distance = Length.Of(100).Si<Milli, Metre>();
        Time duration = Time.Of(20).Si<Second>();
        Velocity expected = Velocity.Of(5).Si<Milli, Metre>().Per.Si<Second>();

        Velocity actual = distance / duration;

        actual.Matches(expected);
    }
    [Fact]
    public void SiLengthByMetricTimeIsVelocity()
    {
        Length distance = Length.Of(120).Si<Kilo, Metre>();
        Time duration = Time.Of(10).Metric<Hour>();
        Velocity expected = Velocity.Of(12).Si<Kilo, Metre>().Per.Metric<Hour>();

        Velocity actual = distance / duration;

        actual.Matches(expected);
    }
    [Fact]
    public void ImperialLengthByTimeIsVelocity()
    {
        Length distance = Length.Of(70).Imperial<Mile>();
        Time duration = Time.Of(2).Metric<Hour>();
        Velocity expected = Velocity.Of(35).Imperial<Mile>().Per.Metric<Hour>();

        Velocity actual = distance / duration;

        actual.Matches(expected);
    }
    [Fact]
    public void VelocityTimesTimeIsLength()
    {
        Time duration = Time.Of(12).Metric<Minute>();
        Velocity velocity = Velocity.Of(350).Imperial<Mile>().Per.Metric<Hour>();
        // Miles are not yet preserved across multiplication...
        Length expected = Length.Of(350 * 12 * miles_in_kilometre / 60).Si<Kilo, Metre>();

        Length actual = velocity * duration;

        actual.Matches(expected);
    }

    [Theory]
    [MemberData(nameof(Lengths))]
    public void LengthSupportsSerialization(Length length) => length.SupportsSerialization();

    public static IEnumerable<Object[]> Lengths()
    {
        static IEnumerable<Length> Interesting()
        {
            yield return Length.Of(21).Si<Metre>();
            yield return Length.Of(342).Si<Micro, Metre>();
            yield return Length.Of(1).Si<Mega, Metre>();
            yield return Length.Of(-11).Imperial<Mile>();
            yield return Length.Of(9).Imperial<Foot>();
            yield return Length.Of(54).Imperial<Yard>();
            yield return Length.Of(3.2).Metric<Kilo, Ångström>();
            yield return Length.Of(11).NonStandard<LightYear>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
