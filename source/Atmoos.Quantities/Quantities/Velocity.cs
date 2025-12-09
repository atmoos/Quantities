using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Velocity : IQuantity<Velocity>, IVelocity
    , IQuotient<Velocity, IVelocity, ILength, ITime>
{
    private readonly Quantity velocity;
    internal Quantity Value => this.velocity;
    Quantity IQuantity<Velocity>.Value => this.velocity;
    internal Velocity(in Quantity value) => this.velocity = value;
    public Velocity To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IVelocity, IUnit => new(other.Transform(in this.velocity));
    public Velocity To<TNominator, TDenominator>(in Quotient<TNominator, TDenominator> other)
        where TNominator : ILength, IUnit
        where TDenominator : ITime, IUnit => new(other.Transform(in this.velocity));
    public static Velocity Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IVelocity, IUnit => new(measure.Create(in value));
    public static Velocity Of<TLength, TTime>(in Double value, in Quotient<TLength, TTime> measure)
       where TLength : IUnit, ILength where TTime : IUnit, ITime => new(measure.Create(in value));
    static Velocity IFactory<Velocity>.Create(in Quantity value) => new(in value);
    internal static Velocity From(in Power power, in Force force) => new(power.Value / force.Value);
    internal static Velocity From(in Length length, in Time time) => new(length.Value / time.Value);
    internal static Velocity From(in Acceleration acceleration, in Time time) => new(acceleration.Value * time.Value);
    public Boolean Equals(Velocity other) => this.velocity.Equals(other.velocity);
    public override Boolean Equals(Object? obj) => obj is Velocity velocity && Equals(velocity);
    public override Int32 GetHashCode() => this.velocity.GetHashCode();
    public override String ToString() => this.velocity.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.velocity.ToString(format, provider);
}
