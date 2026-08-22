using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct MassFlowRate : IQuantity<MassFlowRate>, IMassFlowRate, IQuotient<MassFlowRate, IMassFlowRate, IMass, ITime>
{
    private readonly Quantity massFlowRate;
    internal Quantity Value => this.massFlowRate;
    Quantity IQuantity<MassFlowRate>.Value => this.massFlowRate;

    internal MassFlowRate(in Quantity value) => this.massFlowRate = value;

    public MassFlowRate To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IMassFlowRate, IUnit => new(other.Transform(in this.massFlowRate));

    public MassFlowRate To<TNominator, TDenominator>(in Quotient<TNominator, TDenominator> other)
        where TNominator : IMass, IUnit
        where TDenominator : ITime, IUnit => new(other.Transform(in this.massFlowRate));

    public static MassFlowRate Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IMassFlowRate, IUnit => new(measure.Create(in value));

    public static MassFlowRate Of<TMass, TTime>(in Double value, in Quotient<TMass, TTime> measure)
        where TMass : IMass, IUnit
        where TTime : ITime, IUnit => new(measure.Create(in value));

    static MassFlowRate IFactory<MassFlowRate>.Create(in Quantity value) => new(in value);

    public Boolean Equals(MassFlowRate other) => this.massFlowRate.Equals(other.massFlowRate);

    public override Boolean Equals(Object? obj) => obj is MassFlowRate massFlowRate && Equals(massFlowRate);

    public override Int32 GetHashCode() => this.massFlowRate.GetHashCode();

    public override String ToString() => this.massFlowRate.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.massFlowRate.ToString(format, provider);
}
