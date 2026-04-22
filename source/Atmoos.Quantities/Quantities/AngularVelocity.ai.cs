using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct AngularVelocity : IQuantity<AngularVelocity>, IAngularVelocity, IQuotient<AngularVelocity, IAngularVelocity, IAngle, ITime>
{
    private readonly Quantity angularVelocity;
    internal Quantity Value => this.angularVelocity;
    Quantity IQuantity<AngularVelocity>.Value => this.angularVelocity;

    internal AngularVelocity(in Quantity value) => this.angularVelocity = value;

    public AngularVelocity To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IAngularVelocity, IUnit => new(other.Transform(in this.angularVelocity));

    public AngularVelocity To<TAngle, TTime>(in Quotient<TAngle, TTime> other)
        where TAngle : IAngle, IUnit
        where TTime : ITime, IUnit => new(other.Transform(in this.angularVelocity));

    public static AngularVelocity Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IAngularVelocity, IUnit => new(measure.Create(in value));

    public static AngularVelocity Of<TAngle, TTime>(in Double value, in Quotient<TAngle, TTime> measure)
        where TAngle : IAngle, IUnit
        where TTime : ITime, IUnit => new(measure.Create(in value));

    static AngularVelocity IFactory<AngularVelocity>.Create(in Quantity value) => new(in value);

    public Boolean Equals(AngularVelocity other) => this.angularVelocity.Equals(other.angularVelocity);

    public override Boolean Equals(Object? obj) => obj is AngularVelocity angularVelocity && Equals(angularVelocity);

    public override Int32 GetHashCode() => this.angularVelocity.GetHashCode();

    public override String ToString() => this.angularVelocity.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.angularVelocity.ToString(format, provider);
}
