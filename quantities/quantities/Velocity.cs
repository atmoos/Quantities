using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Creation;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Velocity : IQuantity<Velocity>, IVelocity<ILength, ITime>
    , IFactory<Velocity>
    , IFactory<ICompoundFactory<Denominator<LinearFactory<Velocity, ITime>>, ILength>, Nominator<To, ILength, LinearFactory<Velocity, ITime>>, Nominator<Create, ILength, LinearFactory<Velocity, ITime>>>
    , IMultiplyOperators<Velocity, Force, Power>
    , IMultiplyOperators<Velocity, Time, Length>
{
    private static readonly IRoot root = new FractionalRoot<Metre, Second>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    public Nominator<To, ILength, LinearFactory<Velocity, ITime>> To => new(new To(in this.quant));
    internal Velocity(in Quant quant) => this.quant = quant;
    public static Nominator<Create, ILength, LinearFactory<Velocity, ITime>> Of(in Double value) => new(new Create(in value));
    static Velocity IFactory<Velocity>.Create(in Quant quant) => new(in quant);
    internal static Velocity From(in Power power, in Force force)
    {
        return new(MetricPrefix.Scale(power.Quant.SiDivide(force.Quant), root));
    }
    internal static Velocity From(in Length length, in Time time) => new(length.Quant.Divide(time.Quant));

    public Boolean Equals(Velocity other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Velocity velocity && Equals(velocity);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Velocity left, Velocity right) => left.Equals(right);
    public static Boolean operator !=(Velocity left, Velocity right) => !left.Equals(right);
    public static implicit operator Double(Velocity velocity) => velocity.quant.Value;
    public static Velocity operator +(Velocity left, Velocity right) => new(left.quant + right.quant);
    public static Velocity operator -(Velocity left, Velocity right) => new(left.quant - right.quant);
    public static Velocity operator *(Double scalar, Velocity right) => new(scalar * right.quant);
    public static Velocity operator *(Velocity left, Double scalar) => new(scalar * left.quant);
    public static Velocity operator /(Velocity left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Velocity left, Velocity right) => left.quant / right.quant;

    public static Power operator *(Velocity velocity, Force force) => Power.From(in force, in velocity);
    public static Length operator *(Velocity left, Time right) => Length.From(in left, in right);
}