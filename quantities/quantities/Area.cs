using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Measures.Transformations;
using Quantities.Quantities.Creation;

namespace Quantities.Quantities;

public readonly struct Area : IQuantity<Area>, IArea
    , IFactory<Area>
    , IFactory<ISquareFactory<Area, IArea, ILength>, SquareFactory<Area, To, IArea, ILength>, SquareFactory<Area, Create, IArea, ILength>>
    , IMultiplyOperators<Area, Length, Volume>
    , IDivisionOperators<Area, Length, Length>
{
    private static readonly Measures.IInject<Quant> square = new RaiseTo<Square>();
    private static readonly Measures.IInject<Quant> linear = new ToLinear();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<Area>.Value => this.quant;
    public SquareFactory<Area, To, IArea, ILength> To => new(new To(in this.quant));
    private Area(in Quant quant) => this.quant = quant;
    public static SquareFactory<Area, Create, IArea, ILength> Of(in Double value) => new(new Create(in value));
    static Area IFactory<Area>.Create(in Quant quant) => new(in quant);
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