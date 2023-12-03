using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct LuminousIntensity : IQuantity<LuminousIntensity>, ILuminousIntensity
    , IScalar<LuminousIntensity, ILuminousIntensity>
{
    private readonly Quantity luminousIntensity;
    internal Quantity Value => this.luminousIntensity;
    Quantity IQuantity<LuminousIntensity>.Value => this.luminousIntensity;
    private LuminousIntensity(in Quantity value) => this.luminousIntensity = value;
    public LuminousIntensity To<TLuminousIntensity>(in Scalar<TLuminousIntensity> other)
        where TLuminousIntensity : ILuminousIntensity, IUnit => new(other.Transform(in this.luminousIntensity));
    public static LuminousIntensity Of<TLuminousIntensity>(in Double value, in Scalar<TLuminousIntensity> measure)
        where TLuminousIntensity : ILuminousIntensity, IUnit => new(measure.Create(in value));
    static LuminousIntensity IFactory<LuminousIntensity>.Create(in Quantity value) => new(in value);
    public Boolean Equals(LuminousIntensity other) => this.luminousIntensity.Equals(other.luminousIntensity);
    public override Boolean Equals(Object? obj) => obj is LuminousIntensity luminousIntensity && Equals(luminousIntensity);
    public override Int32 GetHashCode() => this.luminousIntensity.GetHashCode();
    public override String ToString() => this.luminousIntensity.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.luminousIntensity.ToString(format, provider);

    public static implicit operator Double(LuminousIntensity luminousIntensity) => luminousIntensity.luminousIntensity;
    public static Boolean operator ==(LuminousIntensity left, LuminousIntensity right) => left.Equals(right);
    public static Boolean operator !=(LuminousIntensity left, LuminousIntensity right) => !left.Equals(right);
    public static LuminousIntensity operator +(LuminousIntensity left, LuminousIntensity right) => new(left.luminousIntensity + right.luminousIntensity);
    public static LuminousIntensity operator -(LuminousIntensity left, LuminousIntensity right) => new(left.luminousIntensity - right.luminousIntensity);
    public static LuminousIntensity operator *(Double scalar, LuminousIntensity right) => new(scalar * right.luminousIntensity);
    public static LuminousIntensity operator *(LuminousIntensity left, Double scalar) => new(scalar * left.luminousIntensity);
    public static LuminousIntensity operator /(LuminousIntensity left, Double scalar) => new(left.luminousIntensity / scalar);
    public static Double operator /(LuminousIntensity left, LuminousIntensity right) => left.luminousIntensity.Ratio(in right.luminousIntensity);
}
