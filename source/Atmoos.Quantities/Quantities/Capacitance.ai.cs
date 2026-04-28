using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public readonly struct Capacitance : IQuantity<Capacitance>, ICapacitance, IScalar<Capacitance, ICapacitance>
{
    private readonly Quantity capacitance;
    internal Quantity Value => this.capacitance;
    Quantity IQuantity<Capacitance>.Value => this.capacitance;

    private Capacitance(in Quantity value) => this.capacitance = value;

    public Capacitance To<TUnit>(in Scalar<TUnit> other)
        where TUnit : ICapacitance, IUnit => new(other.Transform(in this.capacitance));

    public static Capacitance Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : ICapacitance, IUnit => new(measure.Create(in value));

    static Capacitance IFactory<Capacitance>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Capacitance other) => this.capacitance.Equals(other.capacitance);

    public override Boolean Equals(Object? obj) => obj is Capacitance capacitance && Equals(capacitance);

    public override Int32 GetHashCode() => this.capacitance.GetHashCode();

    public override String ToString() => this.capacitance.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.capacitance.ToString(format, provider);
}
