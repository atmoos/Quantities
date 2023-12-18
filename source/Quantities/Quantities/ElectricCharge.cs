using System.Numerics;
using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct ElectricCharge : IQuantity<ElectricCharge>, IElectricCharge
    , IScalar<ElectricCharge, IElectricCharge>
    , IMultiplyOperators<ElectricCharge, ElectricPotential, Energy>
{
    private readonly Quantity electricCharge;
    internal Quantity Value => this.electricCharge;
    Quantity IQuantity<ElectricCharge>.Value => this.electricCharge;
    private ElectricCharge(in Quantity value) => this.electricCharge = value;
    public ElectricCharge To<TElectricCharge>(in Scalar<TElectricCharge> other)
        where TElectricCharge : IElectricCharge, IUnit => new(other.Transform(in this.electricCharge));
    public static ElectricCharge Of<TElectricCharge>(in Double value, in Scalar<TElectricCharge> measure)
        where TElectricCharge : IElectricCharge, IUnit => new(measure.Create(in value));
    static ElectricCharge IFactory<ElectricCharge>.Create(in Quantity value) => new(in value);
    internal static ElectricCharge From(in Energy energy, in ElectricPotential potential) => new(energy.Value / potential.Value);
    public Boolean Equals(ElectricCharge other) => this.electricCharge.Equals(other.electricCharge);
    public override Boolean Equals(Object? obj) => obj is ElectricCharge electricCharge && Equals(electricCharge);
    public override Int32 GetHashCode() => this.electricCharge.GetHashCode();
    public override String ToString() => this.electricCharge.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.electricCharge.ToString(format, provider);

    public static implicit operator Double(ElectricCharge electricCharge) => electricCharge.electricCharge;
    public static Boolean operator ==(ElectricCharge left, ElectricCharge right) => left.Equals(right);
    public static Boolean operator !=(ElectricCharge left, ElectricCharge right) => !left.Equals(right);
    public static ElectricCharge operator +(ElectricCharge left, ElectricCharge right) => new(left.electricCharge + right.electricCharge);
    public static ElectricCharge operator -(ElectricCharge left, ElectricCharge right) => new(left.electricCharge - right.electricCharge);
    public static ElectricCharge operator *(Double scalar, ElectricCharge right) => new(scalar * right.electricCharge);
    public static ElectricCharge operator *(ElectricCharge left, Double scalar) => new(scalar * left.electricCharge);
    public static ElectricCharge operator /(ElectricCharge left, Double scalar) => new(left.electricCharge / scalar);
    public static Double operator /(ElectricCharge left, ElectricCharge right) => left.electricCharge.Ratio(in right.electricCharge);

    public static Energy operator *(ElectricCharge charge, ElectricPotential potential) => Energy.Times(in charge, in potential);
}
