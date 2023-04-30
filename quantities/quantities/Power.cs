using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Imperial;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct Power : IQuantity<Power>, IPower
    , ISi<Power, IPower>
    , IImperial<Power, IPower>
    , IMetric<Power, IPower>
    , IMultiplyOperators<Power, Time, Energy>
    , IDivisionOperators<Power, ElectricCurrent, ElectricPotential>
    , IDivisionOperators<Power, ElectricPotential, ElectricCurrent>
    , IDivisionOperators<Power, Force, Velocity>
    , IDivisionOperators<Power, Velocity, Force>
{
    private static readonly IRoot root = new UnitRoot<Watt>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Power(in Quant quant) => this.quant = quant;
    public Power To<TUnit>()
        where TUnit : ISiUnit, IPower
    {
        return new(this.quant.As<Si<TUnit>>());
    }
    public Power To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IPower
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public Power ToMetric<TUnit>()
        where TUnit : IMetricUnit, IPower
    {
        return new(this.quant.As<Metric<TUnit>>());
    }
    public Power ToMetric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IPower
    {
        return new(this.quant.As<Metric<TPrefix, TUnit>>());
    }
    public Power ToImperial<TUnit>()
        where TUnit : IImperial, IPower
    {
        return new(this.quant.As<Imperial<TUnit>>());
    }
    public static Power Si<TUnit>(in Double value)
        where TUnit : ISiUnit, IPower
    {
        return new(value.As<Si<TUnit>>());
    }
    public static Power Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IPower
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }
    public static Power Imperial<TUnit>(in Double value)
        where TUnit : IImperial, IPower
    {
        return new(value.As<Imperial<TUnit>>());
    }
    public static Power Metric<TUnit>(in Double value)
        where TUnit : IMetricUnit, IPower
    {
        return new(value.As<Metric<TUnit>>());
    }
    public static Power Metric<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IPower
    {
        return new(value.As<Metric<TPrefix, TUnit>>());
    }

    internal static Power From(in ElectricPotential potential, in ElectricCurrent current)
    {
        return new(MetricPrefix.ScaleThree(potential.Quant.SiMultiply(current.Quant), root));
    }
    internal static Power From(in Force force, in Velocity velocity)
    {
        return new(MetricPrefix.ScaleThree(force.Quant.SiMultiply(velocity.Quant), root));
    }
    internal static Power From(in Energy energy, in Time time)
    {
        return new(MetricPrefix.ScaleThree(energy.Quant.SiDivide(time.Quant), root));
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
    public static Energy operator *(Power left, Time right) => Energy.From(in left, in right);
}
