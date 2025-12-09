using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Time : IQuantity<Time>, ITime
    , IScalar<Time, ITime>
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
    internal static Time From(in Length length, in Velocity velocity) => new(length.Value / velocity.Value);
    internal static Time From(in Velocity velocity, in Acceleration acceleration) => new(velocity.Value / acceleration.Value);
    internal static Time From(in Energy energy, in Power power) => new(energy.Value / power.Value);
    internal static Time From(Double numerator, in Frequency denominator) => new(numerator / denominator.Value);
    public Boolean Equals(Time other) => this.time.Equals(other.time);
    public override Boolean Equals(Object? obj) => obj is Time time && Equals(time);
    public override Int32 GetHashCode() => this.time.GetHashCode();
    public override String ToString() => this.time.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.time.ToString(format, provider);
}
