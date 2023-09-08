using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;

namespace Quantities.Quantities;

public readonly struct Temperature : IQuantity<Temperature>, ITemperature
    , IFactory<IDefaultFactory<Temperature, ITemperature>, Linear<To, Temperature, ITemperature>, Linear<Create, Temperature, ITemperature>>
{
    private readonly Quantity temperature;
    internal Quantity Value => this.temperature;
    Quantity IQuantity<Temperature>.Value => this.temperature;
    public Linear<To, Temperature, ITemperature> To => new(new To(in this.temperature));
    private Temperature(in Quantity value) => this.temperature = value;
    public static Linear<Create, Temperature, ITemperature> Of(in Double value) => new(new Create(in value));
    static Temperature IFactory<Temperature>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Temperature other) => this.temperature.Equals(other.temperature);
    public override Boolean Equals(Object? obj) => obj is Temperature temperature && Equals(temperature);
    public override Int32 GetHashCode() => this.temperature.GetHashCode();
    public override String ToString() => this.temperature.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.temperature.ToString(format, provider);

    public static Boolean operator ==(Temperature left, Temperature right) => left.Equals(right);
    public static Boolean operator !=(Temperature left, Temperature right) => !left.Equals(right);
    public static implicit operator Double(Temperature temperature) => temperature.temperature;
    public static Temperature operator +(Temperature left, Temperature right) => new(left.temperature + right.temperature);
    public static Temperature operator -(Temperature left, Temperature right) => new(left.temperature - right.temperature);
    public static Temperature operator *(Double scalar, Temperature right) => new(scalar * right.temperature);
    public static Temperature operator *(Temperature left, Double scalar) => new(scalar * left.temperature);
    public static Temperature operator /(Temperature left, Double scalar) => new(left.temperature / scalar);
    public static Double operator /(Temperature left, Temperature right) => left.temperature / right.temperature;
}
