using Atmoos.Quantities.Units.Si.Derived.Capacitance;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class CapacitanceTest
{
    [Fact]
    public void FaradToString() => FormattingMatches(v => Capacitance.Of(v, Si<Farad>()), "F");

    [Fact]
    public void MicroFaradToString() => FormattingMatches(v => Capacitance.Of(v, Si<Micro, Farad>()), "μF");

    [Fact]
    public void MicroFaradToFarad()
    {
        Capacitance capacitance = Capacitance.Of(1000000, Si<Micro, Farad>());
        Capacitance expected = Capacitance.Of(1, Si<Farad>());

        Capacitance actual = capacitance.To(Si<Farad>());

        actual.Matches(expected);
    }

    [Fact]
    public void MilliFaradToMicroFarad()
    {
        Capacitance capacitance = Capacitance.Of(1, Si<Milli, Farad>());
        Capacitance expected = Capacitance.Of(1000, Si<Micro, Farad>());

        Capacitance actual = capacitance.To(Si<Micro, Farad>());

        actual.Matches(expected);
    }
}
