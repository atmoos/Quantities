using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Temperature : IQuantity<Temperature>, ITemperature
    , IScalar<Temperature, ITemperature>
{
    private readonly Quantity temperature;
    internal Quantity Value => this.temperature;
    Quantity IQuantity<Temperature>.Value => this.temperature;
    private Temperature(in Quantity value) => this.temperature = value;
    public Temperature To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : ITemperature, IUnit => new(other.Transform(in this.temperature));
    public static Temperature Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : ITemperature, IUnit => new(measure.Create(in value));
    static Temperature IFactory<Temperature>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Temperature other) => this.temperature.Equals(other.temperature);
    public override Boolean Equals(Object? obj) => obj is Temperature temperature && Equals(temperature);
    public override Int32 GetHashCode() => this.temperature.GetHashCode();
    public override String ToString() => this.temperature.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.temperature.ToString(format, provider);

    public static implicit operator Double(Temperature temperature) => temperature.temperature;
    public static Boolean operator ==(Temperature left, Temperature right) => left.Equals(right);
    public static Boolean operator !=(Temperature left, Temperature right) => !left.Equals(right);
    public static Temperature operator +(Temperature left, Temperature right) => new(left.temperature + right.temperature);
    public static Temperature operator -(Temperature left, Temperature right) => new(left.temperature - right.temperature);
    public static Temperature operator *(Double scalar, Temperature right) => new(scalar * right.temperature);
    public static Temperature operator *(Temperature left, Double scalar) => new(scalar * left.temperature);
    public static Temperature operator /(Temperature left, Double scalar) => new(left.temperature / scalar);
    public static Double operator /(Temperature left, Temperature right) => left.temperature.Ratio(in right.temperature);
}
