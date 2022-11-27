using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
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
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    private Data(in Quant quant) => this.quant = quant;
    public Data ToMetric<TUnit>() where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(this.quant.As<Metric<TUnit>>());
    }
    public Data ToMetric<TPrefix, TUnit>()
        where TPrefix : IPrefix, IScaleUp// Metric & Binary Prefixes are ok!
        where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(this.quant.As<Metric<TPrefix, TUnit>>());
    }
    public static Data Metric<TUnit>(in Double value) where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(value.As<Metric<TUnit>>());
    }
    public static Data Metric<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix, IScaleUp // Metric & Binary Prefixes are ok!
        where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new(value.As<Metric<TPrefix, TUnit>>());
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
}
