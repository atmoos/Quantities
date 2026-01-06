using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct ElectricalResistance : IQuantity<ElectricalResistance>, IElectricalResistance, IScalar<ElectricalResistance, IElectricalResistance>
{
    private readonly Quantity resistance;
    internal Quantity Value => this.resistance;
    Quantity IQuantity<ElectricalResistance>.Value => this.resistance;

    private ElectricalResistance(in Quantity value) => this.resistance = value;

    public ElectricalResistance To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IElectricalResistance, IUnit => new(other.Transform(in this.resistance));

    public static ElectricalResistance Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IElectricalResistance, IUnit => new(measure.Create(in value));

    static ElectricalResistance IFactory<ElectricalResistance>.Create(in Quantity value) => new(in value);

    public Boolean Equals(ElectricalResistance other) => this.resistance.Equals(other.resistance);

    public override Boolean Equals(Object? obj) => obj is ElectricalResistance resistance && Equals(resistance);

    public override Int32 GetHashCode() => this.resistance.GetHashCode();

    public override String ToString() => this.resistance.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.resistance.ToString(format, provider);
}
