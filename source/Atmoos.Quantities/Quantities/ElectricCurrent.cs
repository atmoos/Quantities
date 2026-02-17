using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct ElectricCurrent : IQuantity<ElectricCurrent>, IElectricCurrent, IScalar<ElectricCurrent, IElectricCurrent>
{
    private readonly Quantity electricCurrent;
    internal Quantity Value => this.electricCurrent;
    Quantity IQuantity<ElectricCurrent>.Value => this.electricCurrent;

    private ElectricCurrent(in Quantity value) => this.electricCurrent = value;

    public ElectricCurrent To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IElectricCurrent, IUnit => new(other.Transform(in this.electricCurrent));

    public static ElectricCurrent Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IElectricCurrent, IUnit => new(measure.Create(in value));

    static ElectricCurrent IFactory<ElectricCurrent>.Create(in Quantity value) => new(in value);

    public Boolean Equals(ElectricCurrent other) => this.electricCurrent.Equals(other.electricCurrent);

    public override Boolean Equals(Object? obj) => obj is ElectricCurrent electricCurrent && Equals(electricCurrent);

    public override Int32 GetHashCode() => this.electricCurrent.GetHashCode();

    public override String ToString() => this.electricCurrent.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.electricCurrent.ToString(format, provider);
}
