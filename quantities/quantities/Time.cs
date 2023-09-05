using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Time : IQuantity<Time>, ITime
    , IFactory<IDefaultFactory<Time, ITime>, Linear<To, Time, ITime>, Linear<Create, Time, ITime>>
    , IMultiplyOperators<Time, Power, Energy>
    , IMultiplyOperators<Time, Velocity, Length>
    , IMultiplyOperators<Time, DataRate, Data>
{
    private static readonly IRoot root = new SiRoot<Second>();
    private readonly Quantity time;
    internal Quantity Value => this.time;
    Quantity IQuantity<Time>.Value => this.time;
    public Linear<To, Time, ITime> To => new(new To(in this.time));
    private Time(in Quantity value) => this.time = value;
    public static Linear<Create, Time, ITime> Of(in Double value) => new(new Create(in value));
    static Time IFactory<Time>.Create(in Quantity value) => new(in value);
    internal static Time From(in Energy energy, in Power power)
    {
        // ToDo: Extract the time component!
        return new(MetricPrefix.ScaleThree(energy.Value.SiDivide(power.Value), root));
    }

    public Boolean Equals(Time other) => this.time.Equals(other.time);
    public override Boolean Equals(Object? obj) => obj is Time time && Equals(time);
    public override Int32 GetHashCode() => this.time.GetHashCode();
    public override String ToString() => this.time.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.time.ToString(format, provider);

    public static Boolean operator ==(Time left, Time right) => left.Equals(right);
    public static Boolean operator !=(Time left, Time right) => !left.Equals(right);
    public static implicit operator Double(Time time) => time.time;
    public static Time operator +(Time left, Time right) => new(left.time + right.time);
    public static Time operator -(Time left, Time right) => new(left.time - right.time);
    public static Time operator *(Double scalar, Time right) => new(scalar * right.time);
    public static Time operator *(Time left, Double scalar) => new(scalar * left.time);
    public static Time operator /(Time left, Double scalar) => new(left.time / scalar);
    public static Double operator /(Time left, Time right) => left.time / right.time;

    public static Energy operator *(Time left, Power right) => Energy.From(in right, in left);
    public static Length operator *(Time left, Velocity right) => Length.From(in right, in left);
    public static Data operator *(Time left, DataRate right) => Data.From(in left, in right);
}