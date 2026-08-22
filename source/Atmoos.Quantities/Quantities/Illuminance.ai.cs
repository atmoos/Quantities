using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public readonly struct Illuminance : IQuantity<Illuminance>, IIlluminance, IQuotient<Illuminance, IIlluminance, ILuminousFlux, ILength, Two>
{
    private readonly Quantity illuminance;
    internal Quantity Value => this.illuminance;
    Quantity IQuantity<Illuminance>.Value => this.illuminance;

    internal Illuminance(in Quantity value) => this.illuminance = value;

    public Illuminance To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IIlluminance, IUnit => new(other.Transform(in this.illuminance));

    public Illuminance To<TNominator, TDenominator>(in Quotient<TNominator, Power<TDenominator, Two>> other)
        where TNominator : ILuminousFlux, IUnit
        where TDenominator : ILength, IUnit => new(other.Transform(in this.illuminance));

    public static Illuminance Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IIlluminance, IUnit => new(measure.Create(in value));

    public static Illuminance Of<TLuminousFlux, TLength>(in Double value, in Quotient<TLuminousFlux, Power<TLength, Two>> measure)
        where TLuminousFlux : ILuminousFlux, IUnit
        where TLength : ILength, IUnit => new(measure.Create(in value));

    static Illuminance IFactory<Illuminance>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Illuminance other) => this.illuminance.Equals(other.illuminance);

    public override Boolean Equals(Object? obj) => obj is Illuminance illuminance && Equals(illuminance);

    public override Int32 GetHashCode() => this.illuminance.GetHashCode();

    public override String ToString() => this.illuminance.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.illuminance.ToString(format, provider);
}
