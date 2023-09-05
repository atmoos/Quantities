using System.Globalization;
using System.Numerics;
using Quantities.Measures.Transformations;

namespace Quantities.Measures;

internal readonly struct Quantity : IEquatable<Quantity>, IFormattable
    , IMeasureEquality<Quantity>
    , IAdditionOperators<Quantity, Quantity, Quantity>
    , ISubtractionOperators<Quantity, Quantity, Quantity>
    , IMultiplyOperators<Quantity, Double, Quantity>
    , IDivisionOperators<Quantity, Double, Quantity>
    , IDivisionOperators<Quantity, Quantity, Double>
    , ICastOperators<Quantity, Double>
{
    private static readonly IInject<IInject<Quantity>> division = new Divide();
    private static readonly IInject<IInject<Quantity>> multiplication = new Multiply();
    private readonly Map map;
    private readonly Double value;
    internal Quantity(in Double value, in Map map) => (this.map, this.value) = (map, value);
    private Double Project(in Quantity other) => ReferenceEquals(this.map, other.map)
        ? other.value : other.map.Project(in this.map, in other.value);
    public Quantity Project(in Map other) => ReferenceEquals(this.map, other)
        ? this : new Quantity(this.map.Project(in other, in this.value), in other);
    public T Transform<T>(in IInject<T> transformation) => this.map.Injector.Inject(in transformation, in this.value);
    public Quantity PseudoMultiply(in Quantity right)
    {
        var projected = Project(in right);
        return new(this.value * projected, in this.map);
    }
    public Quantity PseudoDivide(in Quantity denominator)
    {
        var projected = Project(in denominator);
        return new(this.value / projected, in this.map);
    }
    public Double SiMultiply(in Quantity right) => this.map.ToSi(in this.value) * right.map.ToSi(in right.value);
    public Double SiDivide(in Quantity right) => this.map.ToSi(in this.value) / right.map.ToSi(in right.value);
    public Quantity Divide(in Quantity right)
    {
        var nominator = this.map.Injector.Inject(in division, in this.value);
        return right.map.Injector.Inject(in nominator, in right.value);
    }
    public Quantity Multiply(in Quantity right)
    {
        var leftTerm = this.map.Injector.Inject(in multiplication, in this.value);
        return right.map.Injector.Inject(in leftTerm, in right.value);
    }
    public void Write(IWriter writer)
    {
        writer.Write("value", this.value);
        this.map.Serialize(writer);
    }
    public Boolean Equals(Quantity other)
    {
        const Double min = 1d - 2e-15;
        const Double max = 1d + 2e-15;
        Double quotient = this / other;
        return quotient is >= min and <= max;
    }
    public Boolean HasSameMeasure(in Quantity other) => ReferenceEquals(this.map, other.map);
    public override Boolean Equals(Object? obj) => obj is Quantity value && Equals(value);
    public override Int32 GetHashCode() => this.value.GetHashCode() ^ this.map.GetHashCode();
    public override String ToString() => ToString("g5", CultureInfo.CurrentCulture);
    public String ToString(String? format, IFormatProvider? provider) => $"{this.value.ToString(format, provider)} {this.map.Representation}";

    public static Boolean operator ==(Quantity left, Quantity right) => left.Equals(right);
    public static Boolean operator !=(Quantity left, Quantity right) => !left.Equals(right);
    public static Quantity operator +(Quantity left, Quantity right)
    {
        var rightValue = left.Project(in right);
        return new(left.value + rightValue, in left.map);
    }
    public static Quantity operator -(Quantity left, Quantity right)
    {
        var rightValue = left.Project(in right);
        return new(left.value - rightValue, in left.map);
    }
    public static Quantity operator *(Double scalar, Quantity right)
    {
        return new(scalar * right.value, in right.map);
    }
    public static Quantity operator *(Quantity left, Double scalar)
    {
        return new(scalar * left.value, in left.map);
    }
    public static Quantity operator /(Quantity left, Double scalar)
    {
        return new(left.value / scalar, in left.map);
    }
    public static Double operator /(Quantity left, Quantity right)
    {
        var rightValue = left.Project(in right);
        return left.value / rightValue;
    }
    public static implicit operator Double(Quantity self) => self.value;
}