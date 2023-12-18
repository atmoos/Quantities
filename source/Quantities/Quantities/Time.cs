using System.Numerics;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Time : IQuantity<Time>, ITime
    , IScalar<Time, ITime>
    , IMultiplyOperators<Time, Power, Energy>
    , IMultiplyOperators<Time, Velocity, Length>
    , IMultiplyOperators<Time, DataRate, Data>
{
    private readonly Quantity time;
    internal Quantity Value => this.time;
    Quantity IQuantity<Time>.Value => this.time;
    private Time(in Quantity value) => this.time = value;
    public Time To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : ITime, IUnit => new(other.Transform(in this.time));
    public static Time Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : ITime, IUnit => new(measure.Create(in value));
    static Time IFactory<Time>.Create(in Quantity value) => new(in value);
    internal static Time From(in Energy energy, in Power power) => new(energy.Value / power.Value);
    public Boolean Equals(Time other) => this.time.Equals(other.time);
    public override Boolean Equals(Object? obj) => obj is Time time && Equals(time);
    public override Int32 GetHashCode() => this.time.GetHashCode();
    public override String ToString() => this.time.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.time.ToString(format, provider);

    public static implicit operator Double(Time time) => time.time;
    public static Boolean operator ==(Time left, Time right) => left.Equals(right);
    public static Boolean operator !=(Time left, Time right) => !left.Equals(right);
    public static Time operator +(Time left, Time right) => new(left.time + right.time);
    public static Time operator -(Time left, Time right) => new(left.time - right.time);
    public static Time operator *(Double scalar, Time right) => new(scalar * right.time);
    public static Time operator *(Time left, Double scalar) => new(scalar * left.time);
    public static Time operator /(Time left, Double scalar) => new(left.time / scalar);
    public static Double operator /(Time left, Time right) => left.time.Ratio(in right.time);

    public static Energy operator *(Time time, Power power) => Energy.Times(in power, in time);
    public static Length operator *(Time time, Velocity velocity) => Length.From(in velocity, in time);
    public static Data operator *(Time time, DataRate rate) => Data.From(in time, in rate);
}
