using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct ElectricPotential : IQuantity<ElectricPotential>, IElectricPotential
    , IFactory<ElectricPotential>
    , IFactory<ISiFactory<ElectricPotential, IElectricPotential>, SiTo<ElectricPotential, IElectricPotential>, SiCreate<ElectricPotential, IElectricPotential>>
    , IMultiplyOperators<ElectricPotential, ElectricCurrent, Power>
    , IDivisionOperators<ElectricPotential, ElectricCurrent, ElectricalResistance>
    , IDivisionOperators<ElectricPotential, ElectricalResistance, ElectricCurrent>
{
    private static readonly IRoot root = new SiRoot<Volt>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    public SiTo<ElectricPotential, IElectricPotential> To => new(in this.quant);
    private ElectricPotential(in Quant quant) => this.quant = quant;
    public static SiCreate<ElectricPotential, IElectricPotential> Of(in Double value) => new(in value);
    static ElectricPotential IFactory<ElectricPotential>.Create(in Quant quant) => new(in quant);

    public Boolean Equals(ElectricPotential other) => this.quant.Equals(other.quant);
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricPotential potential && Equals(potential);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    internal static ElectricPotential From(in ElectricCurrent current, in ElectricalResistance resistance)
    {
        return new(MetricPrefix.ScaleThree(current.Quant.SiMultiply(resistance.Quant), root));
    }
    internal static ElectricPotential From(in Power power, in ElectricCurrent current)
    {
        Double siPower = power.To.Si<Watt>();
        Double siCurrent = current.To.Si<Ampere>();
        return new(MetricPrefix.ScaleThree(siPower / siCurrent, root));
    }

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
