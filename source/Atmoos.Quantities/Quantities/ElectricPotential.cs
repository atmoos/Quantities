using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct ElectricPotential : IQuantity<ElectricPotential>, IElectricPotential
    , IScalar<ElectricPotential, IElectricPotential>
{
    private readonly Quantity potential;
    internal Quantity Value => this.potential;
    Quantity IQuantity<ElectricPotential>.Value => this.potential;
    private ElectricPotential(in Quantity value) => this.potential = value;
    public ElectricPotential To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IElectricPotential, IUnit => new(other.Transform(in this.potential));
    public static ElectricPotential Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IElectricPotential, IUnit => new(measure.Create(in value));
    static ElectricPotential IFactory<ElectricPotential>.Create(in Quantity value) => new(in value);
    public Boolean Equals(ElectricPotential other) => this.potential.Equals(other.potential);
    public String ToString(String? format, IFormatProvider? provider) => this.potential.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricPotential potential && Equals(potential);
    public override Int32 GetHashCode() => this.potential.GetHashCode();
    public override String ToString() => this.potential.ToString();
}
