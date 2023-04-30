using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
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
    , IDivisionOperators<Data, Time, DataRate>
{
    private static readonly IRoot root = new Creator();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Data(in Quant quant) => this.quant = quant;
    public Data To<TUnit>() where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(this.quant.As<Metric<TUnit>>());
    }
    public Data To<TPrefix, TUnit>()
        where TPrefix : IPrefix, IScaleUp// Metric & Binary Prefixes are ok!
        where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(this.quant.As<Metric<TPrefix, TUnit>>());
    }
    public static Data In<TUnit>(in Double value) where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(value.As<Metric<TUnit>>());
    }
    public static Data In<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix, IScaleUp // Metric & Binary Prefixes are ok!
        where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(value.As<Metric<TPrefix, TUnit>>());
    }

    internal static Data From(in Time time, in DataRate rate)
    {
        // ToDo: Recover data units from data rate
        Double bytes = Units.Si.Metric.Byte.FromSi(time.Quant.SiMultiply(rate.Quant));
        return new(BinaryPrefix.Scale(in bytes, root));
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

    private sealed class Creator : IRoot
    {
        public static Quant One { get; } = 1d.As<Metric<Units.Si.Metric.Byte>>();
        public static Quant Zero { get; } = 0d.As<Metric<Units.Si.Metric.Byte>>();

        // As bytes are way more common, use them to create data values by default.
        public Quant Identity(in Double value) => value.As<Metric<Units.Si.Metric.Byte>>();
        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value.As<Metric<TPrefix, Units.Si.Metric.Byte>>();
    }
}
