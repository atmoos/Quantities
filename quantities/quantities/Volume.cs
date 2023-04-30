using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Transformations;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Volume : IQuantity<Volume>, IVolume<Length>
{
    private static readonly ICreate<Quant> cube = new RaiseTo<Cube>();
    private static readonly ICreate<Quant> linear = new ToLinear();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Volume(in Quant quant) => this.quant = quant;
    public Volume ToCubic<TUnit>()
       where TUnit : ISiUnit, ILength
    {
        return new(this.quant.To<Cube, Si<TUnit>>());
    }
    public Volume ToCubic<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, ILength
    {
        return new(this.quant.To<Cube, Si<TPrefix, TUnit>>());
    }
    public Volume ToMetric<TVolume>()
    where TVolume : IMetricUnit, IVolume<ILength>, IInjectUnit<ILength>
    {
        return new(this.quant.As<Metric<TVolume>, Alias<TVolume, ILength>>());
    }
    public Volume ToMetric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IVolume<ILength>, IInjectUnit<ILength>
    {
        return new(this.quant.As<Metric<TPrefix, TUnit>, Alias<TPrefix, TUnit, ILength>>());
    }
    public Volume ToImperial<TUnit>()
        where TUnit : IImperial, IVolume<ILength>, IInjectUnit<ILength>
    {
        return new(this.quant.As<Imperial<TUnit>, Alias<TUnit, ILength>>());
    }
    public Volume ToCubicImperial<TLength>()
        where TLength : IImperial, ILength
    {
        return new(this.quant.To<Cube, Imperial<TLength>>());
    }
    public static Volume Cubic<TUnit>(in Double value)
        where TUnit : ISiUnit, ILength
    {
        return new(value.To<Cube, Si<TUnit>>());
    }
    public static Volume Cubic<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, ILength
    {
        return new(value.To<Cube, Si<TPrefix, TUnit>>());
    }
    public static Volume Metric<TVolume>(Double value)
        where TVolume : IMetricUnit, IVolume<ILength>, IInjectUnit<ILength>
    {
        return new(value.As<Metric<TVolume>, Alias<TVolume, ILength>>());
    }
    public static Volume Metric<TPrefix, TVolume>(Double value)
        where TPrefix : IMetricPrefix
        where TVolume : IMetricUnit, IVolume<ILength>, IInjectUnit<ILength>
    {
        return new(value.As<Metric<TPrefix, TVolume>, Alias<TPrefix, TVolume, ILength>>());
    }
    public static Volume Imperial<TVolume>(Double value)
        where TVolume : IImperial, IVolume<ILength>, IInjectUnit<ILength>
    {
        return new(value.As<Imperial<TVolume>, Alias<TVolume, ILength>>());
    }
    public static Volume CubicImperial<TImperialUnit>(Double value)
        where TImperialUnit : IImperial, ILength
    {
        return new(value.To<Cube, Imperial<TImperialUnit>>());
    }

    public static Area operator /(Volume volume, Length length)
    {
        return Area.From(in volume, in length);
    }
    public static Length operator /(Volume volume, Area area)
    {
        return Length.From(in volume, area);
    }

    public Boolean Equals(Volume other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Volume Volume && Equals(Volume);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    internal static Volume Times(in Length length, in Area area)
    {
        // ToDo: one transform could be enough...
        var pseudoArea = area.Quant.Transform(in linear);
        var pseudoVolume = length.Quant.PseudoMultiply(in pseudoArea);
        return new(pseudoVolume.Transform(in cube));
    }

    internal static Volume Times(in Area area, in Length length)
    {
        // ToDo: one transform could be enough...
        var pseudoArea = area.Quant.Transform(in linear);
        var pseudoVolume = pseudoArea.PseudoMultiply(length.Quant);
        return new(pseudoVolume.Transform(in cube));
    }

    public static Boolean operator ==(Volume left, Volume right) => left.Equals(right);
    public static Boolean operator !=(Volume left, Volume right) => !left.Equals(right);
    public static implicit operator Double(Volume Volume) => Volume.quant.Value;
    public static Volume operator +(Volume left, Volume right) => new(left.quant + right.quant);
    public static Volume operator -(Volume left, Volume right) => new(left.quant - right.quant);
    public static Volume operator *(Double scalar, Volume right) => new(scalar * right.quant);
    public static Volume operator *(Volume left, Double scalar) => new(scalar * left.quant);
    public static Volume operator /(Volume left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Volume left, Volume right) => left.quant / right.quant;

}
