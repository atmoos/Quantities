using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Energy : IQuantity<Energy>, IEnergy<Mass, Length, Time>
    , ISi<Energy, IEnergy>
    , IImperial<Energy, IEnergy>
    , IDivisionOperators<Energy, Time, Power>
    , IDivisionOperators<Energy, Power, Time>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Energy(in Quant quant) => this.quant = quant;
    public Energy To<TUnit>()
        where TUnit : ISiUnit, IEnergy
    {
        return new(this.quant.As<Si<TUnit>>());
    }
    public Energy To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IEnergy
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public Energy ToMetric<TPrefix, TPowerUnit, TTimeUnit>()
        where TPrefix : IMetricPrefix
        where TPowerUnit : ISiUnit, IPower
        where TTimeUnit : IMetricUnit, ITransform, ITime
    {
        return new(this.quant.AsProduct<Si<TPrefix, TPowerUnit>, Metric<TTimeUnit>>());
    }
    public Energy ToImperial<TUnit>()
        where TUnit : IImperial, IEnergy
    {
        return new(this.quant.As<Imperial<TUnit>>());
    }
    public static Energy Si<TUnit>(in Double value)
        where TUnit : ISiUnit, IEnergy
    {
        return new(value.As<Si<TUnit>>());
    }
    public static Energy Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IEnergy
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }
    public static Energy Si<TPrefix, TPowerUnit, TTimeUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TPowerUnit : ISiUnit, IPower
        where TTimeUnit : ISiUnit, ITime
    {
        return new(value.AsProduct<Si<TPrefix, TPowerUnit>, Si<TTimeUnit>>());
    }
    public static Energy Si<TPowerPrefix, TPowerUnit, TTimePrefix, TTimeUnit>(in Double value)
        where TPowerPrefix : IPrefix
        where TPowerUnit : ISiUnit, IPower
        where TTimePrefix : IPrefix
        where TTimeUnit : ISiUnit, ITime
    {
        return new(value.AsProduct<Si<TPowerPrefix, TPowerUnit>, Si<TTimePrefix, TTimeUnit>>());
    }
    public static Energy Metric<TPrefix, TPowerUnit, TTimeUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TPowerUnit : ISiUnit, IPower
        where TTimeUnit : IMetricUnit, ITransform, ITime
    {
        return new(value.AsProduct<Si<TPrefix, TPowerUnit>, Metric<TTimeUnit>>());
    }
    public static Energy Imperial<TUnit>(in Double value)
        where TUnit : IImperial, IEnergy
    {
        return new(value.As<Imperial<TUnit>>());
    }
    internal static Energy From(in Power power, in Time time) => new(power.Quant.Multiply(time.Quant));

    public Boolean Equals(Energy other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Energy energy && Equals(energy);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Energy left, Energy right) => left.Equals(right);
    public static Boolean operator !=(Energy left, Energy right) => !left.Equals(right);
    public static implicit operator Double(Energy energy) => energy.quant.Value;
    public static Energy operator +(Energy left, Energy right) => new(left.quant + right.quant);
    public static Energy operator -(Energy left, Energy right) => new(left.quant - right.quant);
    public static Energy operator *(Double scalar, Energy right) => new(scalar * right.quant);
    public static Energy operator *(Energy left, Double scalar) => new(scalar * left.quant);
    public static Energy operator /(Energy left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Energy left, Energy right) => left.quant / right.quant;
    public static Power operator /(Energy left, Time right) => Power.From(in left, in right);
    public static Time operator /(Energy left, Power right) => Time.From(in left, in right);
}