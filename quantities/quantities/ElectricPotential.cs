﻿using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;

namespace Quantities.Quantities;

public readonly struct ElectricPotential : IQuantity<ElectricPotential>, IElectricPotential
    , IFactory<ISiFactory<ElectricPotential, IElectricPotential>, SiOnly<To, ElectricPotential, IElectricPotential>, SiOnly<Create, ElectricPotential, IElectricPotential>>
    , IMultiplyOperators<ElectricPotential, ElectricCurrent, Power>
    , IDivisionOperators<ElectricPotential, ElectricCurrent, ElectricalResistance>
    , IDivisionOperators<ElectricPotential, ElectricalResistance, ElectricCurrent>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<ElectricPotential>.Value => this.quant;
    public SiOnly<To, ElectricPotential, IElectricPotential> To => new(new To(in this.quant));
    private ElectricPotential(in Quant quant) => this.quant = quant;
    public static SiOnly<Create, ElectricPotential, IElectricPotential> Of(in Double value) => new(new Create(in value));
    static ElectricPotential IFactory<ElectricPotential>.Create(in Quant quant) => new(in quant);
    internal static ElectricPotential From(in ElectricCurrent current, in ElectricalResistance resistance)
    {
        return new(current.Quant.Multiply(Metric.TriadicScaling, resistance.Quant));
    }
    internal static ElectricPotential From(in Power power, in ElectricCurrent current)
    {
        return new(power.Quant.Divide(Metric.TriadicScaling, current.Quant));
    }
    public Boolean Equals(ElectricPotential other) => this.quant.Equals(other.quant);
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricPotential potential && Equals(potential);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();

    public static Boolean operator ==(ElectricPotential left, ElectricPotential right) => left.Equals(right);
    public static Boolean operator !=(ElectricPotential left, ElectricPotential right) => !left.Equals(right);
    public static implicit operator Double(ElectricPotential potential) => potential.quant.Value;
    public static ElectricPotential operator +(ElectricPotential left, ElectricPotential right) => new(left.quant + right.quant);
    public static ElectricPotential operator -(ElectricPotential left, ElectricPotential right) => new(left.quant - right.quant);
    public static ElectricPotential operator *(Double scalar, ElectricPotential right) => new(scalar * right.quant);
    public static ElectricPotential operator *(ElectricPotential left, Double scalar) => new(scalar * left.quant);
    public static ElectricPotential operator /(ElectricPotential left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(ElectricPotential left, ElectricPotential right) => left.quant / right.quant;

    #region Ohm's Law
    public static ElectricalResistance operator /(ElectricPotential left, ElectricCurrent right) => ElectricalResistance.From(in left, in right);
    public static ElectricCurrent operator /(ElectricPotential left, ElectricalResistance right) => ElectricCurrent.From(in left, in right);

    #endregion Ohm's Law

    public static Power operator *(ElectricPotential left, ElectricCurrent right) => Power.From(in left, in right);
}
