using System.Globalization;
using System.Numerics;

namespace Quantities.Measures;

internal readonly struct Quantity : IEquatable<Quantity>, IFormattable
    , IMeasureEquality<Quantity>
    , IAdditionOperators<Quantity, Quantity, Quantity>
    , ISubtractionOperators<Quantity, Quantity, Quantity>
    , IMultiplyOperators<Quantity, Double, Quantity>
    , IMultiplyOperators<Quantity, Quantity, Quantity>
    , IDivisionOperators<Quantity, Quantity, Quantity>
    , IDivisionOperators<Quantity, Double, Quantity>
    , ICastOperators<Quantity, Double>
{
    private readonly Double value;
    private readonly Measure measure;
    private Quantity(in Double value, in Measure map) => (this.measure, this.value) = (map, value);
    private Double Project(in Quantity other) => ReferenceEquals(this.measure, other.measure)
        ? other.value : other.measure.Project(this.measure) * other.value;
    public Quantity Project(in Measure other) => ReferenceEquals(this.measure, other)
        ? this : new Quantity(this.measure.Project(other) * this.value, in other);
    public Double Divide(in Quantity right)
    {
        var rightValue = Project(in right);
        return this.value / rightValue;
    }
    public void Write(IWriter writer)
    {
        writer.Write("value", this.value);
        this.measure.Serialize(writer);
    }
    public Boolean Equals(Quantity other)
    {
        const Double min = 1d - 2e-15;
        const Double max = 1d + 2e-15;
        Double quotient = Divide(in other);
        return quotient is >= min and <= max;
    }
    public static Quantity Of<TMeasure>(in Double value)
        where TMeasure : IMeasure => new(in value, Measure.Of<TMeasure>());
    public static Quantity Of<TMeasure, TInjector>(in Double value)
    where TMeasure : IMeasure where TInjector : IInjector => new(in value, Measure.Of<TMeasure, TInjector>());
    public Boolean HasSameMeasure(in Quantity other) => ReferenceEquals(this.measure, other.measure);
    public override Boolean Equals(Object? obj) => obj is Quantity value && Equals(value);
    public override Int32 GetHashCode() => this.value.GetHashCode() ^ this.measure.GetHashCode();
    public override String ToString() => ToString("g5", CultureInfo.CurrentCulture);
    public String ToString(String? format, IFormatProvider? provider) => $"{this.value.ToString(format, provider)} {this.measure}";

    public static Boolean operator ==(Quantity left, Quantity right) => left.Equals(right);
    public static Boolean operator !=(Quantity left, Quantity right) => !left.Equals(right);

    public static Quantity operator *(Quantity left, Quantity right)
    {
        Result product = left.measure.Multiply(right.measure);
        return new(product * left.value * right.value, product);
    }
    public static Quantity operator /(Quantity left, Quantity right)
    {
        Result quotient = left.measure.Divide(right.measure);
        return new(quotient * left.value / right.value, quotient);
    }
    public static Quantity operator *(Double scalar, Quantity right) => new(scalar * right.value, in right.measure);
    public static Quantity operator *(Quantity left, Double scalar) => new(scalar * left.value, in left.measure);
    public static Quantity operator /(Quantity left, Double scalar) => new(left.value / scalar, in left.measure);
    public static Quantity operator +(Quantity left, Quantity right)
    {
        var rightValue = left.Project(in right);
        return new(left.value + rightValue, in left.measure);
    }
    public static Quantity operator -(Quantity left, Quantity right)
    {
        var rightValue = left.Project(in right);
        return new(left.value - rightValue, in left.measure);
    }
    public static implicit operator Double(Quantity self) => self.value;
}
