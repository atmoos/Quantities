using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public readonly struct Torque : IQuantity<Torque>, ITorque, IProduct<Torque, ITorque, IForce, ILength>
{
    private readonly Quantity torque;
    internal Quantity Value => this.torque;
    Quantity IQuantity<Torque>.Value => this.torque;

    private Torque(in Quantity value) => this.torque = value;

    public Torque To<TTorque>(in Scalar<TTorque> other)
        where TTorque : ITorque, IUnit => new(other.Transform(in this.torque));

    public Torque To<TForce, TLength>(in Product<TForce, TLength> other)
        where TForce : IForce, IUnit
        where TLength : ILength, IUnit => new(other.Transform(in this.torque));

    public static Torque Of<TTorque>(in Double value, in Scalar<TTorque> measure)
        where TTorque : ITorque, IUnit => new(measure.Create(in value));

    public static Torque Of<TForce, TLength>(in Double value, in Product<TForce, TLength> measure)
        where TForce : IForce, IUnit
        where TLength : ILength, IUnit => new(measure.Create(in value));

    static Torque IFactory<Torque>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Torque other) => this.torque.Equals(other.torque);

    public override Boolean Equals(Object? obj) => obj is Torque torque && Equals(torque);

    public override Int32 GetHashCode() => this.torque.GetHashCode();

    public override String ToString() => this.torque.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.torque.ToString(format, provider);
}
