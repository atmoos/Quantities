using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct SpecificEnergy : IQuantity<SpecificEnergy>, ISpecificEnergy, IQuotient<SpecificEnergy, ISpecificEnergy, IEnergy, IMass>
{
    private readonly Quantity specificEnergy;
    internal Quantity Value => this.specificEnergy;
    Quantity IQuantity<SpecificEnergy>.Value => this.specificEnergy;

    internal SpecificEnergy(in Quantity value) => this.specificEnergy = value;

    public SpecificEnergy To<TUnit>(in Scalar<TUnit> other)
        where TUnit : ISpecificEnergy, IUnit => new(other.Transform(in this.specificEnergy));

    public SpecificEnergy To<TNominator, TDenominator>(in Quotient<TNominator, TDenominator> other)
        where TNominator : IEnergy, IUnit
        where TDenominator : IMass, IUnit => new(other.Transform(in this.specificEnergy));

    public static SpecificEnergy Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : ISpecificEnergy, IUnit => new(measure.Create(in value));

    public static SpecificEnergy Of<TNominator, TDenominator>(in Double value, in Quotient<TNominator, TDenominator> measure)
        where TNominator : IEnergy, IUnit
        where TDenominator : IMass, IUnit => new(measure.Create(in value));

    static SpecificEnergy IFactory<SpecificEnergy>.Create(in Quantity value) => new(in value);

    public Boolean Equals(SpecificEnergy other) => this.specificEnergy.Equals(other.specificEnergy);

    public override Boolean Equals(Object? obj) => obj is SpecificEnergy specificEnergy && Equals(specificEnergy);

    public override Int32 GetHashCode() => this.specificEnergy.GetHashCode();

    public override String ToString() => this.specificEnergy.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.specificEnergy.ToString(format, provider);
}
