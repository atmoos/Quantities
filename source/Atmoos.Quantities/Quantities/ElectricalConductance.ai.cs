using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct ElectricalConductance : IQuantity<ElectricalConductance>, IElectricalConductance, IInvertible<ElectricalConductance, IElectricalConductance, IElectricalResistance>
{
    private readonly Quantity electricalConductance;
    internal Quantity Value => this.electricalConductance;
    Quantity IQuantity<ElectricalConductance>.Value => this.electricalConductance;

    private ElectricalConductance(in Quantity value) => this.electricalConductance = value;

    public ElectricalConductance To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IElectricalConductance, IInvertible<IElectricalResistance>, IUnit => new(other.Transform(in this.electricalConductance, static f => ref f.InverseOf<TUnit, IElectricalResistance>()));

    public static ElectricalConductance Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IElectricalConductance, IInvertible<IElectricalResistance>, IUnit => new(measure.Create(in value, static f => ref f.InverseOf<TUnit, IElectricalResistance>()));

    static ElectricalConductance IFactory<ElectricalConductance>.Create(in Quantity value) => new(in value);

    public Boolean Equals(ElectricalConductance other) => this.electricalConductance.Equals(other.electricalConductance);

    public override Boolean Equals(Object? obj) => obj is ElectricalConductance electricalConductance && Equals(electricalConductance);

    public override Int32 GetHashCode() => this.electricalConductance.GetHashCode();

    public override String ToString() => this.electricalConductance.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.electricalConductance.ToString(format, provider);
}
