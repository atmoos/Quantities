using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;

namespace Quantities.Quantities;

public readonly struct Force : IQuantity<Force>, IForce
    , IFactory<ISiFactory<Force, IForce>, Linear<To, Force, IForce>, Linear<Create, Force, IForce>>
    , IMultiplyOperators<Force, Velocity, Power>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<Force>.Value => this.quant;
    public Linear<To, Force, IForce> To => new(new To(in this.quant));
    private Force(in Quant quant) => this.quant = quant;
    public static Linear<Create, Force, IForce> Of(in Double value) => new(new Create(in value));
    static Force IFactory<Force>.Create(in Quant quant) => new(in quant);
    internal static Force From(in Power power, in Velocity velocity)
    {
        return new(power.Quant.Divide(Metric.TriadicScaling, velocity.Quant));
    }
    public Boolean Equals(Force other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Force force && Equals(force);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Force left, Force right) => left.Equals(right);
    public static Boolean operator !=(Force left, Force right) => !left.Equals(right);
    public static implicit operator Double(Force force) => force.quant.Value;
    public static Force operator +(Force left, Force right) => new(left.quant + right.quant);
    public static Force operator -(Force left, Force right) => new(left.quant - right.quant);
    public static Force operator *(Double scalar, Force right) => new(scalar * right.quant);
    public static Force operator *(Force left, Double scalar) => new(scalar * left.quant);
    public static Force operator /(Force left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Force left, Force right) => left.quant / right.quant;

    public static Power operator *(Force force, Velocity velocity) => Power.From(in force, in velocity);
}