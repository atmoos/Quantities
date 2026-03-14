using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct ElectricPotential : IQuantity<ElectricPotential>, IElectricPotential, IScalar<ElectricPotential, IElectricPotential>
{
    private readonly Quantity electricPotential;
    internal Quantity Value => this.electricPotential;
    Quantity IQuantity<ElectricPotential>.Value => this.electricPotential;

    private ElectricPotential(in Quantity value) => this.electricPotential = value;

    public ElectricPotential To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IElectricPotential, IUnit => new(other.Transform(in this.electricPotential));

    public static ElectricPotential Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IElectricPotential, IUnit => new(measure.Create(in value));

    static ElectricPotential IFactory<ElectricPotential>.Create(in Quantity value) => new(in value);

    public Boolean Equals(ElectricPotential other) => this.electricPotential.Equals(other.electricPotential);

    public override Boolean Equals(Object? obj) => obj is ElectricPotential electricPotential && Equals(electricPotential);

    public override Int32 GetHashCode() => this.electricPotential.GetHashCode();

    public override String ToString() => this.electricPotential.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.electricPotential.ToString(format, provider);
}
