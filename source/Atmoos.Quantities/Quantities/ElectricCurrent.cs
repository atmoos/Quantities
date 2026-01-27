using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct ElectricCurrent : IQuantity<ElectricCurrent>, IElectricCurrent, IScalar<ElectricCurrent, IElectricCurrent>
{
    private readonly Quantity current;
    internal Quantity Value => this.current;
    Quantity IQuantity<ElectricCurrent>.Value => this.current;

    private ElectricCurrent(in Quantity value) => this.current = value;

    public ElectricCurrent To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IElectricCurrent, IUnit => new(other.Transform(in this.current));

    public static ElectricCurrent Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IElectricCurrent, IUnit => new(measure.Create(in value));

    static ElectricCurrent IFactory<ElectricCurrent>.Create(in Quantity value) => new(in value);

    public Boolean Equals(ElectricCurrent other) => this.current.Equals(other.current);

    public String ToString(String? format, IFormatProvider? provider) => this.current.ToString(format, provider);

    public override Boolean Equals(Object? obj) => obj is ElectricCurrent current && Equals(current);

    public override Int32 GetHashCode() => this.current.GetHashCode();

    public override String ToString() => this.current.ToString();
}
