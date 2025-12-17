using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Power : IQuantity<Power>, IPower
    , IScalar<Power, IPower>
{
    private readonly Quantity power;
    internal Quantity Value => this.power;
    Quantity IQuantity<Power>.Value => this.power;
    private Power(in Quantity value) => this.power = value;
    public Power To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IPower, IUnit => new(other.Transform(in this.power));
    public static Power Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IPower, IUnit => new(measure.Create(in value));
    static Power IFactory<Power>.Create(in Quantity value) => new(in value);
    public Boolean Equals(Power other) => this.power.Equals(other.power);
    public override Boolean Equals(Object? obj) => obj is Power power && Equals(power);
    public override Int32 GetHashCode() => this.power.GetHashCode();
    public override String ToString() => this.power.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.power.ToString(format, provider);
}
