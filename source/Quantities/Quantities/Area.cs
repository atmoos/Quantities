﻿using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;

namespace Quantities.Quantities;

public readonly struct Area : IQuantity<Area>, IArea
    , IFactory<IQuadraticFactory<Area, IArea, ILength>, Quadratic<To, Area, IArea, ILength>, Quadratic<Create, Area, IArea, ILength>>
    , IMultiplyOperators<Area, Length, Volume>
    , IDivisionOperators<Area, Length, Length>
{
    private readonly Quantity area;
    internal Quantity Value => this.area;
    Quantity IQuantity<Area>.Value => this.area;
    public Quadratic<To, Area, IArea, ILength> To => new(new To(in this.area));
    private Area(in Quantity value) => this.area = value;
    public static Quadratic<Create, Area, IArea, ILength> Of(in Double value) => new(new Create(in value));
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