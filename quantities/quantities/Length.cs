using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Si;
using Quantities.Prefixes;
using Quantities.Unit.Imperial;
using Quantities.Unit.Si;

namespace Quantities.Quantities;

public readonly struct Length : ILength, IEquatable<Length>, IFormattable
{
    private readonly Quant quant;
    private Length(in Quant quant) => this.quant = quant;
    public Length To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiUnit, ILength
    {
        return new(Build<Si<TPrefix, TUnit>>.With(in this.quant));
    }
    public Length ToImperial<TUnit>()
    where TUnit : IImperial, ILength
    {
        return new();
    }
    public static Length Si<TUnit>(in Double value)
        where TUnit : ISiUnit, ILength
    {
        return new(Build<Si<UnitPrefix, TUnit>>.With(in value));
    }
    public static Length Si<TPrefix, TUnit>(in Double value)
    where TPrefix : IPrefix
    where TUnit : ISiUnit, ILength
    {
        return new(Build<Si<TPrefix, TUnit>>.With(in value));
    }

    public static Length Imperial<TUnit>(in Double value)
    where TUnit : IImperial, ILength
    {
        return new();
    }

    public Boolean Equals(Length other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Length length && this.Equals(length);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public static Boolean operator ==(Length left, Length right) => left.Equals(right);
    public static Boolean operator !=(Length left, Length right) => !left.Equals(right);
    public static implicit operator Double(Length length) => length.quant.Value;
    public static Length operator +(Length left, Length right) => new(left.quant + right.quant);
    public static Length operator -(Length left, Length right) => new(left.quant - right.quant);
    public static Length operator *(Double scalar, Length right) => new(scalar * right.quant);
    public static Length operator *(Length left, Double scalar) => new(scalar * left.quant);
    public static Length operator /(Length left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Length left, Length right) => left.quant / right.quant;
}