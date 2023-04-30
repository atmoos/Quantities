using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Mass : IQuantity<Mass>, IMass
    , ISi<Mass, IMass>
    , IMetric<Mass, IMass>
    , IImperial<Mass, IMass>
{
    private readonly Quant quant;
    private Mass(in Quant quant) => this.quant = quant;
    public Mass To<TUnit>()
        where TUnit : ISiUnit, IMass
    {
        return new(this.quant.As<Si<TUnit>>());
    }
    public Mass To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IMass
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public Mass ToMetric<TUnit>() where TUnit : IMetricUnit, IMass
    {
        return new(this.quant.As<Metric<TUnit>>());
    }
    public Mass ToMetric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IMass
    {
        return new(this.quant.As<Metric<TPrefix, TUnit>>());
    }
    public Mass ToImperial<TUnit>()
        where TUnit : IImperial, IMass
    {
        return new(this.quant.As<Imperial<TUnit>>());
    }
    public static Mass Si<TUnit>(in Double value)
        where TUnit : ISiUnit, IMass
    {
        return new(value.As<Si<TUnit>>());
    }
    public static Mass Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IMass
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }
    public static Mass Metric<TUnit>(in Double value) where TUnit : IMetricUnit, IMass
    {
        return new(value.As<Metric<TUnit>>());
    }
    public static Mass Metric<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IMass
    {
        return new(value.As<Metric<TPrefix, TUnit>>());
    }
    public static Mass Imperial<TUnit>(in Double value)
        where TUnit : IImperial, IMass
    {
        return new(value.As<Imperial<TUnit>>());
    }

    public Boolean Equals(Mass other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Mass mass && Equals(mass);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Mass left, Mass right) => left.Equals(right);
    public static Boolean operator !=(Mass left, Mass right) => !left.Equals(right);
    public static implicit operator Double(Mass mass) => mass.quant.Value;
    public static Mass operator +(Mass left, Mass right) => new(left.quant + right.quant);
    public static Mass operator -(Mass left, Mass right) => new(left.quant - right.quant);
    public static Mass operator *(Double scalar, Mass right) => new(scalar * right.quant);
    public static Mass operator *(Mass left, Double scalar) => new(scalar * left.quant);
    public static Mass operator /(Mass left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Mass left, Mass right) => left.quant / right.quant;
}
