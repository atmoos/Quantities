using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct Force : IQuantity<Force>, IForce<Mass, Length, Time>
    , ISi<Force, IForce>
    , IImperial<Force, IForce>
    , IMetric<Force, IForce>
    , INoSystem<Force, IForce>
    , IMultiplyOperators<Force, Velocity, Power>
{
    private static readonly IRoot root = new UnitRoot<Newton>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Force(in Quant quant) => this.quant = quant;
    public Force To<TUnit>()
        where TUnit : ISiUnit, IForce
    {
        return new(this.quant.As<Si<TUnit>>());
    }
    public Force To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IForce
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
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
        return new(this.quant.As<Imperial<TUnit>>());
    }
    public Force ToNonStandard<TUnit>()
        where TUnit : INoSystem, IForce
    {
        return new(this.quant.As<NonStandard<TUnit>>());
    }
    public static Force Si<TUnit>(in Double value)
        where TUnit : ISiUnit, IForce
    {
        return new(value.As<Si<TUnit>>());
    }
    public static Force Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IForce
    {
        return new(value.As<Si<TPrefix, TUnit>>());
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
        return new(value.As<Imperial<TUnit>>());
    }
    public static Force NonStandard<TUnit>(in Double value)
        where TUnit : INoSystem, IForce
    {
        return new(value.As<NonStandard<TUnit>>());
    }

    internal static Force From(in Power power, in Velocity velocity)
    {
        return new(MetricPrefix.ScaleThree(power.Quant.SiDivide(velocity.Quant), root));
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
}