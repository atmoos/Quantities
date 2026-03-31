using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Impulse : IQuantity<Impulse>, IImpulse, IProduct<Impulse, IImpulse, IForce, ITime>
{
    private readonly Quantity impulse;
    internal Quantity Value => this.impulse;
    Quantity IQuantity<Impulse>.Value => this.impulse;

    private Impulse(in Quantity value) => this.impulse = value;

    public Impulse To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IImpulse, IUnit => new(other.Transform(in this.impulse));

    public Impulse To<TForce, TTime>(in Product<TForce, TTime> other)
        where TForce : IForce, IUnit
        where TTime : ITime, IUnit => new(other.Transform(in this.impulse));

    public static Impulse Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IImpulse, IUnit => new(measure.Create(in value));

    public static Impulse Of<TForce, TTime>(in Double value, in Product<TForce, TTime> measure)
        where TForce : IForce, IUnit
        where TTime : ITime, IUnit => new(measure.Create(in value));

    static Impulse IFactory<Impulse>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Impulse other) => this.impulse.Equals(other.impulse);

    public override Boolean Equals(Object? obj) => obj is Impulse impulse && Equals(impulse);

    public override Int32 GetHashCode() => this.impulse.GetHashCode();

    public override String ToString() => this.impulse.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.impulse.ToString(format, provider);
}
