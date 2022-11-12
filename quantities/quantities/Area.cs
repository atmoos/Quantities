using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Unit;
using Quantities.Unit.Imperial;
using Quantities.Unit.Si;

namespace Quantities.Quantities;

public readonly struct Area : IArea<Length>, IEquatable<Area>, IFormattable
{
    private readonly Quant quant;
    private Area(in Quant quant) => this.quant = quant;
    public Area ToSquare<TUnit>()
        where TUnit : ISiUnit, ILength
    {
        return ToSquare<UnitPrefix, TUnit>();
    }
    public Area ToSquare<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiUnit, ILength
    {
        return new(Build<Power<Square, Si<TPrefix, TUnit>>>.With(in this.quant));
    }
    public Area ToImperial<TUnit>()
    where TUnit : IImperial, IArea<ILength>, IUnitInject<ILength>
    {
        return new(Build<Alias<TUnit, ILength>>.With(in this.quant));
    }
    public Area ToSquareImperial<TLength>()
    where TLength : IImperial, ILength
    {
        return new(Build<Power<Square, Other<TLength>>>.With(in this.quant));
    }
    public static Area Square<TUnit>(in Double value)
        where TUnit : ISiUnit, ILength
    {
        return Square<UnitPrefix, TUnit>(in value);
    }
    public static Area Square<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiUnit, ILength
    {
        return new(Build<Power<Square, Si<TPrefix, TUnit>>>.With(in value));
    }
    public static Area SquareImperial<TLength>(Double value)
        where TLength : IImperial, ILength
    {
        return new(Build<Power<Square, Other<TLength>>>.With(in value));
    }
    public static Area Imperial<TArea>(Double value)
        where TArea : IImperial, IArea<ILength>, IUnitInject<ILength>
    {
        return new(Build<Alias<TArea, ILength>>.With(in value));
    }
    internal static Area From(in Quant left, in Quant right)
    {
        return new(left * right);
    }

    public Boolean Equals(Area other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Area Area && Equals(Area);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public static Boolean operator ==(Area left, Area right) => left.Equals(right);
    public static Boolean operator !=(Area left, Area right) => !left.Equals(right);
    public static implicit operator Double(Area Area) => Area.quant.Value;
    public static Area operator +(Area left, Area right) => new(left.quant + right.quant);
    public static Area operator -(Area left, Area right) => new(left.quant - right.quant);
    public static Area operator *(Double scalar, Area right) => new(scalar * right.quant);
    public static Area operator *(Area left, Double scalar) => new(scalar * left.quant);
    public static Length operator /(Area left, Length right) => Length.From(in left.quant, in right);
    public static Area operator /(Area left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Area left, Area right) => left.quant / right.quant;
}