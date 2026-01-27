using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Frequency : IQuantity<Frequency>, IFrequency, IInvertible<Frequency, IFrequency, ITime>
{
    private readonly Quantity frequency;
    internal Quantity Value => this.frequency;
    Quantity IQuantity<Frequency>.Value => this.frequency;

    private Frequency(in Quantity value) => this.frequency = value;

    public Frequency To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : IFrequency, IInvertible<ITime>, IUnit => new(other.Transform(in this.frequency, static f => ref f.InverseOf<TUnit, ITime>()));

    public static Frequency Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : IFrequency, IInvertible<ITime>, IUnit => new(measure.Create(in value, static f => ref f.InverseOf<TUnit, ITime>()));

    static Frequency IFactory<Frequency>.Create(in Quantity value) => new(in value);

    public Boolean Equals(Frequency other) => this.frequency.Equals(other.frequency);

    public override Boolean Equals(Object? obj) => obj is Frequency frequency && Equals(frequency);

    public override Int32 GetHashCode() => this.frequency.GetHashCode();

    public override String ToString() => this.frequency.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.frequency.ToString(format, provider);
}
