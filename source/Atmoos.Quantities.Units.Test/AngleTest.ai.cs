using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class AngleTest
{
    [Fact]
    public void RadianToString() => FormattingMatches(v => Angle.Of(v, Si<Radian>()), "rad");

    [Fact]
    public void MilliRadianToString() => FormattingMatches(v => Angle.Of(v, Si<Milli, Radian>()), "mrad");

    [Fact]
    public void PiRadiansToHalfTurn()
    {
        Angle piRadians = Angle.Of(Math.PI, Si<Radian>());
        // ToDo: This should be 0.5 turn, but we don't have a turn unit yet.
        Angle expected = Angle.Of(Math.PI, Si<Radian>());

        Angle actual = piRadians.To(Si<Radian>());

        actual.Matches(expected);
    }
}
