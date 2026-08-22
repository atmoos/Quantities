using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Angle : IQuantity<Angle>, IAngle, IScalar<Angle, IAngle>
{
    private readonly Quantity angle;
    internal Quantity Value => this.angle;
    Quantity IQuantity<Angle>.Value => this.angle;

    private Angle(in Quantity value) => this.angle = value;

    public Angle To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IAngle, IUnit => new(other.Transform(in this.angle));

    public static Angle Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IAngle, IUnit => new(measure.Create(in value));

    static Angle IFactory<Angle>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Angle other) => this.angle.Equals(other.angle);

    public override Boolean Equals(Object? obj) => obj is Angle angle && Equals(angle);

    public override Int32 GetHashCode() => this.angle.GetHashCode();

    public override String ToString() => this.angle.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.angle.ToString(format, provider);
}
