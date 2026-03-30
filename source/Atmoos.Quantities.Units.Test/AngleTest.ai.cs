using Atmoos.Quantities.Units.NonStandard.Angle;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class AngleTest
{
    [Fact]
    public void RadianToString() => FormattingMatches(v => Angle.Of(v, Si<Radian>()), "rad");

    [Fact]
    public void MilliRadianToString() => FormattingMatches(v => Angle.Of(v, Si<Milli, Radian>()), "mrad");

    [Fact]
    public void DegreeToString() => FormattingMatches(v => Angle.Of(v, Metric<Degree>()), "°");

    [Fact]
    public void GradianToString() => FormattingMatches(v => Angle.Of(v, NonStandard<Gradian>()), "gon");

    [Fact]
    public void TurnToString() => FormattingMatches(v => Angle.Of(v, NonStandard<Turn>()), "rev");

    [Fact]
    public void PiRadiansToHalfTurn()
    {
        Angle piRadians = Angle.Of(Math.PI, Si<Radian>());
        Angle expected = Angle.Of(0.5, NonStandard<Turn>());

        Angle actual = piRadians.To(NonStandard<Turn>());

        actual.Matches(expected);
    }

    [Fact]
    public void OneHundredAndEightyDegreesToPiRadians()
    {
        Angle degrees = Angle.Of(180, Metric<Degree>());
        Angle expected = Angle.Of(Math.PI, Si<Radian>());

        Angle actual = degrees.To(Si<Radian>());

        actual.Matches(expected);
    }

    [Fact]
    public void ThreeHundredAndSixtyDegreesToTwoPiRadians()
    {
        Angle degrees = Angle.Of(360, Metric<Degree>());
        Angle expected = Angle.Of(2 * Math.PI, Si<Radian>());

        Angle actual = degrees.To(Si<Radian>());

        actual.Matches(expected);
    }

    [Fact]
    public void RadianToDegreeRoundTrip()
    {
        Angle original = Angle.Of(1.5, Si<Radian>());

        Angle roundTrip = original.To(Metric<Degree>()).To(Si<Radian>());

        roundTrip.Matches(original);
    }

    [Fact]
    public void TwoHundredGradiansToPiRadians()
    {
        Angle gradians = Angle.Of(200, NonStandard<Gradian>());
        Angle expected = Angle.Of(Math.PI, Si<Radian>());

        Angle actual = gradians.To(Si<Radian>());

        actual.Matches(expected);
    }

    [Fact]
    public void NinetyDegreesToOneHundredGradians()
    {
        Angle degrees = Angle.Of(90, Metric<Degree>());
        Angle expected = Angle.Of(100, NonStandard<Gradian>());

        Angle actual = degrees.To(NonStandard<Gradian>());

        actual.Matches(expected);
    }

    [Fact]
    public void OneTurnToThreeHundredAndSixtyDegrees()
    {
        Angle turn = Angle.Of(1, NonStandard<Turn>());
        Angle expected = Angle.Of(360, Metric<Degree>());

        Angle actual = turn.To(Metric<Degree>());

        actual.Matches(expected);
    }

    [Fact]
    public void OneTurnToTwoPiRadians()
    {
        Angle turn = Angle.Of(1, NonStandard<Turn>());
        Angle expected = Angle.Of(2 * Math.PI, Si<Radian>());

        Angle actual = turn.To(Si<Radian>());

        actual.Matches(expected);
    }

    [Fact]
    public void HalfTurnToTwoHundredGradians()
    {
        Angle halfTurn = Angle.Of(0.5, NonStandard<Turn>());
        Angle expected = Angle.Of(200, NonStandard<Gradian>());

        Angle actual = halfTurn.To(NonStandard<Gradian>());

        // MediumPrecision: two chained irrational (π) conversions prevent exact cancellation in IEEE 754.
        actual.Matches(expected, MediumPrecision);
    }
}
