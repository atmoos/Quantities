using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Unit.Si;

namespace Quantities.Quantities;

public readonly struct Time : ITime, IEquatable<Time>, IFormattable
{
    private readonly Quant quant;
    private Time(in Quant quant) => this.quant = quant;
    public Time ToSeconds() => To<UnitPrefix, Second>();
    public Time To<TPrefix, TUnit>()
        where TPrefix : IPrefix, IScaleDown
        where TUnit : ISiUnit, ITime
    {
        return new(Build<Si<TPrefix, TUnit>>.With(in this.quant));
    }
    public Time To<TUnit>()
    where TUnit : ISiAcceptedUnit, ITime
    {
        return new(Build<Other<TUnit>>.With(in this.quant));
    }
    public TimeSpan ToTimeSpan() => TimeSpan.FromSeconds(ToSeconds());

    public static Time Seconds(in Double value) => Si<UnitPrefix, Second>(in value);
    public static Time Si<TPrefix, TUnit>(in Double value)
    where TPrefix : IPrefix, IScaleDown
    where TUnit : ISiUnit, ITime
    {
        return new(Build<Si<TPrefix, TUnit>>.With(in value));
    }
    public static Time In<TUnit>(in Double value)
        where TUnit : ISiAcceptedUnit, ITime
    {
        return new(Build<Other<TUnit>>.With(in value));
    }


    public Boolean Equals(Time other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Time time && Equals(time);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public static implicit operator Time(TimeSpan span) => Seconds(span.TotalSeconds);
    public static Boolean operator ==(Time left, Time right) => left.Equals(right);
    public static Boolean operator !=(Time left, Time right) => !left.Equals(right);
    public static implicit operator Double(Time time) => time.quant.Value;
    public static Time operator +(Time left, Time right) => new(left.quant + right.quant);
    public static Time operator -(Time left, Time right) => new(left.quant - right.quant);
    public static Time operator *(Double scalar, Time right) => new(scalar * right.quant);
    public static Time operator *(Time left, Double scalar) => new(scalar * left.quant);
    public static Time operator /(Time left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Time left, Time right) => left.quant / right.quant;
}