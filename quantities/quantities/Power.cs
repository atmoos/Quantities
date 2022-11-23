using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct Power : IQuantity<Power>, IPower
    , ISiDerived<Power, IPower>
    , IImperial<Power, IPower>
    , IMetric<Power, IPower>
    , IDivisionOperators<Power, ElectricCurrent, ElectricPotential>
    , IDivisionOperators<Power, ElectricPotential, ElectricCurrent>
    , IDivisionOperators<Power, Force, Velocity>
    , IDivisionOperators<Power, Velocity, Force>
{
    private static readonly Creator create = new();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Power(in Quant quant) => this.quant = quant;
    public Power To<TUnit>()
        where TUnit : ISiDerivedUnit, IPower
    {
        return new(this.quant.As<SiDerived<TUnit>>());
    }
    public Power To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiDerivedUnit, IPower
    {
        return new(this.quant.As<SiDerived<TPrefix, TUnit>>());
    }
    public Power ToMetric<TUnit>()
        where TUnit : ISiAcceptedUnit, IPower
    {
        return new(this.quant.As<SiAccepted<TUnit>>());
    }
    public Power ToMetric<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiAcceptedUnit, IPower
    {
        return new(this.quant.As<SiAccepted<TPrefix, TUnit>>());
    }
    public Power ToImperial<TUnit>()
        where TUnit : IImperial, IPower
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public static Power Si<TUnit>(in Double value)
        where TUnit : ISiDerivedUnit, IPower
    {
        return new(value.As<SiDerived<TUnit>>());
    }
    public static Power Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiDerivedUnit, IPower
    {
        return new(value.As<SiDerived<TPrefix, TUnit>>());
    }
    public static Power Imperial<TUnit>(in Double value)
        where TUnit : IImperial, IPower
    {
        return new(value.As<Other<TUnit>>());
    }
    // ToDo: Consider renaming ISiAccepted to Metric
    public static Power Metric<TUnit>(in Double value)
        where TUnit : ISiAcceptedUnit, IPower
    {
        return new(value.As<SiAccepted<TUnit>>());
    }
    public static Power Metric<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiAcceptedUnit, IPower
    {
        return new(value.As<SiAccepted<TPrefix, TUnit>>());
    }

    internal static Power From(in ElectricPotential potential, in ElectricCurrent current)
    {
        Double siCurrent = current.To<Ampere>();
        Double siPotential = potential.To<Volt>();
        return new(SiPrefix.ScaleThree(siPotential * siCurrent, create));
    }
    internal static Power From(in Force force, in Velocity velocity)
    {
        Double siForce = force.To<Newton>();
        Double siVelocity = velocity.To<Metre>().PerSecond();
        return new(SiPrefix.ScaleThree(siForce * siVelocity, create));
    }

    public Boolean Equals(Power other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Power power && Equals(power);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Power left, Power right) => left.Equals(right);
    public static Boolean operator !=(Power left, Power right) => !left.Equals(right);
    public static implicit operator Double(Power power) => power.quant.Value;
    public static Power operator +(Power left, Power right) => new(left.quant + right.quant);
    public static Power operator -(Power left, Power right) => new(left.quant - right.quant);
    public static Power operator *(Double scalar, Power right) => new(scalar * right.quant);
    public static Power operator *(Power left, Double scalar) => new(scalar * left.quant);
    public static Power operator /(Power left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Power left, Power right) => left.quant / right.quant;

    public static ElectricPotential operator /(Power power, ElectricCurrent current) => ElectricPotential.From(in power, in current);
    public static ElectricCurrent operator /(Power power, ElectricPotential potential) => ElectricCurrent.From(in power, in potential);
    public static Velocity operator /(Power power, Force force) => Velocity.From(in power, in force);
    public static Force operator /(Power power, Velocity velocity) => Force.From(in power, in velocity);

    private sealed class Creator : IPrefixInject<Quant>
    {
        public Quant Identity(in Double value) => value.As<SiDerived<Watt>>();
        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value.As<SiDerived<TPrefix, Watt>>();
    }
}
