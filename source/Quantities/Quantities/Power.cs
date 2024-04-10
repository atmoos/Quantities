using System.Numerics;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Power : IQuantity<Power>, IPower
    , IScalar<Power, IPower>
    , IMultiplyOperators<Power, Time, Energy>
    , IDivisionOperators<Power, ElectricCurrent, ElectricPotential>
    , IDivisionOperators<Power, ElectricPotential, ElectricCurrent>
    , IDivisionOperators<Power, Force, Velocity>
    , IDivisionOperators<Power, Velocity, Force>
{
    private readonly Quantity power;
    internal Quantity Value => this.power;
    Quantity IQuantity<Power>.Value => this.power;
    private Power(in Quantity value) => this.power = value;
    public Power To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IPower, IUnit => new(other.Transform(in this.power));
    public static Power Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IPower, IUnit => new(measure.Create(in value));
    static Power IFactory<Power>.Create(in Quantity value) => new(in value);
    internal static Power From(in ElectricPotential potential, in ElectricCurrent current) => new(potential.Value * current.Value);
    internal static Power From(in Force force, in Velocity velocity) => new(force.Value * velocity.Value);
    internal static Power From(in Energy energy, in Time time) => new(energy.Value / time.Value);
    public Boolean Equals(Power other) => this.power.Equals(other.power);
    public override Boolean Equals(Object? obj) => obj is Power power && Equals(power);
    public override Int32 GetHashCode() => this.power.GetHashCode();
    public override String ToString() => this.power.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.power.ToString(format, provider);

    public static implicit operator Double(Power power) => power.power;
    public static Boolean operator ==(Power left, Power right) => left.Equals(right);
    public static Boolean operator !=(Power left, Power right) => !left.Equals(right);
    public static Boolean operator >(Power left, Power right) => left.power > right.power;
    public static Boolean operator >=(Power left, Power right) => left.power >= right.power;
    public static Boolean operator <(Power left, Power right) => left.power < right.power;
    public static Boolean operator <=(Power left, Power right) => left.power <= right.power;
    public static Power operator +(Power left, Power right) => new(left.power + right.power);
    public static Power operator -(Power left, Power right) => new(left.power - right.power);
    public static Power operator *(Double scalar, Power right) => new(scalar * right.power);
    public static Power operator *(Power left, Double scalar) => new(scalar * left.power);
    public static Power operator /(Power left, Double scalar) => new(left.power / scalar);
    public static Double operator /(Power left, Power right) => left.power.Ratio(in right.power);

    public static ElectricPotential operator /(Power power, ElectricCurrent current) => ElectricPotential.From(in power, in current);
    public static ElectricCurrent operator /(Power power, ElectricPotential potential) => ElectricCurrent.From(in power, in potential);
    public static Velocity operator /(Power power, Force force) => Velocity.From(in power, in force);
    public static Force operator /(Power power, Velocity velocity) => Force.From(in power, in velocity);
    public static Energy operator *(Power power, Time time) => Energy.From(in power, in time);
}
