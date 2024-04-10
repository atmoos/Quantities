using System.Numerics;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct ElectricCurrent : IQuantity<ElectricCurrent>, IElectricCurrent
    , IScalar<ElectricCurrent, IElectricCurrent>
    , IMultiplyOperators<ElectricCurrent, ElectricPotential, Power>
    , IMultiplyOperators<ElectricCurrent, ElectricalResistance, ElectricPotential>
{
    private readonly Quantity current;
    internal Quantity Value => this.current;
    Quantity IQuantity<ElectricCurrent>.Value => this.current;
    private ElectricCurrent(in Quantity value) => this.current = value;
    public ElectricCurrent To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IElectricCurrent, IUnit => new(other.Transform(in this.current));
    public static ElectricCurrent Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IElectricCurrent, IUnit => new(measure.Create(in value));
    static ElectricCurrent IFactory<ElectricCurrent>.Create(in Quantity value) => new(in value);
    internal static ElectricCurrent From(in ElectricPotential potential, in ElectricalResistance resistance) => new(potential.Value / resistance.Value);
    internal static ElectricCurrent From(in Power power, in ElectricPotential potential) => new(power.Value / potential.Value);
    public Boolean Equals(ElectricCurrent other) => this.current.Equals(other.current);
    public String ToString(String? format, IFormatProvider? provider) => this.current.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricCurrent current && Equals(current);
    public override Int32 GetHashCode() => this.current.GetHashCode();
    public override String ToString() => this.current.ToString();

    public static implicit operator Double(ElectricCurrent current) => current.current;
    public static Boolean operator ==(ElectricCurrent left, ElectricCurrent right) => left.Equals(right);
    public static Boolean operator !=(ElectricCurrent left, ElectricCurrent right) => !left.Equals(right);
    public static Boolean operator >(ElectricCurrent left, ElectricCurrent right) => left.current > right.current;
    public static Boolean operator >=(ElectricCurrent left, ElectricCurrent right) => left.current >= right.current;
    public static Boolean operator <(ElectricCurrent left, ElectricCurrent right) => left.current < right.current;
    public static Boolean operator <=(ElectricCurrent left, ElectricCurrent right) => left.current <= right.current;
    public static ElectricCurrent operator +(ElectricCurrent left, ElectricCurrent right) => new(left.current + right.current);
    public static ElectricCurrent operator -(ElectricCurrent left, ElectricCurrent right) => new(left.current - right.current);
    public static ElectricCurrent operator *(Double scalar, ElectricCurrent right) => new(scalar * right.current);
    public static ElectricCurrent operator *(ElectricCurrent left, Double scalar) => new(scalar * left.current);
    public static ElectricCurrent operator /(ElectricCurrent left, Double scalar) => new(left.current / scalar);
    public static Double operator /(ElectricCurrent left, ElectricCurrent right) => left.current.Ratio(in right.current);

    // Ohm's Law
    public static ElectricPotential operator *(ElectricCurrent current, ElectricalResistance resistance) => ElectricPotential.From(in current, in resistance);
    public static Power operator *(ElectricCurrent current, ElectricPotential potential) => Power.From(in potential, in current);
}
