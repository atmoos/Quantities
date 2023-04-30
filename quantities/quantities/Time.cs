using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Time : IQuantity<Time>, ITime
    , IMultiplyOperators<Time, Power, Energy>
    , IMultiplyOperators<Time, Velocity, Length>
    , IMultiplyOperators<Time, DataRate, Data>
// ToDo: Can't apply ISi, or INoSystem interfaces...
{
    private static readonly IRoot root = new UnitRoot<Second>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Time(in Quant quant) => this.quant = quant;
    public Time ToSeconds() => new(this.quant.As<Si<Second>>());
    public Time To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix, IScaleDown
        where TUnit : ISiUnit, ITime
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public Time To<TUnit>()
        where TUnit : IMetricUnit, ITime
    {
        return new(this.quant.As<Metric<TUnit>>());
    }
    public TimeSpan ToTimeSpan() => TimeSpan.FromSeconds(ToSeconds());
    public static Time Seconds(in Double value) => new(value.As<Si<Second>>());
    public static Time Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix, IScaleDown
        where TUnit : ISiUnit, ITime
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }
    public static Time In<TUnit>(in Double value)
        where TUnit : IMetricUnit, ITime
    {
        return new(value.As<Metric<TUnit>>());
    }

    internal static Time From(in Energy energy, in Power power)
    {
        // ToDo: Extract the time component!
        return new(MetricPrefix.ScaleThree(energy.Quant.SiDivide(power.Quant), root));
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
    public static Energy operator *(Time left, Power right) => Energy.From(in right, in left);

    public static Length operator *(Time left, Velocity right) => Length.From(in right, in left);
    public static Data operator *(Time left, DataRate right) => Data.From(in left, in right);
}