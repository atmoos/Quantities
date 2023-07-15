using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;

namespace Quantities.Quantities;

public readonly struct Mass : IQuantity<Mass>, IMass
    , IFactory<IDefaultFactory<Mass, IMass>, Linear<To, Mass, IMass>, Linear<Create, Mass, IMass>>
{
    private readonly Quant quant;
    Quant IQuantity<Mass>.Value => this.quant;
    public Linear<To, Mass, IMass> To => new(new To(in this.quant));
    private Mass(in Quant quant) => this.quant = quant;
    public static Linear<Create, Mass, IMass> Of(in Double value) => new(new Create(in value));
    static Mass IFactory<Mass>.Create(in Quant quant) => new(in quant);

    public Boolean Equals(Mass other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Mass mass && Equals(mass);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Mass left, Mass right) => left.Equals(right);
    public static Boolean operator !=(Mass left, Mass right) => !left.Equals(right);
    public static implicit operator Double(Mass mass) => mass.quant.Value;
    public static Mass operator +(Mass left, Mass right) => new(left.quant + right.quant);
    public static Mass operator -(Mass left, Mass right) => new(left.quant - right.quant);
    public static Mass operator *(Double scalar, Mass right) => new(scalar * right.quant);
    public static Mass operator *(Mass left, Double scalar) => new(scalar * left.quant);
    public static Mass operator /(Mass left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Mass left, Mass right) => left.quant / right.quant;
}
