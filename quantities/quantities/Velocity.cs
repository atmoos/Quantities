using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Other;
using Quantities.Measures.Si;
using Quantities.Prefixes;
using Quantities.Quantities.Builders;
using Quantities.Unit.Imperial;
using Quantities.Unit.Si;

namespace Quantities.Quantities;

public readonly struct Velocity : IVelocity, IEquatable<Velocity>, IFormattable
{
    private readonly Quant quant;
    internal Velocity(in Quant quant) => this.quant = quant;
    public IBuilder<Velocity> To<TUnit>() where TUnit : ISiUnit, ILength => To<UnitPrefix, TUnit>();
    public IBuilder<Velocity> To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiUnit, ILength
    {
        return new SiTo<Velocity, Si<TPrefix, TUnit>>(in this.quant);
    }
    public IBuilder<Velocity> ToImperial<TUnit>()
    where TUnit : IImperial, ILength
    {
        return new OtherTo<Velocity, Other<TUnit>>(in this.quant);
    }
    public static IBuilder<Velocity> Si<TUnit>(in Double value)
        where TUnit : ISiUnit, ILength => Si<UnitPrefix, TUnit>(in value);
    public static IBuilder<Velocity> Si<TPrefix, TUnit>(in Double value)
    where TPrefix : IPrefix
    where TUnit : ISiUnit, ILength
    {
        return new SiBuilder<Velocity, Si<TPrefix, TUnit>>(in value);
    }
    public static IBuilder<Velocity> Imperial<TUnit>(in Double value)
    where TUnit : IImperial, ILength
    {
        return new OtherBuilder<Velocity, Other<TUnit>>(in value);
    }

    public Boolean Equals(Velocity other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Velocity velocity && this.Equals(velocity);

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
}