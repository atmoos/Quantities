using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.Other;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct Force : IQuantity<Force>, IForce<Mass, Length, Time>
    , ISiDerived<Force, IForce>
    , IImperial<Force, IForce>
    , IMetric<Force, IForce>
    , IOther<Force, IForce>
    , IMultiplyOperators<Force, Velocity, Power>
{
    private static readonly Creator create = new();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Force(in Quant quant) => this.quant = quant;
    public Force To<TUnit>()
        where TUnit : ISiDerivedUnit, IForce
    {
        return new(this.quant.As<SiDerived<TUnit>>());
    }
    public Force To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiDerivedUnit, IForce
    {
        return new(this.quant.As<SiDerived<TPrefix, TUnit>>());
    }
    public Force ToMetric<TUnit>() where TUnit : IMetricUnit, IForce
    {
        return new(this.quant.As<Metric<TUnit>>());
    }
    public Force ToMetric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IForce
    {
        return new(this.quant.As<Metric<TPrefix, TUnit>>());
    }
    public Force ToImperial<TUnit>()
        where TUnit : IImperial, IForce
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public Force ToOther<TUnit>()
        where TUnit : IOther, IForce
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public static Force Si<TUnit>(in Double value)
        where TUnit : ISiDerivedUnit, IForce
    {
        return new(value.As<SiDerived<TUnit>>());
    }
    public static Force Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiDerivedUnit, IForce
    {
        return new(value.As<SiDerived<TPrefix, TUnit>>());
    }
    public static Force Metric<TUnit>(in Double value) where TUnit : IMetricUnit, IForce
    {
        return new(value.As<Metric<TUnit>>());
    }

    public static Force Metric<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IForce
    {
        return new(value.As<Metric<TPrefix, TUnit>>());
    }
    public static Force Imperial<TUnit>(in Double value)
        where TUnit : IImperial, IForce
    {
        return new(value.As<Other<TUnit>>());
    }
    public static Force Other<TUnit>(in Double value)
        where TUnit : IOther, IForce
    {
        return new(value.As<Other<TUnit>>());
    }

    internal static Force From(in Power power, in Velocity velocity)
    {
        return new(MetricPrefix.ScaleThree(power.Quant.SiDivide(velocity.Quant), create));
    }

    public Boolean Equals(Force other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Force force && Equals(force);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Force left, Force right) => left.Equals(right);
    public static Boolean operator !=(Force left, Force right) => !left.Equals(right);
    public static implicit operator Double(Force force) => force.quant.Value;
    public static Force operator +(Force left, Force right) => new(left.quant + right.quant);
    public static Force operator -(Force left, Force right) => new(left.quant - right.quant);
    public static Force operator *(Double scalar, Force right) => new(scalar * right.quant);
    public static Force operator *(Force left, Double scalar) => new(scalar * left.quant);
    public static Force operator /(Force left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Force left, Force right) => left.quant / right.quant;

    public static Power operator *(Force force, Velocity velocity) => Power.From(in force, in velocity);

    private sealed class Creator : IPrefixInject<Quant>
    {
        public Quant Identity(in Double value) => value.As<SiDerived<Newton>>();
        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value.As<SiDerived<TPrefix, Newton>>();
    }
}