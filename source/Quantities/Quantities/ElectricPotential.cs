using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;

namespace Quantities.Quantities;

public readonly struct ElectricPotential : IQuantity<ElectricPotential>, IElectricPotential
    , IFactory<ISiFactory<ElectricPotential, IElectricPotential>, SiOnly<To, ElectricPotential, IElectricPotential>, SiOnly<Create, ElectricPotential, IElectricPotential>>
    , IMultiplyOperators<ElectricPotential, ElectricCurrent, Power>
    , IDivisionOperators<ElectricPotential, ElectricCurrent, ElectricalResistance>
    , IDivisionOperators<ElectricPotential, ElectricalResistance, ElectricCurrent>
{
    private readonly Quantity potential;
    internal Quantity Value => this.potential;
    Quantity IQuantity<ElectricPotential>.Value => this.potential;
    public SiOnly<To, ElectricPotential, IElectricPotential> To => new(new To(in this.potential));
    private ElectricPotential(in Quantity value) => this.potential = value;
    public static SiOnly<Create, ElectricPotential, IElectricPotential> Of(in Double value) => new(new Create(in value));
    static ElectricPotential IFactory<ElectricPotential>.Create(in Quantity value) => new(in value);
    internal static ElectricPotential From(in ElectricCurrent current, in ElectricalResistance resistance) => new(current.Value * resistance.Value);
    internal static ElectricPotential From(in Power power, in ElectricCurrent current) => new(power.Value / current.Value);
    public Boolean Equals(ElectricPotential other) => this.potential.Equals(other.potential);
    public String ToString(String? format, IFormatProvider? provider) => this.potential.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricPotential potential && Equals(potential);
    public override Int32 GetHashCode() => this.potential.GetHashCode();
    public override String ToString() => this.potential.ToString();

    public static implicit operator Double(ElectricPotential potential) => potential.potential;
    public static Boolean operator ==(ElectricPotential left, ElectricPotential right) => left.Equals(right);
    public static Boolean operator !=(ElectricPotential left, ElectricPotential right) => !left.Equals(right);
    public static ElectricPotential operator +(ElectricPotential left, ElectricPotential right) => new(left.potential + right.potential);
    public static ElectricPotential operator -(ElectricPotential left, ElectricPotential right) => new(left.potential - right.potential);
    public static ElectricPotential operator *(Double scalar, ElectricPotential right) => new(scalar * right.potential);
    public static ElectricPotential operator *(ElectricPotential left, Double scalar) => new(scalar * left.potential);
    public static ElectricPotential operator /(ElectricPotential left, Double scalar) => new(left.potential / scalar);
    public static Double operator /(ElectricPotential left, ElectricPotential right) => left.potential.Divide(in right.potential);

    #region Ohm's Law
    public static ElectricalResistance operator /(ElectricPotential left, ElectricCurrent right) => ElectricalResistance.From(in left, in right);
    public static ElectricCurrent operator /(ElectricPotential left, ElectricalResistance right) => ElectricCurrent.From(in left, in right);
    #endregion Ohm's Law

    public static Power operator *(ElectricPotential left, ElectricCurrent right) => Power.From(in left, in right);
}
