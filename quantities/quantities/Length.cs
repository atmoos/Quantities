using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Transformations;
using Quantities.Prefixes;
using Quantities.Unit.Imperial;
using Quantities.Unit.Si;

namespace Quantities.Quantities;

public readonly struct Length : ILength, IEquatable<Length>, IFormattable
{
    private static readonly ICreate<Quant> linear = new ToLinear();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Length(in Quant quant) => this.quant = quant;
    public Length To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiBaseUnit, ILength
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public Length ToImperial<TUnit>()
    where TUnit : IImperial, ILength
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public static Length Si<TUnit>(in Double value)
        where TUnit : ISiBaseUnit, ILength
    {
        return new(value.As<Si<TUnit>>());
    }
    public static Length Si<TPrefix, TUnit>(in Double value)
    where TPrefix : IPrefix
    where TUnit : ISiBaseUnit, ILength
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }
    public static Length Imperial<TUnit>(in Double value)
    where TUnit : IImperial, ILength
    {
        return new(value.As<Other<TUnit>>());
    }
    internal static Length From(in Area area, in Length length)
    {
        var pseudoArea = area.Quant.Transform(in linear);
        return new(pseudoArea.PseudoDivide(length.Quant));
    }
    internal static Length From(in Volume volume, in Area area)
    {
        var pseudoArea = area.Quant.Transform(in linear);
        var pseudoVolume = volume.Quant.Transform(in linear);
        return new(pseudoVolume.PseudoDivide(in pseudoArea));
    }

    public Boolean Equals(Length other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Length length && Equals(length);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public static Boolean operator ==(Length left, Length right) => left.Equals(right);
    public static Boolean operator !=(Length left, Length right) => !left.Equals(right);
    public static implicit operator Double(Length length) => length.quant.Value;
    public static Length operator +(Length left, Length right) => new(left.quant + right.quant);
    public static Length operator -(Length left, Length right) => new(left.quant - right.quant);
    public static Area operator *(Length left, Length right) => Area.From(in left, in right);
    public static Volume operator *(Length length, Area area) => Volume.Times(length, area);
    public static Length operator *(Double scalar, Length right) => new(scalar * right.quant);
    public static Length operator *(Length left, Double scalar) => new(scalar * left.quant);
    public static Length operator /(Length left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Length left, Length right) => left.quant / right.quant;
}