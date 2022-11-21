using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Time : IQuantity<Time>, ITime
{
    private readonly Quant quant;
    private Time(in Quant quant) => this.quant = quant;
    public Time ToSeconds() => new(this.quant.As<Si<Second>>());

    public Time To<TPrefix, TUnit>()
        where TPrefix : IPrefix, IScaleDown
        where TUnit : ISiBaseUnit, ITime
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public Time To<TUnit>()
    where TUnit : ISiAcceptedUnit, ITime
    {
        return new(this.quant.As<SiAccepted<TUnit>>());
    }
    public TimeSpan ToTimeSpan() => TimeSpan.FromSeconds(ToSeconds());

    public static Time Seconds(in Double value) => new(value.As<Si<Second>>());
    public static Time Si<TPrefix, TUnit>(in Double value)
    where TPrefix : IPrefix, IScaleDown
    where TUnit : ISiBaseUnit, ITime
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }
    public static Time In<TUnit>(in Double value)
        where TUnit : ISiAcceptedUnit, ITime
    {
        return new(value.As<Other<TUnit>>());
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