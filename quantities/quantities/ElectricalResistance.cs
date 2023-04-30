using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct ElectricalResistance : IQuantity<ElectricalResistance>, IElectricalResistance
    , ISi<ElectricalResistance, IElectricalResistance>
    , IMultiplyOperators<ElectricalResistance, ElectricCurrent, ElectricPotential>
{
    private static readonly IRoot root = new UnitRoot<Ohm>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private ElectricalResistance(in Quant quant) => this.quant = quant;
    public ElectricalResistance To<TUnit>()
        where TUnit : ISiUnit, IElectricalResistance
    {
        return new(this.quant.As<Si<TUnit>>());
    }
    public ElectricalResistance To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IElectricalResistance
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public static ElectricalResistance Si<TUnit>(in Double value)
        where TUnit : ISiUnit, IElectricalResistance
    {
        return new(value.As<Si<TUnit>>());
    }
    public static ElectricalResistance Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IElectricalResistance
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }

    public Boolean Equals(ElectricalResistance other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is ElectricalResistance resistance && Equals(resistance);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    internal static ElectricalResistance From(in ElectricPotential potential, in ElectricCurrent current)
    {
        return new(MetricPrefix.ScaleThree(potential.Quant.SiDivide(current.Quant), root));
    }
    public static Boolean operator ==(ElectricalResistance left, ElectricalResistance right) => left.Equals(right);
    public static Boolean operator !=(ElectricalResistance left, ElectricalResistance right) => !left.Equals(right);
    public static implicit operator Double(ElectricalResistance resistance) => resistance.quant.Value;
    public static ElectricalResistance operator +(ElectricalResistance left, ElectricalResistance right) => new(left.quant + right.quant);
    public static ElectricalResistance operator -(ElectricalResistance left, ElectricalResistance right) => new(left.quant - right.quant);
    public static ElectricalResistance operator *(Double scalar, ElectricalResistance right) => new(scalar * right.quant);
    public static ElectricalResistance operator *(ElectricalResistance left, Double scalar) => new(scalar * left.quant);
    public static ElectricalResistance operator /(ElectricalResistance left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(ElectricalResistance left, ElectricalResistance right) => left.quant / right.quant;

    // Ohm's Law
    public static ElectricPotential operator *(ElectricalResistance resistance, ElectricCurrent current) => ElectricPotential.From(in current, in resistance);
}
