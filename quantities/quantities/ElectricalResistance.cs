using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct ElectricalResistance : IQuantity<ElectricalResistance>, IElectricalResistance
    , ISiDerived<ElectricalResistance, IElectricalResistance>
    , IMultiplyOperators<ElectricalResistance, ElectricCurrent, ElectricPotential>
{
    private static readonly Creator create = new();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private ElectricalResistance(in Quant quant) => this.quant = quant;
    public ElectricalResistance To<TUnit>()
        where TUnit : ISiDerivedUnit, IElectricalResistance
    {
        return new(this.quant.As<SiDerived<TUnit>>());
    }
    public ElectricalResistance To<TPrefix, TUnit>()
        where TPrefix : ISiPrefix
        where TUnit : ISiDerivedUnit, IElectricalResistance
    {
        return new(this.quant.As<SiDerived<TPrefix, TUnit>>());
    }
    public static ElectricalResistance Si<TUnit>(in Double value)
        where TUnit : ISiDerivedUnit, IElectricalResistance
    {
        return new(value.As<SiDerived<TUnit>>());
    }
    public static ElectricalResistance Si<TPrefix, TUnit>(in Double value)
        where TPrefix : ISiPrefix
        where TUnit : ISiDerivedUnit, IElectricalResistance
    {
        return new(value.As<SiDerived<TPrefix, TUnit>>());
    }

    public Boolean Equals(ElectricalResistance other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is ElectricalResistance resistance && Equals(resistance);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    internal static ElectricalResistance From(in ElectricPotential potential, in ElectricCurrent current)
    {
        return new(SiPrefix.ScaleThree(potential.Quant.SiDivide(current.Quant), create));
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

    private sealed class Creator : IPrefixInject<Quant>
    {
        public Quant Identity(in Double value) => value.As<SiDerived<Ohm>>();
        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value.As<SiDerived<TPrefix, Ohm>>();
    }
}
