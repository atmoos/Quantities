using System.Numerics;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Area : IQuantity<Area>, IArea
    , IPowerOf<Area, IArea, ILength, Two>
    , IMultiplyOperators<Area, Length, Volume>
    , IDivisionOperators<Area, Length, Length>
    , IMultiplyOperators<Area, Pressure, Force>
{
    private readonly Quantity area;
    internal Quantity Value => this.area;
    Quantity IQuantity<Area>.Value => this.area;
    private Area(in Quantity value) => this.area = value;
    public Area To<TLength>(in Power<TLength, Two> other)
        where TLength : ILength, IUnit => new(other.Transform(in this.area));
    public readonly Area To<TArea>(in Scalar<TArea> other)
        where TArea : IArea, IPowerOf<ILength>, IUnit => new(other.Transform(in this.area, static f => ref f.AliasOf<TArea, ILength>()));
    public static Area Of<TLength>(in Double value, in Power<TLength, Two> measure)
        where TLength : ILength, IUnit => new(measure.Create(in value));
    public static Area Of<TArea>(in Double value, in Scalar<TArea> measure)
        where TArea : IArea, IPowerOf<ILength>, IUnit => new(measure.Create(in value, static f => ref f.AliasOf<TArea, ILength>()));
    static Area IFactory<Area>.Create(in Quantity value) => new(in value);
    internal static Area From(in Length left, in Length right) => new(left.Value * right.Value);
    internal static Area From(in Volume volume, in Length length) => new(volume.Value / length.Value);
    public Boolean Equals(Area other) => this.area.Equals(other.area);
    public override Boolean Equals(Object? obj) => obj is Area Area && Equals(Area);
    public override Int32 GetHashCode() => this.area.GetHashCode();
    public override String ToString() => this.area.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.area.ToString(format, provider);

    public static implicit operator Double(Area area) => area.area;

    public static Volume operator *(Area area, Length length) => Volume.Times(in area, in length);
    public static Length operator /(Area left, Length right) => Length.From(in left, in right);

    public static Force operator *(Area area, Pressure pressure) => Force.From(in pressure, in area);
}
