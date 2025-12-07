using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

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
}
