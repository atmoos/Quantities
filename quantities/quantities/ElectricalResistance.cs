using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;

namespace Quantities.Quantities;

public readonly struct ElectricalResistance : IQuantity<ElectricalResistance>, IElectricalResistance
    , IFactory<ISiFactory<ElectricalResistance, IElectricalResistance>, SiOnly<To, ElectricalResistance, IElectricalResistance>, SiOnly<Create, ElectricalResistance, IElectricalResistance>>
    , IMultiplyOperators<ElectricalResistance, ElectricCurrent, ElectricPotential>
{
    private readonly Quantity resistance;
    internal Quantity Value => this.resistance;
    Quantity IQuantity<ElectricalResistance>.Value => this.resistance;
    public SiOnly<To, ElectricalResistance, IElectricalResistance> To => new(new To(in this.resistance));
    private ElectricalResistance(in Quantity value) => this.resistance = value;
    public static SiOnly<Create, ElectricalResistance, IElectricalResistance> Of(in Double value) => new(new Create(in value));
    static ElectricalResistance IFactory<ElectricalResistance>.Create(in Quantity value) => new(in value);
    internal static ElectricalResistance From(in ElectricPotential potential, in ElectricCurrent current) => new(potential.Value / current.Value);
    public Boolean Equals(ElectricalResistance other) => this.resistance.Equals(other.resistance);
    public override Boolean Equals(Object? obj) => obj is ElectricalResistance resistance && Equals(resistance);
    public override Int32 GetHashCode() => this.resistance.GetHashCode();
    public override String ToString() => this.resistance.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.resistance.ToString(format, provider);

    public static implicit operator Double(ElectricalResistance resistance) => resistance.resistance;
    public static Boolean operator ==(ElectricalResistance left, ElectricalResistance right) => left.Equals(right);
    public static Boolean operator !=(ElectricalResistance left, ElectricalResistance right) => !left.Equals(right);
    public static ElectricalResistance operator +(ElectricalResistance left, ElectricalResistance right) => new(left.resistance + right.resistance);
    public static ElectricalResistance operator -(ElectricalResistance left, ElectricalResistance right) => new(left.resistance - right.resistance);
    public static ElectricalResistance operator *(Double scalar, ElectricalResistance right) => new(scalar * right.resistance);
    public static ElectricalResistance operator *(ElectricalResistance left, Double scalar) => new(scalar * left.resistance);
    public static ElectricalResistance operator /(ElectricalResistance left, Double scalar) => new(left.resistance / scalar);
    public static Double operator /(ElectricalResistance left, ElectricalResistance right) => left.resistance.Divide(in right.resistance);

    // Ohm's Law
    public static ElectricPotential operator *(ElectricalResistance resistance, ElectricCurrent current) => ElectricPotential.From(in current, in resistance);
}
