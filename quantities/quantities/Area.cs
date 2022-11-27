using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Transformations;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.Other;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Area : IQuantity<Area>, IArea<Length>
    , IMultiplyOperators<Area, Length, Volume>
    , IDivisionOperators<Area, Length, Length>
{
    private static readonly ICreate<Quant> square = new RaiseTo<Square>();
    private static readonly ICreate<Quant> linear = new ToLinear();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Area(in Quant quant) => this.quant = quant;
    public Area ToSquare<TUnit>()
        where TUnit : ISiBaseUnit, ILength
    {
        return new(this.quant.To<Square, Si<TUnit>>());
    }
    public Area ToSquare<TPrefix, TUnit>()
        where TPrefix : ISiPrefix
        where TUnit : ISiBaseUnit, ILength
    {
        return new(this.quant.To<Square, Si<TPrefix, TUnit>>());
    }
    public Area ToMetric<TArea>()
        where TArea : IMetricUnit, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(this.quant.As<Metric<TArea>, Alias<TArea, ILength>>());
    }
    public Area ToMetric<TPrefix, TArea>()
        where TPrefix : ISiPrefix
        where TArea : IMetricUnit, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(this.quant.As<Metric<TPrefix, TArea>, Alias<TPrefix, TArea, ILength>>());
    }
    public Area ToImperial<TArea>()
        where TArea : IImperial, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(this.quant.As<Other<TArea>, Alias<TArea, ILength>>());
    }
    public Area ToSquareImperial<TLength>()
        where TLength : IImperial, ILength
    {
        return new(this.quant.To<Square, Other<TLength>>());
    }
    public Area ToOther<TArea>()
        where TArea : IOther, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(this.quant.As<Other<TArea>, Alias<TArea, ILength>>());
    }
    public static Area Square<TUnit>(in Double value)
        where TUnit : ISiBaseUnit, ILength
    {
        return new(value.To<Square, Si<TUnit>>());
    }
    public static Area Square<TPrefix, TUnit>(in Double value)
        where TPrefix : ISiPrefix
        where TUnit : ISiBaseUnit, ILength
    {
        return new(value.To<Square, Si<TPrefix, TUnit>>());
    }
    public static Area Metric<TArea>(in Double value)
        where TArea : IMetricUnit, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(value.As<Metric<TArea>, Alias<TArea, ILength>>());
    }
    public static Area Metric<TPrefix, TArea>(in Double value)
        where TPrefix : ISiPrefix
        where TArea : IMetricUnit, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(value.As<Metric<TPrefix, TArea>, Alias<TPrefix, TArea, ILength>>());
    }
    public static Area SquareImperial<TLength>(Double value)
        where TLength : IImperial, ILength
    {
        return new(value.To<Square, Other<TLength>>());
    }
    public static Area Imperial<TArea>(Double value)
        where TArea : IImperial, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(value.As<Other<TArea>, Alias<TArea, ILength>>());
    }
    public static Area Other<TArea>(Double value)
        where TArea : IOther, IArea<ILength>, IInjectUnit<ILength>
    {
        return new(value.As<Other<TArea>, Alias<TArea, ILength>>());
    }
    internal static Area From(in Length left, in Length right)
    {
        var pseudoLength = left.Quant.PseudoMultiply(right.Quant);
        return new(pseudoLength.Transform(in square));
    }
    internal static Area From(in Volume volume, in Length length)
    {
        var pseudoVolume = volume.Quant.Transform(in linear);
        var pseudoArea = pseudoVolume.PseudoDivide(length.Quant);
        return new(pseudoArea.Transform(in square));
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
    public static Volume operator *(Area area, Length length) => Volume.Times(in area, in length);
    public static Area operator *(Area left, Double scalar) => new(scalar * left.quant);
    public static Length operator /(Area left, Length right) => Length.From(in left, in right);
    public static Area operator /(Area left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Area left, Area right) => left.quant / right.quant;
}