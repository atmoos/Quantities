using System.Numerics;
using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Area : IQuantity<Area>, IArea
    , ISquare<Area, IArea, ILength>
    , IMultiplyOperators<Area, Length, Volume>
    , IDivisionOperators<Area, Length, Length>
{
    private readonly Quantity area;
    internal Quantity Value => this.area;
    Quantity IQuantity<Area>.Value => this.area;
    private Area(in Quantity value) => this.area = value;
    public Area To<TLength>(in Square<TLength> square)
        where TLength : ILength, IUnit => new(square.Transform(in this.area));
    public Area To<TAlias>(in Alias<TAlias, ILength> alias)
        where TAlias : IArea, IAlias<ILength>, IUnit => new(alias.Transform(in this.area));
    public static Area Of<TLength>(in Double value, in Square<TLength> square)
        where TLength : ILength, IUnit => new(square.Create(in value));
    public static Area Of<TAlias>(in Double value, in Alias<TAlias, ILength> alias)
        where TAlias : IArea, IAlias<ILength>, IUnit => new(alias.Create(in value));
    static Area IFactory<Area>.Create(in Quantity value) => new(in value);
    internal static Area From(in Length left, in Length right) => new(left.Value * right.Value);
    internal static Area From(in Volume volume, in Length length) => new(volume.Value / length.Value);
    public Boolean Equals(Area other) => this.area.Equals(other.area);
    public override Boolean Equals(Object? obj) => obj is Area Area && Equals(Area);
    public override Int32 GetHashCode() => this.area.GetHashCode();
    public override String ToString() => this.area.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.area.ToString(format, provider);

    public static implicit operator Double(Area area) => area.area;
    public static Boolean operator ==(Area left, Area right) => left.Equals(right);
    public static Boolean operator !=(Area left, Area right) => !left.Equals(right);
    public static Area operator +(Area left, Area right) => new(left.area + right.area);
    public static Area operator -(Area left, Area right) => new(left.area - right.area);
    public static Area operator *(Double scalar, Area right) => new(scalar * right.area);
    public static Volume operator *(Area area, Length length) => Volume.Times(in area, in length);
    public static Area operator *(Area left, Double scalar) => new(scalar * left.area);
    public static Length operator /(Area left, Length right) => Length.From(in left, in right);
    public static Area operator /(Area left, Double scalar) => new(left.area / scalar);
    public static Double operator /(Area left, Area right) => left.area.Divide(in right.area);
}
