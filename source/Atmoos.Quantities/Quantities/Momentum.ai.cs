using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Momentum : IQuantity<Momentum>, IMomentum, IProduct<Momentum, IMomentum, IMass, IVelocity>
{
    private readonly Quantity momentum;
    internal Quantity Value => this.momentum;
    Quantity IQuantity<Momentum>.Value => this.momentum;

    private Momentum(in Quantity value) => this.momentum = value;

    public Momentum To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IMomentum, IUnit => new(other.Transform(in this.momentum));

    public Momentum To<TMass, TVelocity>(in Product<TMass, TVelocity> other)
        where TMass : IMass, IUnit
        where TVelocity : IVelocity, IUnit => new(other.Transform(in this.momentum));

    public static Momentum Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IMomentum, IUnit => new(measure.Create(in value));

    public static Momentum Of<TMass, TVelocity>(in Double value, in Product<TMass, TVelocity> measure)
        where TMass : IMass, IUnit
        where TVelocity : IVelocity, IUnit => new(measure.Create(in value));

    static Momentum IFactory<Momentum>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Momentum other) => this.momentum.Equals(other.momentum);

    public override Boolean Equals(Object? obj) => obj is Momentum momentum && Equals(momentum);

    public override Int32 GetHashCode() => this.momentum.GetHashCode();

    public override String ToString() => this.momentum.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.momentum.ToString(format, provider);
}
