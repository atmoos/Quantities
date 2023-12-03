using System.Numerics;
using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Acceleration : IQuantity<Acceleration>, IAcceleration
    , IScalar<Acceleration, IAcceleration>
    , IMultiplyOperators<Acceleration, Time, Velocity>
{
    private readonly Quantity acceleration;
    internal Quantity Value => this.acceleration;
    Quantity IQuantity<Acceleration>.Value => this.acceleration;
    private Acceleration(in Quantity value) => this.acceleration = value;
    public Acceleration To<TAcceleration>(in Scalar<TAcceleration> other)
        where TAcceleration : IAcceleration, IUnit => new(other.Transform(in this.acceleration));
    public Acceleration To<TNominator, TDenominator>(in Quotient<TNominator, Square<TDenominator>> other)
        where TNominator : ILength, IUnit
        where TDenominator : ITime, IUnit => new(other.Transform(in this.acceleration));
    public static Acceleration Of<TAcceleration>(in Double value, in Scalar<TAcceleration> measure)
        where TAcceleration : IAcceleration, IUnit => new(measure.Create(in value));
    public static Acceleration Of<TNominator, TDenominator>(in Double value, in Quotient<TNominator, Square<TDenominator>> measure)
        where TNominator : ILength, IUnit
        where TDenominator : ITime, IUnit => new(measure.Create(in value));
    static Acceleration IFactory<Acceleration>.Create(in Quantity value) => new(in value);
    internal static Acceleration From(in Velocity velocity, in Time time) => new(velocity.Value / time.Value);
    public Boolean Equals(Acceleration other) => this.acceleration.Equals(other.acceleration);
    public override Boolean Equals(Object? obj) => obj is Acceleration acceleration && Equals(acceleration);
    public override Int32 GetHashCode() => this.acceleration.GetHashCode();
    public override String ToString() => this.acceleration.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.acceleration.ToString(format, provider);

    public static implicit operator Double(Acceleration acceleration) => acceleration.acceleration;
    public static Boolean operator ==(Acceleration left, Acceleration right) => left.Equals(right);
    public static Boolean operator !=(Acceleration left, Acceleration right) => !left.Equals(right);
    public static Acceleration operator +(Acceleration left, Acceleration right) => new(left.acceleration + right.acceleration);
    public static Acceleration operator -(Acceleration left, Acceleration right) => new(left.acceleration - right.acceleration);
    public static Acceleration operator *(Double scalar, Acceleration right) => new(scalar * right.acceleration);
    public static Acceleration operator *(Acceleration left, Double scalar) => new(scalar * left.acceleration);
    public static Acceleration operator /(Acceleration left, Double scalar) => new(left.acceleration / scalar);
    public static Double operator /(Acceleration left, Acceleration right) => left.acceleration.Ratio(in right.acceleration);

    public static Velocity operator *(Acceleration acceleration, Time time) => Velocity.From(in acceleration, in time);
}
