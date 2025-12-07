using System.Numerics;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Acceleration : IQuantity<Acceleration>, IAcceleration
    , IQuotient<Acceleration, IAcceleration, ILength, ITime, Two>
    , IMultiplyOperators<Acceleration, Time, Velocity>
{
    private readonly Quantity acceleration;
    internal Quantity Value => this.acceleration;
    Quantity IQuantity<Acceleration>.Value => this.acceleration;
    internal Acceleration(in Quantity value) => this.acceleration = value;
    public Acceleration To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IAcceleration, IUnit => new(other.Transform(in this.acceleration));
    public Acceleration To<TNominator, TDenominator>(in Quotient<TNominator, Power<TDenominator, Two>> other)
        where TNominator : ILength, IUnit
        where TDenominator : ITime, IUnit => new(other.Transform(in this.acceleration));
    public static Acceleration Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IAcceleration, IUnit => new(measure.Create(in value));
    public static Acceleration Of<TLength, TTime>(in Double value, in Quotient<TLength, Power<TTime, Two>> measure)
        where TLength : IUnit, ILength where TTime : IUnit, ITime => new(measure.Create(in value));
    static Acceleration IFactory<Acceleration>.Create(in Quantity value) => new(in value);
    internal static Acceleration From(in Velocity velocity, in Time time) => new(velocity.Value / time.Value);
    public Boolean Equals(Acceleration other) => this.acceleration.Equals(other.acceleration);
    public override Boolean Equals(Object? obj) => obj is Acceleration acceleration && Equals(acceleration);
    public override Int32 GetHashCode() => this.acceleration.GetHashCode();
    public override String ToString() => this.acceleration.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.acceleration.ToString(format, provider);

    public static Velocity operator *(Acceleration acceleration, Time time) => Velocity.From(in acceleration, in time);
}
