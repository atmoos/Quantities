using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities;

public readonly struct DataRate : IQuantity<DataRate>, IInformationRate
    , IFactory<IQuotientFactory<IInformationRate, IAmountOfInformation, ITime>, DataRate.Factory<To>, DataRate.Factory<Create>>
    , IMultiplyOperators<DataRate, Time, Data>
{
    private readonly Quantity dataRate;
    internal Quantity Value => this.dataRate;
    Quantity IQuantity<DataRate>.Value => this.dataRate;
    public Factory<To> To => new(new To(in this.dataRate));
    internal DataRate(in Quantity value) => this.dataRate = value;
    public static Factory<Create> Of(in Double value) => new(new Create(in value));
    static DataRate IFactory<DataRate>.Create(in Quantity value) => new(in value);
    internal static DataRate From(in Data data, in Time time) => new(data.Value / time.Value);
    public Boolean Equals(DataRate other) => this.dataRate.Equals(other.dataRate);
    public override Boolean Equals(Object? obj) => obj is DataRate rate && Equals(rate);
    public override Int32 GetHashCode() => this.dataRate.GetHashCode();
    public override String ToString() => this.dataRate.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.dataRate.ToString(format, provider);

    public static implicit operator Double(DataRate rate) => rate.dataRate;
    public static Boolean operator ==(DataRate left, DataRate right) => left.Equals(right);
    public static Boolean operator !=(DataRate left, DataRate right) => !left.Equals(right);
    public static DataRate operator +(DataRate left, DataRate right) => new(left.dataRate + right.dataRate);
    public static DataRate operator -(DataRate left, DataRate right) => new(left.dataRate - right.dataRate);
    public static DataRate operator *(Double scalar, DataRate right) => new(scalar * right.dataRate);
    public static DataRate operator *(DataRate left, Double scalar) => new(scalar * left.dataRate);
    public static DataRate operator /(DataRate left, Double scalar) => new(left.dataRate / scalar);
    public static Double operator /(DataRate left, DataRate right) => left.dataRate.Divide(in right.dataRate);

    public static Data operator *(DataRate left, Time right) => Data.From(in left, in right);

    public readonly struct Factory<TCreate> : IQuotientFactory<IInformationRate, IAmountOfInformation, ITime>, IBinaryFactory<Denominator<TCreate, DataRate, ITime>, IAmountOfInformation>, IMetricFactory<Denominator<TCreate, DataRate, ITime>, IAmountOfInformation>
        where TCreate : struct, ICreate
    {
        private readonly TCreate creator;
        internal Factory(in TCreate creator) => this.creator = creator;
        public Denominator<TCreate, DataRate, ITime> Binary<TPrefix, TUnit>()
            where TPrefix : IBinaryPrefix
            where TUnit : IMetricUnit, IAmountOfInformation => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Metric<TPrefix, TUnit>>>.Item);
        public Denominator<TCreate, DataRate, ITime> Metric<TUnit>() where TUnit : IMetricUnit, IAmountOfInformation => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Metric<TUnit>>>.Item);
        public Denominator<TCreate, DataRate, ITime> Metric<TPrefix, TUnit>()
            where TPrefix : IMetricPrefix
            where TUnit : IMetricUnit, IAmountOfInformation => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Metric<TPrefix, TUnit>>>.Item);
    }
}
