using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Derived.Capacitance;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class CapacitanceTest
{
    [Fact]
    public void FaradToString() => FormattingMatches(v => Capacitance.Of(v, Si<Farad>()), "F");

    [Fact]
    public void CoulombPerVoltToString() => FormattingMatches(v => Capacitance.Of(v, Si<Coulomb>().Per(Si<Volt>())), "C/V");

    [Fact]
    public void MicroFaradToString() => FormattingMatches(v => Capacitance.Of(v, Si<Micro, Farad>()), "μF");

    [Fact]
    public void CoulombPerVoltToFarad()
    {
        Capacitance capacitance = Capacitance.Of(1, Si<Coulomb>().Per(Si<Volt>()));
        Capacitance expected = Capacitance.Of(1, Si<Farad>());

        Capacitance actual = capacitance.To(Si<Farad>());

        actual.Matches(expected);
    }

    [Fact]
    public void FaradToCoulombPerVolt()
    {
        Capacitance capacitance = Capacitance.Of(2, Si<Farad>());
        Capacitance expected = Capacitance.Of(2, Si<Coulomb>().Per(Si<Volt>()));

        Capacitance actual = capacitance.To(Si<Coulomb>().Per(Si<Volt>()));

        actual.Matches(expected);
    }

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

    [Fact]
    public void CoulombPerVoltRoundTripViaFarad()
    {
        Capacitance expected = Capacitance.Of(3.5, Si<Coulomb>().Per(Si<Volt>()));

        Capacitance actual = expected.To(Si<Farad>()).To(Si<Coulomb>().Per(Si<Volt>()));

        actual.Matches(expected);
    }

    [Fact]
    public void FaradRoundTripViaCoulombPerVolt()
    {
        Capacitance expected = Capacitance.Of(2.5, Si<Farad>());

        Capacitance actual = expected.To(Si<Coulomb>().Per(Si<Volt>())).To(Si<Farad>());

        actual.Matches(expected);
    }
}
