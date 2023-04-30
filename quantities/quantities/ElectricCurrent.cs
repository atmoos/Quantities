using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct ElectricCurrent : IQuantity<ElectricCurrent>, IElectricCurrent
    , ISi<ElectricCurrent, IElectricCurrent>
    , IMultiplyOperators<ElectricCurrent, ElectricPotential, Power>
    , IMultiplyOperators<ElectricCurrent, ElectricalResistance, ElectricPotential>
{
    private static readonly Creator create = new();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private ElectricCurrent(in Quant quant) => this.quant = quant;
    public ElectricCurrent To<TUnit>()
        where TUnit : ISiUnit, IElectricCurrent
    {
        return new(this.quant.As<Si<TUnit>>());
    }
    public ElectricCurrent To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IElectricCurrent
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public static ElectricCurrent Si<TUnit>(in Double value)
        where TUnit : ISiUnit, IElectricCurrent
    {
        return new(value.As<Si<TUnit>>());
    }
    public static ElectricCurrent Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IElectricCurrent
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }

    public Boolean Equals(ElectricCurrent other) => this.quant.Equals(other.quant);
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricCurrent current && Equals(current);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    internal static ElectricCurrent From(in ElectricPotential potential, in ElectricalResistance resistance)
    {
        return new(MetricPrefix.ScaleThree(potential.Quant.SiDivide(resistance.Quant), create));
    }
    internal static ElectricCurrent From(in Power power, in ElectricPotential potential)
    {
        return new(MetricPrefix.ScaleThree(power.Quant.SiDivide(potential.Quant), create));
    }
    public static Boolean operator ==(ElectricCurrent left, ElectricCurrent right) => left.Equals(right);
    public static Boolean operator !=(ElectricCurrent left, ElectricCurrent right) => !left.Equals(right);
    public static implicit operator Double(ElectricCurrent current) => current.quant.Value;
    public static ElectricCurrent operator +(ElectricCurrent left, ElectricCurrent right) => new(left.quant + right.quant);
    public static ElectricCurrent operator -(ElectricCurrent left, ElectricCurrent right) => new(left.quant - right.quant);
    public static ElectricCurrent operator *(Double scalar, ElectricCurrent right) => new(scalar * right.quant);
    public static ElectricCurrent operator *(ElectricCurrent left, Double scalar) => new(scalar * left.quant);
    public static ElectricCurrent operator /(ElectricCurrent left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(ElectricCurrent left, ElectricCurrent right) => left.quant / right.quant;

    // Ohm's Law
    public static ElectricPotential operator *(ElectricCurrent current, ElectricalResistance resistance) => ElectricPotential.From(in current, in resistance);
    public static Power operator *(ElectricCurrent left, ElectricPotential right) => Power.From(in right, in left);

    private sealed class Creator : IPrefixInject<Quant>
    {
        public Quant Identity(in Double value) => value.As<Si<Ampere>>();
        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value.As<Si<TPrefix, Ampere>>();
    }
}
