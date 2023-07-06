using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Creation;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct DataRate : IQuantity<DataRate>, IInformationRate
    , IFactory<DataRate>
    , IFactory<IPerFactory<IAmountOfInformation, ITime>, DataRate.Factory<To>, DataRate.Factory<Create>>
    , IMultiplyOperators<DataRate, Time, Data>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<DataRate>.Value => this.quant;
    public Factory<To> To => new(new To(in this.quant));
    internal DataRate(in Quant quant) => this.quant = quant;
    public static Factory<Create> Of(in Double value) => new(new Create(in value));
    static DataRate IFactory<DataRate>.Create(in Quant quant) => new(in quant);
    internal static DataRate From(in Data data, in Time time) => new(data.Quant.Divide(time.Quant));

    public Boolean Equals(DataRate other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is DataRate rate && Equals(rate);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(DataRate left, DataRate right) => left.Equals(right);
    public static Boolean operator !=(DataRate left, DataRate right) => !left.Equals(right);
    public static implicit operator Double(DataRate rate) => rate.quant.Value;
    public static DataRate operator +(DataRate left, DataRate right) => new(left.quant + right.quant);
    public static DataRate operator -(DataRate left, DataRate right) => new(left.quant - right.quant);
    public static DataRate operator *(Double scalar, DataRate right) => new(scalar * right.quant);
    public static DataRate operator *(DataRate left, Double scalar) => new(scalar * left.quant);
    public static DataRate operator /(DataRate left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(DataRate left, DataRate right) => left.quant / right.quant;

    public static Data operator *(DataRate left, Time right) => Data.From(in right, in left);

    public readonly struct Factory<TCreate> : IPerFactory<IAmountOfInformation, ITime>, IBinaryFactory<Denominator<TCreate, DataRate, ITime>, IAmountOfInformation>, IMetricFactory<Denominator<TCreate, DataRate, ITime>, IAmountOfInformation>
        where TCreate : struct, ICreate
    {
        private readonly TCreate create;
        internal Factory(in TCreate create) => this.create = create;
        public Denominator<TCreate, DataRate, ITime> Binary<TPrefix, TUnit>()
            where TPrefix : IBinaryPrefix
            where TUnit : IMetricUnit, IAmountOfInformation => new(in this.create, ZeroAllocation<PerInjector<TCreate, Metric<TPrefix, TUnit>>>.Item);
        public Denominator<TCreate, DataRate, ITime> Metric<TUnit>() where TUnit : IMetricUnit, IAmountOfInformation => new(in this.create, ZeroAllocation<PerInjector<TCreate, Metric<TUnit>>>.Item);
        public Denominator<TCreate, DataRate, ITime> Metric<TPrefix, TUnit>()
            where TPrefix : IMetricPrefix
            where TUnit : IMetricUnit, IAmountOfInformation => new(in this.create, ZeroAllocation<PerInjector<TCreate, Metric<TPrefix, TUnit>>>.Item);
    }
}
