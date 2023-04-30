using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Builders;
using Quantities.Quantities.Roots;
using Quantities.Units.Imperial;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Velocity : IQuantity<Velocity>, IVelocity<Length, Time>
    , IMultiplyOperators<Velocity, Force, Power>
    , IMultiplyOperators<Velocity, Time, Length>
{
    private static readonly IRoot root = new FractionalRoot<Metre, Second>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    internal Velocity(in Quant quant) => this.quant = quant;
    public IBuilder<Velocity> To<TUnit>() where TUnit : ISiUnit, ILength
    {
        return new Transform<Velocity, Si<TUnit>>(in this.quant);
    }
    public IBuilder<Velocity> To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, ILength
    {
        return new Transform<Velocity, Si<TPrefix, TUnit>>(in this.quant);
    }
    public IBuilder<Velocity> ToImperial<TUnit>()
    where TUnit : IImperial, ILength
    {
        return new Transform<Velocity, Imperial<TUnit>>(in this.quant);
    }
    public static IBuilder<Velocity> Si<TUnit>(in Double value)
        where TUnit : ISiUnit, ILength => new Builder<Velocity, Si<TUnit>>(in value);
    public static IBuilder<Velocity> Si<TPrefix, TUnit>(in Double value)
    where TPrefix : IMetricPrefix
    where TUnit : ISiUnit, ILength
    {
        return new Builder<Velocity, Si<TPrefix, TUnit>>(in value);
    }
    public static IBuilder<Velocity> Imperial<TUnit>(in Double value)
    where TUnit : IImperial, ILength
    {
        return new Builder<Velocity, Imperial<TUnit>>(in value);
    }
    internal static Velocity From(in Power power, in Force force)
    {
        return new(MetricPrefix.Scale(power.Quant.SiDivide(force.Quant), root));
    }
    internal static Velocity From(in Length length, in Time time) => new(length.Quant.Divide(time.Quant));

    public Boolean Equals(Velocity other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Velocity velocity && Equals(velocity);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public static Boolean operator ==(Velocity left, Velocity right) => left.Equals(right);
    public static Boolean operator !=(Velocity left, Velocity right) => !left.Equals(right);
    public static implicit operator Double(Velocity velocity) => velocity.quant.Value;
    public static Velocity operator +(Velocity left, Velocity right) => new(left.quant + right.quant);
    public static Velocity operator -(Velocity left, Velocity right) => new(left.quant - right.quant);
    public static Velocity operator *(Double scalar, Velocity right) => new(scalar * right.quant);
    public static Velocity operator *(Velocity left, Double scalar) => new(scalar * left.quant);
    public static Velocity operator /(Velocity left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Velocity left, Velocity right) => left.quant / right.quant;

    public static Power operator *(Velocity velocity, Force force) => Power.From(in force, in velocity);
    public static Length operator *(Velocity left, Time right) => Length.From(in left, in right);
}