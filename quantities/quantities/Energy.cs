using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Energy : IQuantity<Energy>, IEnergy<Mass, Length, Time>
    , ISiDerived<Energy, IEnergy>
    , IImperial<Energy, IEnergy>
    , IDivisionOperators<Energy, Time, Power>
    , IDivisionOperators<Energy, Power, Time>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Energy(in Quant quant) => this.quant = quant;
    public Energy To<TUnit>()
        where TUnit : ISiDerivedUnit, IEnergy
    {
        return new(this.quant.As<SiDerived<TUnit>>());
    }
    public Energy To<TPrefix, TUnit>()
        where TPrefix : ISiPrefix
        where TUnit : ISiDerivedUnit, IEnergy
    {
        return new(this.quant.As<SiDerived<TPrefix, TUnit>>());
    }
    public Energy ToMetric<TPrefix, TPowerUnit, TTimeUnit>()
        where TPrefix : ISiPrefix
        where TPowerUnit : ISiDerivedUnit, IPower
        where TTimeUnit : IMetricUnit, ITransform, ITime
    {
        return new(this.quant.AsProduct<SiDerived<TPrefix, TPowerUnit>, Metric<TTimeUnit>>());
    }
    public Energy ToImperial<TUnit>()
        where TUnit : IImperial, IEnergy
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public static Energy Si<TUnit>(in Double value)
        where TUnit : ISiDerivedUnit, IEnergy
    {
        return new(value.As<SiDerived<TUnit>>());
    }
    public static Energy Si<TPrefix, TUnit>(in Double value)
        where TPrefix : ISiPrefix
        where TUnit : ISiDerivedUnit, IEnergy
    {
        return new(value.As<SiDerived<TPrefix, TUnit>>());
    }
    public static Energy Si<TPrefix, TPowerUnit, TTimeUnit>(in Double value)
        where TPrefix : ISiPrefix
        where TPowerUnit : ISiDerivedUnit, IPower
        where TTimeUnit : ISiBaseUnit, ITime
    {
        return new(value.AsProduct<SiDerived<TPrefix, TPowerUnit>, Si<TTimeUnit>>());
    }
    public static Energy Si<TPowerPrefix, TPowerUnit, TTimePrefix, TTimeUnit>(in Double value)
        where TPowerPrefix : IPrefix
        where TPowerUnit : ISiDerivedUnit, IPower
        where TTimePrefix : IPrefix
        where TTimeUnit : ISiBaseUnit, ITime
    {
        return new(value.AsProduct<SiDerived<TPowerPrefix, TPowerUnit>, Si<TTimePrefix, TTimeUnit>>());
    }
    public static Energy Metric<TPrefix, TPowerUnit, TTimeUnit>(in Double value)
        where TPrefix : ISiPrefix
        where TPowerUnit : ISiDerivedUnit, IPower
        where TTimeUnit : IMetricUnit, ITransform, ITime
    {
        return new(value.AsProduct<SiDerived<TPrefix, TPowerUnit>, Metric<TTimeUnit>>());
    }
    public static Energy Imperial<TUnit>(in Double value)
        where TUnit : IImperial, IEnergy
    {
        return new(value.As<Other<TUnit>>());
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