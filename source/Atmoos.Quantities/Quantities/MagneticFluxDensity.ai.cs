using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public readonly struct MagneticFluxDensity : IQuantity<MagneticFluxDensity>, IMagneticFluxDensity, IQuotient<MagneticFluxDensity, IMagneticFluxDensity, IMagneticFlux, ILength, Two>
{
    private readonly Quantity magneticFluxDensity;
    internal Quantity Value => this.magneticFluxDensity;
    Quantity IQuantity<MagneticFluxDensity>.Value => this.magneticFluxDensity;

    internal MagneticFluxDensity(in Quantity value) => this.magneticFluxDensity = value;

    public MagneticFluxDensity To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IMagneticFluxDensity, IUnit => new(other.Transform(in this.magneticFluxDensity));

    public MagneticFluxDensity To<TNominator, TDenominator>(in Quotient<TNominator, Power<TDenominator, Two>> other)
        where TNominator : IMagneticFlux, IUnit
        where TDenominator : ILength, IUnit => new(other.Transform(in this.magneticFluxDensity));

    public static MagneticFluxDensity Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IMagneticFluxDensity, IUnit => new(measure.Create(in value));

    public static MagneticFluxDensity Of<TMagneticFlux, TLength>(in Double value, in Quotient<TMagneticFlux, Power<TLength, Two>> measure)
        where TMagneticFlux : IMagneticFlux, IUnit
        where TLength : ILength, IUnit => new(measure.Create(in value));

    static MagneticFluxDensity IFactory<MagneticFluxDensity>.Create(in Quantity value) => new(in value);

    public Boolean Equals(MagneticFluxDensity other) => this.magneticFluxDensity.Equals(other.magneticFluxDensity);

    public override Boolean Equals(Object? obj) => obj is MagneticFluxDensity magneticFluxDensity && Equals(magneticFluxDensity);

    public override Int32 GetHashCode() => this.magneticFluxDensity.GetHashCode();

    public override String ToString() => this.magneticFluxDensity.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.magneticFluxDensity.ToString(format, provider);
}
