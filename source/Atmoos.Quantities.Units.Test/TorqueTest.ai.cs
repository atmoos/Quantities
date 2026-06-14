using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class TorqueTest
{
    [Fact]
    public void NewtonMetreToString() => FormattingMatches(v => Torque.Of(v, Si<Newton>().Times(Si<Metre>())), Join("N", "m"));

    [Fact]
    public void KiloNewtonMetreToString() => FormattingMatches(v => Torque.Of(v, Si<Kilo, Newton>().Times(Si<Metre>())), Join("kN", "m"));

    [Fact]
    public void KiloNewtonMetreToNewtonMetre()
    {
        Torque torque = Torque.Of(1, Si<Kilo, Newton>().Times(Si<Metre>()));
        Torque expected = Torque.Of(1000, Si<Newton>().Times(Si<Metre>()));

        Torque actual = torque.To(Si<Newton>().Times(Si<Metre>()));

        actual.Matches(expected);
    }

    [Fact]
    public void NewtonMetreToNewtonCentiMetre()
    {
        Torque torque = Torque.Of(1, Si<Newton>().Times(Si<Metre>()));
        Torque expected = Torque.Of(100, Si<Newton>().Times(Si<Centi, Metre>()));

        Torque actual = torque.To(Si<Newton>().Times(Si<Centi, Metre>()));

        actual.Matches(expected);
    }
}
