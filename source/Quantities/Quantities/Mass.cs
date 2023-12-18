﻿using System.Numerics;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Mass : IQuantity<Mass>, IMass
    , IScalar<Mass, IMass>
    , IDivisionOperators<Mass, Volume, SpecificMass>
{
    private readonly Quantity mass;
    internal Quantity Value => this.mass;
    Quantity IQuantity<Mass>.Value => this.mass;
    private Mass(in Quantity value) => this.mass = value;
    public Mass To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IMass, IUnit => new(other.Transform(in this.mass));
    public static Mass Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IMass, IUnit => new(measure.Create(in value));
    static Mass IFactory<Mass>.Create(in Quantity value) => new(in value);
    internal static Mass Times(in Volume volume, in SpecificMass specificMass) => new(volume.Value * specificMass.Value);
    internal static Mass Times(in SpecificMass specificMass, in Volume volume) => new(specificMass.Value * volume.Value);
    public Boolean Equals(Mass other) => this.mass.Equals(other.mass);
    public override Boolean Equals(Object? obj) => obj is Mass mass && Equals(mass);
    public override Int32 GetHashCode() => this.mass.GetHashCode();
    public override String ToString() => this.mass.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.mass.ToString(format, provider);

    public static implicit operator Double(Mass mass) => mass.mass;
    public static Boolean operator ==(Mass left, Mass right) => left.Equals(right);
    public static Boolean operator !=(Mass left, Mass right) => !left.Equals(right);
    public static Mass operator +(Mass left, Mass right) => new(left.mass + right.mass);
    public static Mass operator -(Mass left, Mass right) => new(left.mass - right.mass);
    public static Mass operator *(Double scalar, Mass right) => new(scalar * right.mass);
    public static Mass operator *(Mass left, Double scalar) => new(scalar * left.mass);
    public static Mass operator /(Mass left, Double scalar) => new(left.mass / scalar);
    public static Double operator /(Mass left, Mass right) => left.mass.Ratio(in right.mass);

    public static SpecificMass operator /(Mass mass, Volume volume) => SpecificMass.From(in mass, in volume);
}
