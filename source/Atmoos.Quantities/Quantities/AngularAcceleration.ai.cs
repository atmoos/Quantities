using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct AngularAcceleration : IQuantity<AngularAcceleration>, IAngularAcceleration, IQuotient<AngularAcceleration, IAngularAcceleration, IAngle, ITime, Two>
{
    private readonly Quantity angularAcceleration;
    internal Quantity Value => this.angularAcceleration;
    Quantity IQuantity<AngularAcceleration>.Value => this.angularAcceleration;

    internal AngularAcceleration(in Quantity value) => this.angularAcceleration = value;

    public AngularAcceleration To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IAngularAcceleration, IUnit => new(other.Transform(in this.angularAcceleration));

    public AngularAcceleration To<TAngle, TTime>(in Quotient<TAngle, Power<TTime, Two>> other)
        where TAngle : IAngle, IUnit
        where TTime : ITime, IUnit => new(other.Transform(in this.angularAcceleration));

    public static AngularAcceleration Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IAngularAcceleration, IUnit => new(measure.Create(in value));

    public static AngularAcceleration Of<TAngle, TTime>(in Double value, in Quotient<TAngle, Power<TTime, Two>> measure)
        where TAngle : IAngle, IUnit
        where TTime : ITime, IUnit => new(measure.Create(in value));

    static AngularAcceleration IFactory<AngularAcceleration>.Create(in Quantity value) => new(in value);

    public Boolean Equals(AngularAcceleration other) => this.angularAcceleration.Equals(other.angularAcceleration);

    public override Boolean Equals(Object? obj) => obj is AngularAcceleration angularAcceleration && Equals(angularAcceleration);

    public override Int32 GetHashCode() => this.angularAcceleration.GetHashCode();

    public override String ToString() => this.angularAcceleration.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.angularAcceleration.ToString(format, provider);
}
