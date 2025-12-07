using System.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Force : IQuantity<Force>, IForce
    , IScalar<Force, IForce>
    , IMultiplyOperators<Force, Velocity, Power>
    , IDivisionOperators<Force, Area, Pressure>
{
    private readonly Quantity force;
    internal Quantity Value => this.force;
    Quantity IQuantity<Force>.Value => this.force;
    private Force(in Quantity value) => this.force = value;
    public Force To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IForce, IUnit => new(other.Transform(in this.force));
    public static Force Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IForce, IUnit => new(measure.Create(in value));
    static Force IFactory<Force>.Create(in Quantity value) => new(in value);
    internal static Force From(in Power power, in Velocity velocity) => new(power.Value / velocity.Value);
    internal static Force From(in Pressure pressure, in Area area) => new(pressure.Value * area.Value);
    public Boolean Equals(Force other) => this.force.Equals(other.force);
    public override Boolean Equals(Object? obj) => obj is Force force && Equals(force);
    public override Int32 GetHashCode() => this.force.GetHashCode();
    public override String ToString() => this.force.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.force.ToString(format, provider);

    public static Power operator *(Force force, Velocity velocity) => Power.From(in force, in velocity);
    public static Pressure operator /(Force force, Area area) => Pressure.From(in force, in area);
}
