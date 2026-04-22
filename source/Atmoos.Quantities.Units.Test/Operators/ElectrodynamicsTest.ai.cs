using Atmoos.Quantities;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Derived.ElectricalConductance;

namespace Atmoos.Quantities.Units.Test.Operators;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class ElectrodynamicsTest
{
    [Fact]
    public void CurrentDividedByPotentialYieldsConductance()
    {
        ElectricCurrent current = ElectricCurrent.Of(2, Si<Ampere>());
        ElectricPotential potential = ElectricPotential.Of(10, Si<Volt>());
        ElectricalConductance expected = ElectricalConductance.Of(0.2, Si<Siemens>());

        ElectricalConductance actual = current / potential;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PotentialTimesConductanceYieldsCurrent()
    {
        ElectricPotential potential = ElectricPotential.Of(10, Si<Volt>());
        ElectricalConductance conductance = ElectricalConductance.Of(0.2, Si<Siemens>());
        ElectricCurrent expected = ElectricCurrent.Of(2, Si<Ampere>());

        ElectricCurrent actual = potential * conductance;

        Assert.Equal(expected, actual);
    }
}
