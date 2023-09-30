using Quantities.Dimensions;
using Quantities.Factories;

namespace Quantities;

public readonly struct Mass : IQuantity<Mass>, IMass
    , IFactory<IDefaultFactory<Mass, IMass>, Linear<To, Mass, IMass>, Linear<Create, Mass, IMass>>
{
    private readonly Quantity mass;
    Quantity IQuantity<Mass>.Value => this.mass;
    public Linear<To, Mass, IMass> To => new(new To(in this.mass));
    private Mass(in Quantity value) => this.mass = value;
    public static Linear<Create, Mass, IMass> Of(in Double value) => new(new Create(in value));
    static Mass IFactory<Mass>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Mass other) => this.mass.Equals(other.mass);
    public override Boolean Equals(Object? obj) => obj is Mass mass && Equals(mass);
    public override Int32 GetHashCode() => this.mass.GetHashCode();
    public override String ToString() => this.mass.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.mass.ToString(format, provider);

    public static implicit operator Double(Mass mass) => mass.mass;
    public static Boolean operator ==(Mass left, Mass right) => left.Equals(right);
    public static Boolean operator !=(Mass left, Mass right) => !left.Equals(right);
    public static Mass operator +(Mass left, Mass right) => new(left.mass + right.mass);
    public static Mass operator -(Mass left, Mass right) => new(left.mass - right.mass);
    public static Mass operator *(Double scalar, Mass right) => new(scalar * right.mass);
    public static Mass operator *(Mass left, Double scalar) => new(scalar * left.mass);
    public static Mass operator /(Mass left, Double scalar) => new(left.mass / scalar);
    public static Double operator /(Mass left, Mass right) => left.mass.Divide(in right.mass);
}
