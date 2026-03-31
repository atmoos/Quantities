using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Density : IQuantity<Density>, IDensity, IQuotient<Density, IDensity, IMass, ILength, Three>
{
    private readonly Quantity density;
    internal Quantity Value => this.density;
    Quantity IQuantity<Density>.Value => this.density;

    internal Density(in Quantity value) => this.density = value;

    public Density To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IDensity, IUnit => new(other.Transform(in this.density));

    public Density To<TNominator, TDenominator>(in Quotient<TNominator, Power<TDenominator, Three>> other)
        where TNominator : IMass, IUnit
        where TDenominator : ILength, IUnit => new(other.Transform(in this.density));

    public static Density Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IDensity, IUnit => new(measure.Create(in value));

    public static Density Of<TMass, TLength>(in Double value, in Quotient<TMass, Power<TLength, Three>> measure)
        where TMass : IMass, IUnit
        where TLength : ILength, IUnit => new(measure.Create(in value));

    static Density IFactory<Density>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Density other) => this.density.Equals(other.density);

    public override Boolean Equals(Object? obj) => obj is Density density && Equals(density);

    public override Int32 GetHashCode() => this.density.GetHashCode();

    public override String ToString() => this.density.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.density.ToString(format, provider);
}
