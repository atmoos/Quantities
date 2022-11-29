using System.Numerics;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Builders;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct DataRate : IQuantity<DataRate>, IInformationRate
    , IMultiplyOperators<DataRate, Time, Data>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    internal DataRate(in Quant quant) => this.quant = quant;
    public IBuilder<DataRate> ToMetric<TUnit>() where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new Transform<DataRate, Metric<TUnit>>(in this.quant);
    }
    public IBuilder<DataRate> ToMetric<TPrefix, TUnit>()
        where TPrefix : IPrefix, IScaleUp// Metric & Binary Prefixes are ok!
        where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new Transform<DataRate, Metric<TPrefix, TUnit>>(in this.quant);
    }
    public static IBuilder<DataRate> Metric<TUnit>(in Double value) where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new Builder<DataRate, Metric<TUnit>>(in value);
    }
    public static IBuilder<DataRate> Metric<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix, IScaleUp // Metric & Binary Prefixes are ok!
        where TUnit : IMetricUnit, IAmountOfInformation
    {
        return new Builder<DataRate, Metric<TPrefix, TUnit>>(in value);
    }

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
}
