using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;

namespace Quantities.Quantities;

/* This one is complicated..
- https://en.wikipedia.org/wiki/Units_of_information
- https://en.wikipedia.org/wiki/Bit
- https://en.wikipedia.org/wiki/Byte#History
*/
/* ToDo: Find better naming:
- AmountOfInformation
- DataSize
- AmountOfData
- Information
*/
public readonly struct Data : IQuantity<Data>, IAmountOfInformation
    , IFactory<IMetricFactory<Data, IAmountOfInformation>, Data.Factory<To>, Data.Factory<Create>>
    , IDivisionOperators<Data, Time, DataRate>
{
    private static readonly Polynomial bytes = Polynomial.Of<Units.Si.Metric.Byte>();
    private static readonly IRoot root = new MetricRoot<Units.Si.Metric.Byte>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    public Factory<To> To => new(new To(in this.quant));
    private Data(in Quant quant) => this.quant = quant;
    Quant IQuantity<Data>.Value => this.quant;
    public static Factory<Create> Of(in Double value) => new(new Create(in value));
    static Data IFactory<Data>.Create(in Quant quant) => new(in quant);
    internal static Data From(in Time time, in DataRate rate)
    {
        // ToDo: Recover data units from data rate
        Double asBytes = bytes.Inverse(time.Quant.SiMultiply(rate.Quant));
        return new(BinaryPrefix.Scale(in asBytes, root));
    }

    public Boolean Equals(Data other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Data data && Equals(data);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Data left, Data right) => left.Equals(right);
    public static Boolean operator !=(Data left, Data right) => !left.Equals(right);
    public static implicit operator Double(Data data) => data.quant.Value;
    public static Data operator +(Data left, Data right) => new(left.quant + right.quant);
    public static Data operator -(Data left, Data right) => new(left.quant - right.quant);
    public static Data operator *(Double scalar, Data right) => new(scalar * right.quant);
    public static Data operator *(Data left, Double scalar) => new(scalar * left.quant);
    public static Data operator /(Data left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Data left, Data right) => left.quant / right.quant;

    public static DataRate operator /(Data left, Time right) => DataRate.From(in left, in right);

    public readonly struct Factory<TCreate> : IBinaryFactory<Data, IAmountOfInformation>, IMetricFactory<Data, IAmountOfInformation>
        where TCreate : struct, ICreate
    {
        private readonly TCreate creator;
        internal Factory(in TCreate creator) => this.creator = creator;
        public Data Binary<TPrefix, TUnit>()
            where TPrefix : IBinaryPrefix
            where TUnit : IMetricUnit, IAmountOfInformation => new(this.creator.Create<Metric<TPrefix, TUnit>>());
        public Data Metric<TUnit>() where TUnit : IMetricUnit, IAmountOfInformation => new(this.creator.Create<Metric<TUnit>>());
        public Data Metric<TPrefix, TUnit>()
            where TPrefix : IMetricPrefix
            where TUnit : IMetricUnit, IAmountOfInformation => new(this.creator.Create<Metric<TPrefix, TUnit>>());
    }
}
