﻿using System.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Area : IQuantity<Area>, IArea
    , ISquare<Area, IArea, ILength>
    , IMultiplyOperators<Area, Length, Volume>
    , IDivisionOperators<Area, Length, Length>
{
    private readonly Quantity area;
    internal Quantity Value => this.area;
    Quantity IQuantity<Area>.Value => this.area;
    private Area(in Quantity value) => this.area = value;
    public Area To<TLength>(in Square<TLength> other)
        where TLength : ILength, IUnit => new(other.Transform(in this.area));
    public Area To<TArea>(in Scalar<TArea> other)
        where TArea : IArea, IPowerOf<ILength>, IUnit => new(other.Transform(in this.area, f => f.PowerOf<TArea, ILength>()));
    public static Area Of<TLength>(in Double value, in Square<TLength> measure)
        where TLength : ILength, IUnit => new(measure.Create(in value));
    public static Area Of<TArea>(in Double value, in Scalar<TArea> measure)
        where TArea : IArea, IPowerOf<ILength>, IUnit => new(measure.Create(in value, f => f.PowerOf<TArea, ILength>()));
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
    public static Boolean operator >(Area left, Area right) => left.area > right.area;
    public static Boolean operator >=(Area left, Area right) => left.area >= right.area;
    public static Boolean operator <(Area left, Area right) => left.area < right.area;
    public static Boolean operator <=(Area left, Area right) => left.area <= right.area;
    public static Area operator +(Area left, Area right) => new(left.area + right.area);
    public static Area operator -(Area left, Area right) => new(left.area - right.area);
    public static Area operator *(Double scalar, Area right) => new(scalar * right.area);
    public static Volume operator *(Area area, Length length) => Volume.Times(in area, in length);
    public static Area operator *(Area left, Double scalar) => new(scalar * left.area);
    public static Length operator /(Area left, Length right) => Length.From(in left, in right);
    public static Area operator /(Area left, Double scalar) => new(left.area / scalar);
    public static Double operator /(Area left, Area right) => left.area.Ratio(in right.area);
}
