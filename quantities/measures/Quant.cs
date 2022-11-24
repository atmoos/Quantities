using System.Globalization;
using System.Numerics;
using Quantities.Measures.Transformations;

namespace Quantities.Measures;

internal readonly struct Quant : IEquatable<Quant>, IFormattable
    , IAdditionOperators<Quant, Quant, Quant>
    , ISubtractionOperators<Quant, Quant, Quant>
    , IMultiplyOperators<Quant, Double, Quant>
    , IDivisionOperators<Quant, Double, Quant>
    , IDivisionOperators<Quant, Quant, Double>
{
    private static readonly ICreate<ICreate<Quant>> division = new Divide();
    private readonly Map map;
    private readonly Double value;
    public Double Value => this.value;
    internal Quant(in Double value, in Map map)
    {
        this.map = map;
        this.value = value;
    }
    public Double ToSi() => this.map.ToSi(in this.value);
    private Double Project(in Quant other) => ReferenceEquals(this.map, other.map)
        ? other.value : this.map.FromSi(other.map.ToSi(in other.value));
    public T Transform<T>(in ICreate<T> transformation) => this.map.Injector.Inject(in transformation, in this.value);
    public Quant PseudoMultiply(in Quant right)
    {
        var projected = Project(in right);
        return new(this.value * projected, in this.map);
    }
    public Quant PseudoDivide(in Quant denominator)
    {
        var projected = Project(in denominator);
        return new(this.value / projected, in this.map);
    }
    public Double SiMultiply(in Quant right) => this.map.ToSi(in this.value) * right.map.ToSi(in right.value);
    public Double SiDivide(in Quant right) => this.map.ToSi(in this.value) / right.map.ToSi(in right.value);
    public Quant Divide(in Quant right)
    {
        var nominator = this.map.Injector.Inject(in division, in this.value);
        return right.map.Injector.Inject(in nominator, in right.value);
    }

    public Boolean Equals(Quant other)
    {
        const Double min = 1d - 2e-15;
        const Double max = 1d + 2e-15;
        Double quotient = this / other;
        return quotient is >= min and <= max;
    }

    public override Boolean Equals(Object? obj)
    {
        if (obj is Quant quant) {
            return Equals(quant);
        }
        return false;
    }

    public override Int32 GetHashCode() => this.value.GetHashCode() ^ this.map.GetHashCode();
    public override String ToString() => ToString("g5", CultureInfo.CurrentCulture);
    public String ToString(String? format, IFormatProvider? provider) => $"{this.value.ToString(format, provider)} {this.map.Representation}";
    public static Boolean operator ==(Quant left, Quant right) => left.Equals(right);
    public static Boolean operator !=(Quant left, Quant right) => !left.Equals(right);
    public static Quant operator +(Quant left, Quant right)
    {
        var rightValue = left.Project(in right);
        return new(left.value + rightValue, in left.map);
    }
    public static Quant operator -(Quant left, Quant right)
    {
        var rightValue = left.Project(in right);
        return new(left.value - rightValue, in left.map);
    }
    public static Quant operator *(Double scalar, Quant right)
    {
        return new(scalar * right.value, in right.map);
    }
    public static Quant operator *(Quant left, Double scalar)
    {
        return new(scalar * left.value, in left.map);
    }
    public static Quant operator /(Quant left, Double scalar)
    {
        return new(left.value / scalar, in left.map);
    }
    public static Double operator /(Quant left, Quant right)
    {
        var rightValue = left.Project(in right);
        return left.Value / rightValue;
    }
}
