﻿using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Unit.Imperial;
using Quantities.Unit.Other;
using Quantities.Unit.Si;
using Quantities.Unit.Si.Derived;

namespace Quantities;

public readonly struct Temperature : ITemperature, IEquatable<Temperature>, IFormattable
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Temperature(in Quant quant) => this.quant = quant;
    public Temperature To<TUnit>()
        where TUnit : ISiBaseUnit, ITemperature
    {
        return new(this.quant.As<Si<TUnit>>());
    }
    public Temperature To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiBaseUnit, ITemperature
    {
        return new(this.quant.As<Si<TPrefix, TUnit>>());
    }
    public Temperature ToCelsius() => new(this.quant.As<SiDerived<Celsius>>());
    public Temperature ToImperial<TUnit>()
        where TUnit : IImperial, ITemperature
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public Temperature ToOther<TUnit>()
        where TUnit : IOther, ITemperature
    {
        return new(this.quant.As<Other<TUnit>>());
    }
    public static Temperature Si<TUnit>(in Double value)
        where TUnit : ISiBaseUnit, ITemperature
    {
        return new(value.As<Si<TUnit>>());
    }
    public static Temperature Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiBaseUnit, ITemperature
    {
        return new(value.As<Si<TPrefix, TUnit>>());
    }
    public static Temperature Celsius(in Double value)
    {
        return new(value.As<SiDerived<Celsius>>());
    }
    public static Temperature Imperial<TUnit>(Double value)
        where TUnit : IImperial, ITemperature
    {
        return new(value.As<Other<TUnit>>());
    }
    public static Temperature Other<TUnit>(Double value)
        where TUnit : IOther, ITemperature
    {
        return new(value.As<Other<TUnit>>());
    }

    public Boolean Equals(Temperature other) => this.quant.Equals(other.quant);

    public override Boolean Equals(Object? obj) => obj is Temperature temperature && Equals(temperature);

    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Temperature left, Temperature right) => left.Equals(right);
    public static Boolean operator !=(Temperature left, Temperature right) => !left.Equals(right);
    public static implicit operator Double(Temperature temperature) => temperature.quant.Value;
    public static Temperature operator +(Temperature left, Temperature right) => new(left.quant + right.quant);
    public static Temperature operator -(Temperature left, Temperature right) => new(left.quant - right.quant);
    public static Temperature operator *(Double scalar, Temperature right) => new(scalar * right.quant);
    public static Temperature operator *(Temperature left, Double scalar) => new(scalar * left.quant);
    public static Temperature operator /(Temperature left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Temperature left, Temperature right) => left.quant / right.quant;
}