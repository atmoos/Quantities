using Atmoos.Quantities.Units.Si.Derived.ElectricalConductance;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class ElectricalConductanceTest
{
    [Fact]
    public void SiemensToString() => FormattingMatches(v => ElectricalConductance.Of(v, Si<Siemens>()), "S");

    [Fact]
    public void MilliSiemensToString() => FormattingMatches(v => ElectricalConductance.Of(v, Si<Milli, Siemens>()), "mS");

    [Fact]
    public void MilliSiemensToSiemens()
    {
        ElectricalConductance conductance = ElectricalConductance.Of(1000, Si<Milli, Siemens>());
        ElectricalConductance expected = ElectricalConductance.Of(1, Si<Siemens>());

        ElectricalConductance actual = conductance.To(Si<Siemens>());

        actual.Matches(expected);
    }

    [Fact]
    public void SiemensToMilliSiemens()
    {
        ElectricalConductance conductance = ElectricalConductance.Of(1, Si<Siemens>());
        ElectricalConductance expected = ElectricalConductance.Of(1000, Si<Milli, Siemens>());

        ElectricalConductance actual = conductance.To(Si<Milli, Siemens>());

        actual.Matches(expected);
    }
}
