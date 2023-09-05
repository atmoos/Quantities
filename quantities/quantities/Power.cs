﻿using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct Power : IQuantity<Power>, IPower
    , IFactory<IDefaultFactory<Power, IPower>, Linear<To, Power, IPower>, Linear<Create, Power, IPower>>
    , IMultiplyOperators<Power, Time, Energy>
    , IDivisionOperators<Power, ElectricCurrent, ElectricPotential>
    , IDivisionOperators<Power, ElectricPotential, ElectricCurrent>
    , IDivisionOperators<Power, Force, Velocity>
    , IDivisionOperators<Power, Velocity, Force>
{
    private static readonly IRoot root = new SiRoot<Watt>();
    private readonly Quantity power;
    internal Quantity Value => this.power;
    Quantity IQuantity<Power>.Value => this.power;
    public Linear<To, Power, IPower> To => new(new To(in this.power));
    private Power(in Quantity value) => this.power = value;
    public static Linear<Create, Power, IPower> Of(in Double value) => new(new Create(in value));
    static Power IFactory<Power>.Create(in Quantity value) => new(in value);
    internal static Power From(in ElectricPotential potential, in ElectricCurrent current)
    {
        return new(MetricPrefix.ScaleThree(potential.Value.SiMultiply(current.Value), root));
    }
    internal static Power From(in Force force, in Velocity velocity)
    {
        return new(MetricPrefix.ScaleThree(force.Value.SiMultiply(velocity.Value), root));
    }
    internal static Power From(in Energy energy, in Time time)
    {
        return new(MetricPrefix.ScaleThree(energy.Value.SiDivide(time.Value), root));
    }

    public Boolean Equals(Power other) => this.power.Equals(other.power);
    public override Boolean Equals(Object? obj) => obj is Power power && Equals(power);
    public override Int32 GetHashCode() => this.power.GetHashCode();
    public override String ToString() => this.power.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.power.ToString(format, provider);

    public static Boolean operator ==(Power left, Power right) => left.Equals(right);
    public static Boolean operator !=(Power left, Power right) => !left.Equals(right);
    public static implicit operator Double(Power power) => power.power;
    public static Power operator +(Power left, Power right) => new(left.power + right.power);
    public static Power operator -(Power left, Power right) => new(left.power - right.power);
    public static Power operator *(Double scalar, Power right) => new(scalar * right.power);
    public static Power operator *(Power left, Double scalar) => new(scalar * left.power);
    public static Power operator /(Power left, Double scalar) => new(left.power / scalar);
    public static Double operator /(Power left, Power right) => left.power / right.power;

    public static ElectricPotential operator /(Power power, ElectricCurrent current) => ElectricPotential.From(in power, in current);
    public static ElectricCurrent operator /(Power power, ElectricPotential potential) => ElectricCurrent.From(in power, in potential);
    public static Velocity operator /(Power power, Force force) => Velocity.From(in power, in force);
    public static Force operator /(Power power, Velocity velocity) => Force.From(in power, in velocity);
    public static Energy operator *(Power left, Time right) => Energy.From(in left, in right);
}
