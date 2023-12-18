using System.Numerics;
using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;


/// <summary>
/// Also known as (volumetric mass) density.
/// </summary>
// See: https://en.wikipedia.org/wiki/Density
public readonly struct SpecificMass : IQuantity<SpecificMass>, ISpecificMass
    , IScalar<SpecificMass, ISpecificMass>
    , IQuotient<SpecificMass, ISpecificMass, IMass, IVolume>
    , IMultiplyOperators<SpecificMass, Volume, Mass>
{
    private readonly Quantity specificMass;
    internal Quantity Value => this.specificMass;
    Quantity IQuantity<SpecificMass>.Value => this.specificMass;
    private SpecificMass(in Quantity value) => this.specificMass = value;
    public SpecificMass To<TSpecificMass>(in Scalar<TSpecificMass> other)
        where TSpecificMass : ISpecificMass, IUnit => new(other.Transform(in this.specificMass));
    public SpecificMass To<TMass, TVolume>(in Quotient<TMass, TVolume> other)
        where TMass : IMass, IUnit
        where TVolume : IVolume, IUnit => new(other.Transform(in this.specificMass));
    public SpecificMass To<TMass, TLength>(in Quotient<TMass, Cubic<TLength>> other)
        where TMass : IMass, IUnit
        where TLength : ILength, IUnit => new(other.Transform(in this.specificMass));
    public static SpecificMass Of<TSpecificMass>(in Double value, in Scalar<TSpecificMass> measure)
        where TSpecificMass : ISpecificMass, IUnit => new(measure.Create(in value));
    public static SpecificMass Of<TMass, TVolume>(in Double value, in Quotient<TMass, TVolume> measure)
        where TMass : IMass, IUnit
        where TVolume : IVolume, IUnit => new(measure.Create(in value));
    public static SpecificMass Of<TMass, TLength>(in Double value, in Quotient<TMass, Cubic<TLength>> measure)
        where TMass : IMass, IUnit
        where TLength : ILength, IUnit => new(measure.Create(in value));
    static SpecificMass IFactory<SpecificMass>.Create(in Quantity value) => new(in value);
    internal static SpecificMass From(in Mass mass, in Volume volume) => new(mass.Value / volume.Value);
    public Boolean Equals(SpecificMass other) => this.specificMass.Equals(other.specificMass);
    public override Boolean Equals(Object? obj) => obj is SpecificMass specificMass && Equals(specificMass);
    public override Int32 GetHashCode() => this.specificMass.GetHashCode();
    public override String ToString() => this.specificMass.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.specificMass.ToString(format, provider);

    public static implicit operator Double(SpecificMass specificMass) => specificMass.specificMass;
    public static Boolean operator ==(SpecificMass left, SpecificMass right) => left.Equals(right);
    public static Boolean operator !=(SpecificMass left, SpecificMass right) => !left.Equals(right);
    public static SpecificMass operator +(SpecificMass left, SpecificMass right) => new(left.specificMass + right.specificMass);
    public static SpecificMass operator -(SpecificMass left, SpecificMass right) => new(left.specificMass - right.specificMass);
    public static SpecificMass operator *(Double scalar, SpecificMass right) => new(scalar * right.specificMass);
    public static SpecificMass operator *(SpecificMass left, Double scalar) => new(scalar * left.specificMass);
    public static SpecificMass operator /(SpecificMass left, Double scalar) => new(left.specificMass / scalar);
    public static Double operator /(SpecificMass left, SpecificMass right) => left.specificMass.Ratio(in right.specificMass);

    public static Mass operator *(SpecificMass specificMass, Volume volume) => Mass.Times(in specificMass, in volume);
}
