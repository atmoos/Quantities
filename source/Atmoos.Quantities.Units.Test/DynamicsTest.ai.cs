using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class DynamicsTest
{
    [Fact]
    public void MassTimesVelocityYieldsMomentum()
    {
        Mass mass = Mass.Of(10, Si<Kilogram>());
        Velocity velocity = Velocity.Of(5, Si<Metre>().Per(Si<Second>()));

        Momentum actual = mass * velocity;

        Assert.Equal(Mass.Of(50, Si<Kilogram>()), actual / Velocity.Of(1, Si<Metre>().Per(Si<Second>())));
    }

    [Fact]
    public void VelocityTimesMassYieldsMomentum()
    {
        Velocity velocity = Velocity.Of(3, Si<Metre>().Per(Si<Second>()));
        Mass mass = Mass.Of(20, Si<Kilogram>());

        Momentum actual = velocity * mass;

        Assert.Equal(Mass.Of(60, Si<Kilogram>()), actual / Velocity.Of(1, Si<Metre>().Per(Si<Second>())));
    }

    [Fact]
    public void MomentumDividedByMassYieldsVelocity()
    {
        Mass mass = Mass.Of(25, Si<Kilogram>());
        Velocity velocity = Velocity.Of(4, Si<Metre>().Per(Si<Second>()));
        Momentum momentum = mass * velocity;
        Velocity expected = Velocity.Of(4, Si<Metre>().Per(Si<Second>()));

        Velocity actual = momentum / mass;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MomentumDividedByVelocityYieldsMass()
    {
        Mass mass = Mass.Of(20, Si<Kilogram>());
        Velocity velocity = Velocity.Of(3, Si<Metre>().Per(Si<Second>()));
        Momentum momentum = mass * velocity;
        Mass expected = Mass.Of(20, Si<Kilogram>());

        Mass actual = momentum / velocity;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ForceTimesTimeYieldsImpulse()
    {
        Force force = Force.Of(50, Si<Newton>());
        Time duration = Time.Of(4, Si<Second>());
        Impulse expected = Impulse.Of(200, Si<Newton>().Times(Si<Second>()));

        Impulse actual = force * duration;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TimeTimesForceYieldsImpulse()
    {
        Time duration = Time.Of(3, Si<Second>());
        Force force = Force.Of(40, Si<Newton>());
        Impulse expected = Impulse.Of(120, Si<Newton>().Times(Si<Second>()));

        Impulse actual = duration * force;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ImpulseDividedByForceYieldsTime()
    {
        Impulse impulse = Impulse.Of(200, Si<Newton>().Times(Si<Second>()));
        Force force = Force.Of(50, Si<Newton>());
        Time expected = Time.Of(4, Si<Second>());

        Time actual = impulse / force;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ImpulseDividedByTimeYieldsForce()
    {
        Impulse impulse = Impulse.Of(120, Si<Newton>().Times(Si<Second>()));
        Time duration = Time.Of(3, Si<Second>());
        Force expected = Force.Of(40, Si<Newton>());

        Force actual = impulse / duration;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnergyDividedByMassYieldsSpecificEnergy()
    {
        Energy energy = Energy.Of(500, Si<Joule>());
        Mass mass = Mass.Of(10, Si<Kilogram>());
        SpecificEnergy expected = SpecificEnergy.Of(50, Si<Joule>().Per(Si<Kilogram>()));

        SpecificEnergy actual = energy / mass;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SpecificEnergyTimesMassYieldsEnergy()
    {
        SpecificEnergy specificEnergy = SpecificEnergy.Of(25, Si<Joule>().Per(Si<Kilogram>()));
        Mass mass = Mass.Of(8, Si<Kilogram>());
        Energy expected = Energy.Of(200, Si<Joule>());

        Energy actual = specificEnergy * mass;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MassTimesSpecificEnergyYieldsEnergy()
    {
        Mass mass = Mass.Of(12, Si<Kilogram>());
        SpecificEnergy specificEnergy = SpecificEnergy.Of(50, Si<Joule>().Per(Si<Kilogram>()));
        Energy expected = Energy.Of(600, Si<Joule>());

        Energy actual = mass * specificEnergy;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnergyDividedBySpecificEnergyYieldsMass()
    {
        Energy energy = Energy.Of(1000, Si<Joule>());
        SpecificEnergy specificEnergy = SpecificEnergy.Of(50, Si<Joule>().Per(Si<Kilogram>()));
        Mass expected = Mass.Of(20, Si<Kilogram>());

        Mass actual = energy / specificEnergy;

        Assert.Equal(expected, actual);
    }
}
