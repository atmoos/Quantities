using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Builders;
using Quantities.Units.Imperial;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct Velocity : IQuantity<Velocity>, IVelocity<Length, Time>
    , IMultiplyOperators<Velocity, Force, Power>
{
    private static readonly Creator create = new();
    private readonly Quant quant;
    internal Velocity(in Quant quant) => this.quant = quant;
    public IBuilder<Velocity> To<TUnit>() where TUnit : ISiBaseUnit, ILength
    {
        return new Transform<Velocity, Si<TUnit>>(in this.quant);
    }

    public IBuilder<Velocity> To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiBaseUnit, ILength
    {
        return new Transform<Velocity, Si<TPrefix, TUnit>>(in this.quant);
    }
    public IBuilder<Velocity> ToImperial<TUnit>()
    where TUnit : IImperial, ILength
    {
        return new Transform<Velocity, Other<TUnit>>(in this.quant);
    }
    public static IBuilder<Velocity> Si<TUnit>(in Double value)
        where TUnit : ISiBaseUnit, ILength => new Builder<Velocity, Si<TUnit>>(in value);
    public static IBuilder<Velocity> Si<TPrefix, TUnit>(in Double value)
    where TPrefix : IPrefix
    where TUnit : ISiBaseUnit, ILength
    {
        return new Builder<Velocity, Si<TPrefix, TUnit>>(in value);
    }
    public static IBuilder<Velocity> Imperial<TUnit>(in Double value)
    where TUnit : IImperial, ILength
    {
        return new Builder<Velocity, Other<TUnit>>(in value);
    }

    internal static Velocity From(in Power power, in Force force)
    {
        Double siPower = power.To<Watt>();
        Double siForce = force.To<Newton>();
        return new(SiPrefix.Scale(siPower / siForce, create));
    }

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

    private sealed class Creator : IPrefixInject<Quant>
    {
        public Quant Identity(in Double value) => Build<Divide<Si<Metre>, Si<Second>>>.With(in value);
        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix => Build<Divide<Si<TPrefix, Metre>, Si<Second>>>.With(in value);
    }
}