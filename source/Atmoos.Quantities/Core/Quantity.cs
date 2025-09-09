using System.Globalization;
using System.Numerics;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;

namespace Atmoos.Quantities.Core;

internal readonly struct Quantity : IEquatable<Quantity>, IFormattable
    , IMeasureEquality<Quantity>
    , IComparisonOperators<Quantity, Quantity, Boolean>
    , IAdditionOperators<Quantity, Quantity, Quantity>
    , ISubtractionOperators<Quantity, Quantity, Quantity>
    , IUnaryNegationOperators<Quantity, Quantity>
    , IMultiplyOperators<Quantity, Double, Quantity>
    , IMultiplyOperators<Quantity, Quantity, Quantity>
    , IDivisionOperators<Quantity, Quantity, Quantity>
    , IDivisionOperators<Quantity, Double, Quantity>
    , ICastOperators<Quantity, Double>
{
    private readonly Double value;
    private readonly Measure measure;
    internal Quantity(in Double value, in Measure measure) => (this.measure, this.value) = (measure, value);
    public Quantity Project(in Measure other) => ReferenceEquals(this.measure, other)
        ? this : new Quantity(this.measure / (Polynomial)other * this.value, in other);
    private Double Project(in Quantity other) => ReferenceEquals(this.measure, other.measure)
        ? other.value : other.measure / (Polynomial)this.measure * other.value;
    public Double Ratio(in Quantity right)
    {
        Double rightValue = Project(in right);
        return this.value / rightValue;
    }
    public void Write(IWriter writer, String name)
    {
        writer.Write(nameof(this.value), this.value);
        writer.Write("quantity", name);
        this.measure.Serialize(writer);
    }
    public Boolean Equals(Quantity other)
    {
        const Double min = 1d - 2e-15;
        const Double max = 1d + 2e-15;
        Double projectedOther = Project(in other);
        if (projectedOther == 0d) {
            return this.value == 0d;
        }
        Double quotient = this.value / projectedOther;
        return quotient is >= min and <= max;
    }
    internal Boolean EqualsExactly(Quantity other) => this.HasSameMeasure(in other) && this.value == other.value;
    public static Quantity Of<TMeasure>(in Double value)
        where TMeasure : IMeasure => new(in value, in Measure.Of<TMeasure>());
    public Boolean HasSameMeasure(in Quantity other) => ReferenceEquals(this.measure, other.measure);
    public override Boolean Equals(Object? obj) => obj is Quantity value && Equals(value);
    public override Int32 GetHashCode() => HashCode.Combine(this.value, this.measure);
    public override String ToString() => ToString("g5", CultureInfo.CurrentCulture);
    public String ToString(String? format, IFormatProvider? provider) => $"{this.value.ToString(format, provider)} {this.measure}";

    public static Boolean operator ==(Quantity left, Quantity right) => left.Equals(right);
    public static Boolean operator !=(Quantity left, Quantity right) => !left.Equals(right);
    public static Boolean operator >(Quantity left, Quantity right) => left.value > left.Project(in right);
    public static Boolean operator >=(Quantity left, Quantity right) => left.value >= left.Project(in right);
    public static Boolean operator <(Quantity left, Quantity right) => left.value < left.Project(in right);
    public static Boolean operator <=(Quantity left, Quantity right) => left.value <= left.Project(in right);
    public static Quantity operator *(Quantity left, Quantity right)
    {
        Result product = left.measure * right.measure;
        return new(product * left.value * right.value, product);
    }
    public static Quantity operator /(Quantity left, Quantity right)
    {
        Result quotient = left.measure / right.measure;
        return new(quotient * left.value / right.value, quotient);
    }
    public static Quantity operator -(Quantity value) => new(-value.value, in value.measure);
    public static Quantity operator *(Double scalar, Quantity right) => new(scalar * right.value, in right.measure);
    public static Quantity operator *(Quantity left, Double scalar) => new(scalar * left.value, in left.measure);
    public static Quantity operator /(Quantity left, Double scalar) => new(left.value / scalar, in left.measure);
    public static Quantity operator /(Double scalar, Quantity right)
    {
        ref readonly Measure inverse = ref right.measure.Inverse;
        Polynomial conversion = right.measure * (Polynomial)inverse;
        return new(scalar / (conversion * right.value), in inverse);
    }
    public static Quantity operator +(Quantity left, Quantity right)
    {
        Double rightValue = left.Project(in right);
        return new(left.value + rightValue, in left.measure);
    }
    public static Quantity operator -(Quantity left, Quantity right)
    {
        Double rightValue = left.Project(in right);
        return new(left.value - rightValue, in left.measure);
    }
    public static implicit operator Double(Quantity self) => self.value;
}
