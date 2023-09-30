using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;

namespace Quantities;

public readonly struct Velocity : IQuantity<Velocity>, IVelocity
    , IFactory<IQuotientFactory<IVelocity, ILength, ITime>, Quotient<To, Velocity, IVelocity, ILength, ITime>, Quotient<Create, Velocity, IVelocity, ILength, ITime>>
    , IMultiplyOperators<Velocity, Force, Power>
    , IMultiplyOperators<Velocity, Time, Length>
{
    private readonly Quantity velocity;
    internal Quantity Value => this.velocity;
    Quantity IQuantity<Velocity>.Value => this.velocity;
    public Quotient<To, Velocity, IVelocity, ILength, ITime> To => new(new To(in this.velocity));
    internal Velocity(in Quantity value) => this.velocity = value;
    public static Quotient<Create, Velocity, IVelocity, ILength, ITime> Of(in Double value) => new(new Create(in value));
    static Velocity IFactory<Velocity>.Create(in Quantity value) => new(in value);
    internal static Velocity From(in Power power, in Force force) => new(power.Value / force.Value);
    internal static Velocity From(in Length length, in Time time) => new(length.Value / time.Value);
    public Boolean Equals(Velocity other) => this.velocity.Equals(other.velocity);
    public override Boolean Equals(Object? obj) => obj is Velocity velocity && Equals(velocity);
    public override Int32 GetHashCode() => this.velocity.GetHashCode();
    public override String ToString() => this.velocity.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.velocity.ToString(format, provider);

    public static implicit operator Double(Velocity velocity) => velocity.velocity;
    public static Boolean operator ==(Velocity left, Velocity right) => left.Equals(right);
    public static Boolean operator !=(Velocity left, Velocity right) => !left.Equals(right);
    public static Velocity operator +(Velocity left, Velocity right) => new(left.velocity + right.velocity);
    public static Velocity operator -(Velocity left, Velocity right) => new(left.velocity - right.velocity);
    public static Velocity operator *(Double scalar, Velocity right) => new(scalar * right.velocity);
    public static Velocity operator *(Velocity left, Double scalar) => new(scalar * left.velocity);
    public static Velocity operator /(Velocity left, Double scalar) => new(left.velocity / scalar);
    public static Double operator /(Velocity left, Velocity right) => left.velocity.Divide(in right.velocity);

    public static Power operator *(Velocity velocity, Force force) => Power.From(in force, in velocity);
    public static Length operator *(Velocity left, Time right) => Length.From(in left, in right);
}
