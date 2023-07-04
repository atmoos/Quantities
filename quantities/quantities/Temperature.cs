using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Quantities.Creation;

namespace Quantities.Quantities;

public readonly struct Temperature : IQuantity<Temperature>, ITemperature
    , IFactory<Temperature>
    , IFactory<ICompoundFactory<Temperature, ITemperature>, Linear<To, Temperature, ITemperature>, Linear<Create, Temperature, ITemperature>>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<Temperature>.Value => this.quant;
    public Linear<To, Temperature, ITemperature> To => new(new To(in this.quant));
    private Temperature(in Quant quant) => this.quant = quant;
    public static Linear<Create, Temperature, ITemperature> Of(in Double value) => new(new Create(in value));
    static Temperature IFactory<Temperature>.Create(in Quant quant) => new(in quant);

    public Boolean Equals(Temperature other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Temperature temperature && Equals(temperature);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Temperature left, Temperature right) => left.Equals(right);
    public static Boolean operator !=(Temperature left, Temperature right) => !left.Equals(right);
    public static implicit operator Double(Temperature temperature) => temperature.quant.Value;
    public static Temperature operator +(Temperature left, Temperature right) => new(left.quant + right.quant);
    public static Temperature operator -(Temperature left, Temperature right) => new(left.quant - right.quant);
    public static Temperature operator *(Double scalar, Temperature right) => new(scalar * right.quant);
    public static Temperature operator *(Temperature left, Double scalar) => new(scalar * left.quant);
    public static Temperature operator /(Temperature left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Temperature left, Temperature right) => left.quant / right.quant;
}
