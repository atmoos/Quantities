﻿using System.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Length : IQuantity<Length>, ILength
    , IScalar<Length, ILength>
    , IMultiplyOperators<Length, Length, Area>
    , IMultiplyOperators<Length, Area, Volume>
    , IDivisionOperators<Length, Time, Velocity>
{
    private readonly Quantity length;
    internal Quantity Value => this.length;
    Quantity IQuantity<Length>.Value => this.length;
    private Length(in Quantity value) => this.length = value;
    public Length To<TLength>(in Scalar<TLength> other)
        where TLength : ILength, IUnit => new(other.Transform(in this.length));
    public static Length Of<TLength>(in Double value, in Scalar<TLength> measure)
        where TLength : ILength, IUnit => new(measure.Create(in value));
    static Length IFactory<Length>.Create(in Quantity value) => new(in value);
    internal static Length From(in Area area, in Length length) => new(area.Value / length.Value);
    internal static Length From(in Velocity velocity, in Time time) => new(velocity.Value * time.Value);
    internal static Length From(in Volume volume, in Area area) => new(volume.Value / area.Value);
    public Boolean Equals(Length other) => this.length.Equals(other.length);
    public override Boolean Equals(Object? obj) => obj is Length length && Equals(length);
    public override Int32 GetHashCode() => this.length.GetHashCode();
    public override String ToString() => this.length.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.length.ToString(format, provider);

    public static implicit operator Double(Length length) => length.length;
    public static Boolean operator ==(Length left, Length right) => left.Equals(right);
    public static Boolean operator !=(Length left, Length right) => !left.Equals(right);
    public static Boolean operator >(Length left, Length right) => left.length > right.length;
    public static Boolean operator >=(Length left, Length right) => left.length >= right.length;
    public static Boolean operator <(Length left, Length right) => left.length < right.length;
    public static Boolean operator <=(Length left, Length right) => left.length <= right.length;
    public static Length operator +(Length left, Length right) => new(left.length + right.length);
    public static Length operator -(Length left, Length right) => new(left.length - right.length);
    public static Area operator *(Length left, Length right) => Area.From(in left, in right);
    public static Volume operator *(Length length, Area area) => Volume.Times(length, area);
    public static Length operator *(Double scalar, Length right) => new(scalar * right.length);
    public static Length operator *(Length left, Double scalar) => new(scalar * left.length);
    public static Length operator /(Length left, Double scalar) => new(left.length / scalar);
    public static Double operator /(Length left, Length right) => left.length.Ratio(in right.length);

    public static Velocity operator /(Length length, Time time) => Velocity.From(in length, in time);
}
