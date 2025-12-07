using System.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct DataRate : IQuantity<DataRate>, IInformationRate
    , IQuotient<DataRate, IInformationRate, IAmountOfInformation, ITime>
    , IMultiplyOperators<DataRate, Time, Data>
{
    private readonly Quantity dataRate;
    internal Quantity Value => this.dataRate;
    Quantity IQuantity<DataRate>.Value => this.dataRate;
    internal DataRate(in Quantity value) => this.dataRate = value;
    public DataRate To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IInformationRate, IUnit => new(other.Transform(in this.dataRate));
    public DataRate To<TNominator, TDenominator>(in Quotient<TNominator, TDenominator> other)
        where TNominator : IAmountOfInformation, IUnit
        where TDenominator : ITime, IUnit => new(other.Transform(in this.dataRate));
    public static DataRate Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IInformationRate, IUnit => new(measure.Create(in value));
    public static DataRate Of<TNominator, TDenominator>(in Double value, in Quotient<TNominator, TDenominator> measure)
        where TNominator : IAmountOfInformation, IUnit
        where TDenominator : ITime, IUnit => new(measure.Create(in value));
    static DataRate IFactory<DataRate>.Create(in Quantity value) => new(in value);
    internal static DataRate From(in Data data, in Time time) => new(data.Value / time.Value);
    public Boolean Equals(DataRate other) => this.dataRate.Equals(other.dataRate);
    public override Boolean Equals(Object? obj) => obj is DataRate rate && Equals(rate);
    public override Int32 GetHashCode() => this.dataRate.GetHashCode();
    public override String ToString() => this.dataRate.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.dataRate.ToString(format, provider);

    public static Data operator *(DataRate rate, Time time) => Data.From(in rate, in time);
}
