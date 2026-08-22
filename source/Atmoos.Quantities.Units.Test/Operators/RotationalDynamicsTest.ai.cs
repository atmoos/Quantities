using Atmoos.Quantities;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test.Operators;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class RotationalDynamicsTest
{
    [Fact]
    public void ForceTimesLengthYieldsTorque()
    {
        Force force = Force.Of(12, Si<Newton>());
        Length length = Length.Of(3, Si<Metre>());
        Torque expected = Torque.Of(36, Si<Newton>().Times(Si<Metre>()));

        Torque actual = force * length;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void LengthTimesForceYieldsTorque()
    {
        Length length = Length.Of(4, Si<Metre>());
        Force force = Force.Of(5, Si<Newton>());
        Torque expected = Torque.Of(20, Si<Newton>().Times(Si<Metre>()));

        Torque actual = length * force;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TorqueDividedByLengthYieldsForce()
    {
        Torque torque = Torque.Of(30, Si<Newton>().Times(Si<Metre>()));
        Length length = Length.Of(6, Si<Metre>());
        Force expected = Force.Of(5, Si<Newton>());

        Force actual = torque / length;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TorqueDividedByForceYieldsLength()
    {
        Torque torque = Torque.Of(42, Si<Newton>().Times(Si<Metre>()));
        Force force = Force.Of(7, Si<Newton>());
        Length expected = Length.Of(6, Si<Metre>());

        Length actual = torque / force;

        Assert.Equal(expected, actual);
    }
}
