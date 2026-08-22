using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public readonly struct DynamicViscosity : IQuantity<DynamicViscosity>, IDynamicViscosity, IProduct<DynamicViscosity, IDynamicViscosity, IPressure, ITime>
{
    private readonly Quantity dynamicViscosity;
    internal Quantity Value => this.dynamicViscosity;
    Quantity IQuantity<DynamicViscosity>.Value => this.dynamicViscosity;

    private DynamicViscosity(in Quantity value) => this.dynamicViscosity = value;

    public DynamicViscosity To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IDynamicViscosity, IUnit => new(other.Transform(in this.dynamicViscosity));

    public DynamicViscosity To<TPressure, TTime>(in Product<TPressure, TTime> other)
        where TPressure : IPressure, IUnit
        where TTime : ITime, IUnit => new(other.Transform(in this.dynamicViscosity));

    public static DynamicViscosity Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IDynamicViscosity, IUnit => new(measure.Create(in value));

    public static DynamicViscosity Of<TPressure, TTime>(in Double value, in Product<TPressure, TTime> measure)
        where TPressure : IPressure, IUnit
        where TTime : ITime, IUnit => new(measure.Create(in value));

    static DynamicViscosity IFactory<DynamicViscosity>.Create(in Quantity value) => new(in value);

    public Boolean Equals(DynamicViscosity other) => this.dynamicViscosity.Equals(other.dynamicViscosity);

    public override Boolean Equals(Object? obj) => obj is DynamicViscosity dynamicViscosity && Equals(dynamicViscosity);

    public override Int32 GetHashCode() => this.dynamicViscosity.GetHashCode();

    public override String ToString() => this.dynamicViscosity.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.dynamicViscosity.ToString(format, provider);
}
