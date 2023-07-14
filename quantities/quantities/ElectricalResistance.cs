using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct ElectricalResistance : IQuantity<ElectricalResistance>, IElectricalResistance
    , IFactory<ISiFactory<ElectricalResistance, IElectricalResistance>, SiFactory<To, ElectricalResistance, IElectricalResistance>, SiFactory<Create, ElectricalResistance, IElectricalResistance>>
    , IMultiplyOperators<ElectricalResistance, ElectricCurrent, ElectricPotential>
{
    private static readonly IRoot root = new SiRoot<Ohm>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<ElectricalResistance>.Value => this.quant;
    public SiFactory<To, ElectricalResistance, IElectricalResistance> To => new(new To(in this.quant));
    private ElectricalResistance(in Quant quant) => this.quant = quant;
    public static SiFactory<Create, ElectricalResistance, IElectricalResistance> Of(in Double value) => new(new Create(in value));
    static ElectricalResistance IFactory<ElectricalResistance>.Create(in Quant quant) => new(in quant);
    internal static ElectricalResistance From(in ElectricPotential potential, in ElectricCurrent current)
    {
        return new(MetricPrefix.ScaleThree(potential.Quant.SiDivide(current.Quant), root));
    }

    public Boolean Equals(ElectricalResistance other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is ElectricalResistance resistance && Equals(resistance);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(ElectricalResistance left, ElectricalResistance right) => left.Equals(right);
    public static Boolean operator !=(ElectricalResistance left, ElectricalResistance right) => !left.Equals(right);
    public static implicit operator Double(ElectricalResistance resistance) => resistance.quant.Value;
    public static ElectricalResistance operator +(ElectricalResistance left, ElectricalResistance right) => new(left.quant + right.quant);
    public static ElectricalResistance operator -(ElectricalResistance left, ElectricalResistance right) => new(left.quant - right.quant);
    public static ElectricalResistance operator *(Double scalar, ElectricalResistance right) => new(scalar * right.quant);
    public static ElectricalResistance operator *(ElectricalResistance left, Double scalar) => new(scalar * left.quant);
    public static ElectricalResistance operator /(ElectricalResistance left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(ElectricalResistance left, ElectricalResistance right) => left.quant / right.quant;

    // Ohm's Law
    public static ElectricPotential operator *(ElectricalResistance resistance, ElectricCurrent current) => ElectricPotential.From(in current, in resistance);
}
