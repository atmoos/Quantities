using System.Numerics;
using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

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
    , IScalar<Data, IAmountOfInformation>
    , IDivisionOperators<Data, Time, DataRate>
{
    private readonly Quantity data;
    internal Quantity Value => this.data;
    Quantity IQuantity<Data>.Value => this.data;
    public Data To<TDim>(in Scalar<TDim> other)
        where TDim : IAmountOfInformation, IUnit => new(other.Transform(in this.data));
    private Data(in Quantity value) => this.data = value;
    public static Data Of<TDim>(in Double value, in Scalar<TDim> measure)
        where TDim : IAmountOfInformation, IUnit => new(measure.Create(in value));
    static Data IFactory<Data>.Create(in Quantity value) => new(in value);
    internal static Data From(in Time time, in DataRate rate) => new(time.Value * rate.Value);
    internal static Data From(in DataRate rate, in Time time) => new(rate.Value * time.Value);
    public Boolean Equals(Data other) => this.data.Equals(other.data);
    public override Boolean Equals(Object? obj) => obj is Data data && Equals(data);
    public override Int32 GetHashCode() => this.data.GetHashCode();
    public override String ToString() => this.data.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.data.ToString(format, provider);

    public static implicit operator Double(Data data) => data.data;
    public static Boolean operator ==(Data left, Data right) => left.Equals(right);
    public static Boolean operator !=(Data left, Data right) => !left.Equals(right);
    public static Data operator +(Data left, Data right) => new(left.data + right.data);
    public static Data operator -(Data left, Data right) => new(left.data - right.data);
    public static Data operator *(Double scalar, Data right) => new(scalar * right.data);
    public static Data operator *(Data left, Double scalar) => new(scalar * left.data);
    public static Data operator /(Data left, Double scalar) => new(left.data / scalar);
    public static Double operator /(Data left, Data right) => left.data.Divide(in right.data);

    public static DataRate operator /(Data data, Time time) => DataRate.From(in data, in time);
}
