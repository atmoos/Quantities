using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Measures.Transformations;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Volume : IQuantity<Volume>, IVolume<ILength>
    , IFactory<Volume>
    , IFactory<ICubicFactory<Volume, IVolume<ILength>, ILength>, CubicFactory<Volume, PowerFactory<Volume, CubicTo, ILength>, IVolume<ILength>, ILength>, CubicFactory<Volume, PowerFactory<Volume, CubicCreate, ILength>, IVolume<ILength>, ILength>>
{
    private static readonly ICreate<Quant> cube = new RaiseTo<Cube>();
    private static readonly ICreate<Quant> linear = new ToLinear();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    public CubicFactory<Volume, PowerFactory<Volume, CubicTo, ILength>, IVolume<ILength>, ILength> To => new(new PowerFactory<Volume, CubicTo, ILength>(new CubicTo(in this.quant)));
    private Volume(in Quant quant) => this.quant = quant;
    public static CubicFactory<Volume, PowerFactory<Volume, CubicCreate, ILength>, IVolume<ILength>, ILength> Of(in Double value) => new(new PowerFactory<Volume, CubicCreate, ILength>(new CubicCreate(in value)));
    static Volume IFactory<Volume>.Create(in Quant quant) => new(in quant);

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
    public static Area operator /(Volume volume, Length length) => Area.From(in volume, in length);
    public static Length operator /(Volume volume, Area area) => Length.From(in volume, area);

}
