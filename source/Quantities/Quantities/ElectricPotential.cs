using System.Numerics;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct ElectricPotential : IQuantity<ElectricPotential>, IElectricPotential
    , IScalar<ElectricPotential, IElectricPotential>
    , IMultiplyOperators<ElectricPotential, ElectricCurrent, Power>
    , IDivisionOperators<ElectricPotential, ElectricCurrent, ElectricalResistance>
    , IDivisionOperators<ElectricPotential, ElectricalResistance, ElectricCurrent>
{
    private readonly Quantity potential;
    internal Quantity Value => this.potential;
    Quantity IQuantity<ElectricPotential>.Value => this.potential;
    private ElectricPotential(in Quantity value) => this.potential = value;
    public ElectricPotential To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IElectricPotential, IUnit => new(other.Transform(in this.potential));
    public static ElectricPotential Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IElectricPotential, IUnit => new(measure.Create(in value));
    static ElectricPotential IFactory<ElectricPotential>.Create(in Quantity value) => new(in value);
    internal static ElectricPotential From(in ElectricCurrent current, in ElectricalResistance resistance) => new(current.Value * resistance.Value);
    internal static ElectricPotential From(in Power power, in ElectricCurrent current) => new(power.Value / current.Value);
    public Boolean Equals(ElectricPotential other) => this.potential.Equals(other.potential);
    public String ToString(String? format, IFormatProvider? provider) => this.potential.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricPotential potential && Equals(potential);
    public override Int32 GetHashCode() => this.potential.GetHashCode();
    public override String ToString() => this.potential.ToString();

    public static implicit operator Double(ElectricPotential potential) => potential.potential;
    public static Boolean operator ==(ElectricPotential left, ElectricPotential right) => left.Equals(right);
    public static Boolean operator !=(ElectricPotential left, ElectricPotential right) => !left.Equals(right);
    public static ElectricPotential operator +(ElectricPotential left, ElectricPotential right) => new(left.potential + right.potential);
    public static ElectricPotential operator -(ElectricPotential left, ElectricPotential right) => new(left.potential - right.potential);
    public static ElectricPotential operator *(Double scalar, ElectricPotential right) => new(scalar * right.potential);
    public static ElectricPotential operator *(ElectricPotential left, Double scalar) => new(scalar * left.potential);
    public static ElectricPotential operator /(ElectricPotential left, Double scalar) => new(left.potential / scalar);
    public static Double operator /(ElectricPotential left, ElectricPotential right) => left.potential.Divide(in right.potential);

    #region Ohm's Law
    public static ElectricalResistance operator /(ElectricPotential potential, ElectricCurrent current) => ElectricalResistance.From(in potential, in current);
    public static ElectricCurrent operator /(ElectricPotential potential, ElectricalResistance resistance) => ElectricCurrent.From(in potential, in resistance);
    #endregion Ohm's Law

    public static Power operator *(ElectricPotential potential, ElectricCurrent current) => Power.From(in potential, in current);
}
