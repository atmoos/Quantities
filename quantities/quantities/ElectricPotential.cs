using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct ElectricPotential : IQuantity<ElectricPotential>, IElectricPotential
    , ISiDerived<ElectricPotential, IElectricPotential>
    , IMultiplyOperators<ElectricPotential, ElectricCurrent, Power>
    , IDivisionOperators<ElectricPotential, ElectricCurrent, ElectricalResistance>
    , IDivisionOperators<ElectricPotential, ElectricalResistance, ElectricCurrent>
{
    private static readonly Creator create = new();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private ElectricPotential(in Quant quant) => this.quant = quant;
    public ElectricPotential To<TUnit>()
        where TUnit : ISiDerivedUnit, IElectricPotential
    {
        return new(this.quant.As<SiDerived<TUnit>>());
    }
    public ElectricPotential To<TPrefix, TUnit>()
        where TPrefix : ISiPrefix
        where TUnit : ISiDerivedUnit, IElectricPotential
    {
        return new(this.quant.As<SiDerived<TPrefix, TUnit>>());
    }
    public static ElectricPotential Si<TUnit>(in Double value)
        where TUnit : ISiDerivedUnit, IElectricPotential
    {
        return new(value.As<SiDerived<TUnit>>());
    }
    public static ElectricPotential Si<TPrefix, TUnit>(in Double value)
        where TPrefix : ISiPrefix
        where TUnit : ISiDerivedUnit, IElectricPotential
    {
        return new(value.As<SiDerived<TPrefix, TUnit>>());
    }

    public Boolean Equals(ElectricPotential other) => this.quant.Equals(other.quant);
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricPotential potential && Equals(potential);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    internal static ElectricPotential From(in ElectricCurrent current, in ElectricalResistance resistance)
    {
        return new(SiPrefix.ScaleThree(current.Quant.SiMultiply(resistance.Quant), create));
    }
    internal static ElectricPotential From(in Power power, in ElectricCurrent current)
    {
        Double siPower = power.To<Watt>();
        Double siCurrent = current.To<Ampere>();
        return new(SiPrefix.ScaleThree(siPower / siCurrent, create));
    }

    public static Boolean operator ==(ElectricPotential left, ElectricPotential right) => left.Equals(right);
    public static Boolean operator !=(ElectricPotential left, ElectricPotential right) => !left.Equals(right);
    public static implicit operator Double(ElectricPotential potential) => potential.quant.Value;
    public static ElectricPotential operator +(ElectricPotential left, ElectricPotential right) => new(left.quant + right.quant);
    public static ElectricPotential operator -(ElectricPotential left, ElectricPotential right) => new(left.quant - right.quant);
    public static ElectricPotential operator *(Double scalar, ElectricPotential right) => new(scalar * right.quant);
    public static ElectricPotential operator *(ElectricPotential left, Double scalar) => new(scalar * left.quant);
    public static ElectricPotential operator /(ElectricPotential left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(ElectricPotential left, ElectricPotential right) => left.quant / right.quant;

    #region Ohm's Law
    public static ElectricalResistance operator /(ElectricPotential left, ElectricCurrent right) => ElectricalResistance.From(in left, in right);
    public static ElectricCurrent operator /(ElectricPotential left, ElectricalResistance right) => ElectricCurrent.From(in left, in right);

    #endregion Ohm's Law

    public static Power operator *(ElectricPotential left, ElectricCurrent right) => Power.From(in left, in right);

    private sealed class Creator : IPrefixInject<Quant>
    {
        public Quant Identity(in Double value) => value.As<SiDerived<Volt>>();
        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value.As<SiDerived<TPrefix, Volt>>();
    }
}
