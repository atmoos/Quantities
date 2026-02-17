using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct ElectricalResistance : IQuantity<ElectricalResistance>, IElectricalResistance, IScalar<ElectricalResistance, IElectricalResistance>
{
    private readonly Quantity electricalResistance;
    internal Quantity Value => this.electricalResistance;
    Quantity IQuantity<ElectricalResistance>.Value => this.electricalResistance;

    private ElectricalResistance(in Quantity value) => this.electricalResistance = value;

    public ElectricalResistance To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IElectricalResistance, IUnit => new(other.Transform(in this.electricalResistance));

    public static ElectricalResistance Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IElectricalResistance, IUnit => new(measure.Create(in value));

    static ElectricalResistance IFactory<ElectricalResistance>.Create(in Quantity value) => new(in value);

    public Boolean Equals(ElectricalResistance other) => this.electricalResistance.Equals(other.electricalResistance);

    public override Boolean Equals(Object? obj) => obj is ElectricalResistance electricalResistance && Equals(electricalResistance);

    public override Int32 GetHashCode() => this.electricalResistance.GetHashCode();

    public override String ToString() => this.electricalResistance.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.electricalResistance.ToString(format, provider);
}
