using System.Numerics;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Frequency : IQuantity<Frequency>, IFrequency
    , IInvertible<Frequency, IFrequency, ITime>
    , IMultiplyOperators<Frequency, Time, Double>
{
    private readonly Quantity frequency;
    internal Quantity Value => this.frequency;
    Quantity IQuantity<Frequency>.Value => this.frequency;
    private Frequency(in Quantity value) => this.frequency = value;
    public Frequency To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IFrequency, IInvertible<ITime>, IUnit => new(other.Transform(in this.frequency, f => f.InverseOf<TUnit, ITime>()));
    public static Frequency Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IFrequency, IInvertible<ITime>, IUnit => new(measure.Create(in value, f => f.InverseOf<TUnit, ITime>()));
    static Frequency IFactory<Frequency>.Create(in Quantity value) => new(in value);
    internal static Frequency From(Double numerator, in Time denominator) => new(numerator / denominator.Value);
    public Boolean Equals(Frequency other) => this.frequency.Equals(other.frequency);
    public override Boolean Equals(Object? obj) => obj is Frequency frequency && Equals(frequency);
    public override Int32 GetHashCode() => this.frequency.GetHashCode();
    public override String ToString() => this.frequency.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.frequency.ToString(format, provider);

    public static implicit operator Double(Frequency frequency) => frequency.frequency;
    public static Boolean operator ==(Frequency left, Frequency right) => left.Equals(right);
    public static Boolean operator !=(Frequency left, Frequency right) => !left.Equals(right);
    public static Frequency operator +(Frequency left, Frequency right) => new(left.frequency + right.frequency);
    public static Frequency operator -(Frequency left, Frequency right) => new(left.frequency - right.frequency);
    public static Frequency operator *(Double scalar, Frequency right) => new(scalar * right.frequency);
    public static Frequency operator *(Frequency left, Double scalar) => new(scalar * left.frequency);
    public static Frequency operator /(Frequency left, Double scalar) => new(left.frequency / scalar);
    public static Double operator /(Frequency left, Frequency right) => left.frequency.Ratio(in right.frequency);

    public static Time operator /(Double scalar, Frequency frequency) => Time.From(scalar, in frequency);
    public static Double operator *(Frequency frequency, Time time) => frequency.frequency * time.Value;
}
