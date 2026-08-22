using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct AmountOfSubstance : IQuantity<AmountOfSubstance>, IAmountOfSubstance, IScalar<AmountOfSubstance, IAmountOfSubstance>
{
    private readonly Quantity amountOfSubstance;
    internal Quantity Value => this.amountOfSubstance;
    Quantity IQuantity<AmountOfSubstance>.Value => this.amountOfSubstance;

    private AmountOfSubstance(in Quantity value) => this.amountOfSubstance = value;

    public AmountOfSubstance To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IAmountOfSubstance, IUnit => new(other.Transform(in this.amountOfSubstance));

    public static AmountOfSubstance Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IAmountOfSubstance, IUnit => new(measure.Create(in value));

    static AmountOfSubstance IFactory<AmountOfSubstance>.Create(in Quantity value) => new(in value);

    public Boolean Equals(AmountOfSubstance other) => this.amountOfSubstance.Equals(other.amountOfSubstance);

    public override Boolean Equals(Object? obj) => obj is AmountOfSubstance amountOfSubstance && Equals(amountOfSubstance);

    public override Int32 GetHashCode() => this.amountOfSubstance.GetHashCode();

    public override String ToString() => this.amountOfSubstance.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.amountOfSubstance.ToString(format, provider);
}
