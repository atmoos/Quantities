﻿using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Unit.Imperial;
using Quantities.Unit.Si;

namespace Quantities.Quantities;

public readonly struct Mass : IMass, IEquatable<Mass>, IFormattable
{
    private readonly Quant quant;
    private Mass(in Quant quant) => this.quant = quant;
    public Mass ToKilogram() => new(this.quant.As<Si<Kilogram>>());
    public Mass To<TUnit>()
        where TUnit : ISiDerivedUnit, IMass
    {
        return new(this.quant.As<SiDerived<TUnit>>());
    }
    public Mass To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiDerivedUnit, IMass
    {
        return new(this.quant.As<SiDerived<TPrefix, TUnit>>());
    }
    public Mass ToImperial<TUnit>()
        where TUnit : IImperial, IMass
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public static Mass Kilogram(in Double value) => new(value.As<Si<Kilogram>>());
    public static Mass Si<TUnit>(in Double value)
        where TUnit : ISiDerivedUnit, IMass
    {
        return new(value.As<SiDerived<TUnit>>());
    }
    public static Mass Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiDerivedUnit, IMass
    {
        return new(value.As<SiDerived<TPrefix, TUnit>>());
    }
    public static Mass Imperial<TUnit>(Double value)
        where TUnit : IImperial, IMass
    {
        return new(value.As<Other<TUnit>>());
    }

    public Boolean Equals(Mass other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Mass mass && Equals(mass);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public static Boolean operator ==(Mass left, Mass right) => left.Equals(right);
    public static Boolean operator !=(Mass left, Mass right) => !left.Equals(right);
    public static implicit operator Double(Mass mass) => mass.quant.Value;
    public static Mass operator +(Mass left, Mass right) => new(left.quant + right.quant);
    public static Mass operator -(Mass left, Mass right) => new(left.quant - right.quant);
    public static Mass operator *(Double scalar, Mass right) => new(scalar * right.quant);
    public static Mass operator *(Mass left, Double scalar) => new(scalar * left.quant);
    public static Mass operator /(Mass left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Mass left, Mass right) => left.quant / right.quant;
}
