using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Mass : IQuantity<Mass>, IMass, IScalar<Mass, IMass>
{
    private readonly Quantity mass;
    Quantity IQuantity<Mass>.Value => this.mass;

    private Mass(in Quantity value) => this.mass = value;

    public Mass To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IMass, IUnit => new(other.Transform(in this.mass));

    public static Mass Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IMass, IUnit => new(measure.Create(in value));

    static Mass IFactory<Mass>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Mass other) => this.mass.Equals(other.mass);

    public override Boolean Equals(Object? obj) => obj is Mass mass && Equals(mass);

    public override Int32 GetHashCode() => this.mass.GetHashCode();

    public override String ToString() => this.mass.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.mass.ToString(format, provider);
}
