using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Time : IQuantity<Time>, ITime
    , IFactory<ICompoundFactory<Time, ITime>, Linear<To, Time, ITime>, Linear<Create, Time, ITime>>
    , IMultiplyOperators<Time, Power, Energy>
    , IMultiplyOperators<Time, Velocity, Length>
    , IMultiplyOperators<Time, DataRate, Data>
{
    private static readonly IRoot root = new SiRoot<Second>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<Time>.Value => this.quant;
    public Linear<To, Time, ITime> To => new(new To(in this.quant));
    private Time(in Quant quant) => this.quant = quant;
    public static Linear<Create, Time, ITime> Of(in Double value) => new(new Create(in value));
    static Time IFactory<Time>.Create(in Quant quant) => new(in quant);
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