using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct AmountOfSubstance : IQuantity<AmountOfSubstance>, IAmountOfSubstance
    , IScalar<AmountOfSubstance, IAmountOfSubstance>
{
    private readonly Quantity amountOfSubstance;
    internal Quantity Value => this.amountOfSubstance;
    Quantity IQuantity<AmountOfSubstance>.Value => this.amountOfSubstance;
    private AmountOfSubstance(in Quantity value) => this.amountOfSubstance = value;
    public AmountOfSubstance To<TAmountOfSubstance>(in Scalar<TAmountOfSubstance> other)
        where TAmountOfSubstance : IAmountOfSubstance, IUnit => new(other.Transform(in this.amountOfSubstance));
    public static AmountOfSubstance Of<TAmountOfSubstance>(in Double value, in Scalar<TAmountOfSubstance> measure)
        where TAmountOfSubstance : IAmountOfSubstance, IUnit => new(measure.Create(in value));
    static AmountOfSubstance IFactory<AmountOfSubstance>.Create(in Quantity value) => new(in value);
    public Boolean Equals(AmountOfSubstance other) => this.amountOfSubstance.Equals(other.amountOfSubstance);
    public override Boolean Equals(Object? obj) => obj is AmountOfSubstance amountOfSubstance && Equals(amountOfSubstance);
    public override Int32 GetHashCode() => this.amountOfSubstance.GetHashCode();
    public override String ToString() => this.amountOfSubstance.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.amountOfSubstance.ToString(format, provider);

    public static implicit operator Double(AmountOfSubstance amountOfSubstance) => amountOfSubstance.amountOfSubstance;
    public static Boolean operator ==(AmountOfSubstance left, AmountOfSubstance right) => left.Equals(right);
    public static Boolean operator !=(AmountOfSubstance left, AmountOfSubstance right) => !left.Equals(right);
    public static AmountOfSubstance operator +(AmountOfSubstance left, AmountOfSubstance right) => new(left.amountOfSubstance + right.amountOfSubstance);
    public static AmountOfSubstance operator -(AmountOfSubstance left, AmountOfSubstance right) => new(left.amountOfSubstance - right.amountOfSubstance);
    public static AmountOfSubstance operator *(Double scalar, AmountOfSubstance right) => new(scalar * right.amountOfSubstance);
    public static AmountOfSubstance operator *(AmountOfSubstance left, Double scalar) => new(scalar * left.amountOfSubstance);
    public static AmountOfSubstance operator /(AmountOfSubstance left, Double scalar) => new(left.amountOfSubstance / scalar);
    public static Double operator /(AmountOfSubstance left, AmountOfSubstance right) => left.amountOfSubstance.Ratio(in right.amountOfSubstance);
}
