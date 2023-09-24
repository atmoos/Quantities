using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;

namespace Quantities.Quantities;

public readonly struct Force : IQuantity<Force>, IForce
    , IFactory<ISiFactory<Force, IForce>, Linear<To, Force, IForce>, Linear<Create, Force, IForce>>
    , IMultiplyOperators<Force, Velocity, Power>
{
    private readonly Quantity force;
    internal Quantity Value => this.force;
    Quantity IQuantity<Force>.Value => this.force;
    public Linear<To, Force, IForce> To => new(new To(in this.force));
    private Force(in Quantity value) => this.force = value;
    public static Linear<Create, Force, IForce> Of(in Double value) => new(new Create(in value));
    static Force IFactory<Force>.Create(in Quantity value) => new(in value);
    internal static Force From(in Power power, in Velocity velocity) => new(power.Value / velocity.Value);
    public Boolean Equals(Force other) => this.force.Equals(other.force);
    public override Boolean Equals(Object? obj) => obj is Force force && Equals(force);
    public override Int32 GetHashCode() => this.force.GetHashCode();
    public override String ToString() => this.force.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.force.ToString(format, provider);

    public static implicit operator Double(Force force) => force.force;
    public static Boolean operator ==(Force left, Force right) => left.Equals(right);
    public static Boolean operator !=(Force left, Force right) => !left.Equals(right);
    public static Force operator +(Force left, Force right) => new(left.force + right.force);
    public static Force operator -(Force left, Force right) => new(left.force - right.force);
    public static Force operator *(Double scalar, Force right) => new(scalar * right.force);
    public static Force operator *(Force left, Double scalar) => new(scalar * left.force);
    public static Force operator /(Force left, Double scalar) => new(left.force / scalar);
    public static Double operator /(Force left, Force right) => left.force.Divide(in right.force);

    public static Power operator *(Force force, Velocity velocity) => Power.From(in force, in velocity);
}
