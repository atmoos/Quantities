using System.Collections;
using System.Numerics;
using static Quantities.Dimensions.Convenience;

namespace Quantities.Dimensions;

internal abstract class Dim
    : IEquatable<Dim>
    , IEnumerable<Scalar>
    , IEqualityOperators<Dim, Dim, Boolean>
    , IMultiplyOperators<Dim, Dim, Dim>
    , IDivisionOperators<Dim, Dim, Dim>
{
    public abstract Int32 E { get; }
    protected Dim() { }
    public abstract Dim Pow(Int32 n);
    public abstract Dim Root(Int32 n);
    protected abstract Dim Multiply(Dim other);
    public Boolean Equals(Dim? other) => other is Dim d && E == d.E && Equal(d);
    public override Boolean Equals(Object? other) => Equals(other as Dim);
    public abstract override Int32 GetHashCode();
    protected abstract Boolean Equal(Dim other);
    public abstract IEnumerator<Scalar> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Dim operator /(Dim left, Dim right) => left * right.Pow(-1);
    public static Dim operator *(Dim left, Dim right) => left.Multiply(right);
    public static Boolean operator ==(Dim? left, Dim? right) => (left, right) switch {
        (null, null) => true,
        (null, _) => false,
        (_, null) => false,
        (Dim l, Dim r) => l.Equal(r)
    };
    public static Boolean operator !=(Dim? left, Dim? right) => !(left == right);
}

internal sealed class Unit : Dim
{
    private const String unitSymbol = "𝟙";
    public static Unit Identity { get; } = new Unit();
    private Unit() { }
    public override Int32 E => 0;
    public override Dim Pow(Int32 n) => this;
    public override Dim Root(Int32 n) => this;
    protected override Dim Multiply(Dim other) => other;
    protected override Boolean Equal(Dim other) => ReferenceEquals(this, other);
    public override Int32 GetHashCode() => unitSymbol.GetHashCode();
    public override IEnumerator<Scalar> GetEnumerator()
    {
        yield break;
    }
    public override String ToString() => unitSymbol;
}

internal abstract class Scalar : Dim
{
    private readonly Int32 m;
    public override Int32 E => this.m;
    protected Scalar(Int32 m) => this.m = m;
    public abstract Boolean Is(Dim other);
    public static Scalar Of<TDim>()
        where TDim : IDimension => new Impl<TDim>(1);
    private sealed class Impl<T> : Scalar
         where T : IDimension
    {
        public Impl(Int32 multiplicity) : base(multiplicity) { }
        protected override Dim Multiply(Dim other) => other switch {
            Unit => this,
            Impl<T> s => this.m + s.m == 0 ? Unit.Identity : new Impl<T>(this.m + s.m),
            Scalar s => new Product(this, s),
            _ => other * this
        };
        public override Dim Pow(Int32 n) => n switch {
            0 => Unit.Identity,
            1 => this,
            _ => new Impl<T>(this.m * n)
        };
        public override Dim Root(Int32 n) => n switch {
            0 => throw new DivideByZeroException("Can't take zeroth root."),
            1 => this,
            _ => new Impl<T>(this.m / n)
        };
        public override Boolean Is(Dim other) => other is Impl<T>;
        protected override Boolean Equal(Dim other) => other is Impl<T> s && E == s.E;
        public override IEnumerator<Scalar> GetEnumerator()
        {
            yield return this;
        }
        public override Int32 GetHashCode() => HashCode.Combine(typeof(Impl<T>), this.m);
        public override String ToString()
        {
            var scalar = typeof(T).Name[1..];
            return $"{scalar}{Exp(this.m)}";
        }
    }
}

internal sealed class Product : Dim
{
    private const String narrowSpace = "\u2006";
    private readonly Int32 e = 1;
    private readonly Dim left, right;
    public override Int32 E => this.e;
    public Dim L => this.left;
    public Dim R => this.right;
    public Product(Dim left, Dim right) : this(left, right, 1) { }
    private Product(Dim left, Dim right, Int32 e) => (this.left, this.right, this.e) = (left, right, e);
    public override Dim Pow(Int32 n) => n switch {
        0 => Unit.Identity,
        1 => this,
        _ => new Product(this.left, this.right, n * this.e)
    };
    public override Dim Root(Int32 n) => n switch {
        0 => throw new DivideByZeroException("Can't take zeroth root."),
        1 => this,
        _ => new Product(this.left, this.right, this.e / n)
    };
    protected override Boolean Equal(Dim other) => other is Product p && E == p.E && Same(p);
    protected override Dim Multiply(Dim other) => other switch {
        Unit => this,
        Scalar s when this.Any(t => t.Is(s)) => Simplify(this.Concat(s)),
        Dim p when this.Any(t => p.Any(pp => t.Is(pp))) => Simplify(this.Concat(p)),
        _ => new Product(this, other)
    };
    private Boolean Same(Product other)
        => this.left.Equals(other.left) && this.right.Equals(other.right)
        || this.left.Equals(other.right) && this.right.Equals(other.left);
    public override IEnumerator<Scalar> GetEnumerator()
        => WithMultiplicity(this.left, this.e).Concat(WithMultiplicity(this.right, this.e)).GetEnumerator();
    private static IEnumerable<Scalar> WithMultiplicity(Dim dim, Int32 e)
        => e == 1 ? dim : dim.Select(d => d.Pow(e)).SelectMany(l => l);
    public override String ToString()
    {
        var product = $"{this.left}{narrowSpace}{this.right}";
        return this.e == 1 ? product : $"[{product}]{Exp(this.e)}";
    }
    public override Int32 GetHashCode() => HashCode.Combine(this.left, this.right, this.e);
    public static Product SimplifyExponents(Dim l, Dim r)
    {
        var sign = l.E < 0 && r.E < 0 ? -1 : 1;
        var gcd = sign * GreatestCommonDivisor(l.E, r.E);
        return gcd == 1 ? new Product(l, r) : new Product(l.Root(gcd), r.Root(gcd), gcd);
    }
}
file static class Convenience
{
    public static String Exp(Int32 n)
    {
        var sign = n >= 0 ? String.Empty : "⁻";
        var value = n switch {
            1 => String.Empty,
            _ => Sup(Math.Abs(n))
        };
        return $"{sign}{value}";
        static String Sup(Int32 n) => n switch {
            1 => "\u00B9",
            2 or 3 => ((Char)(0x00B0 + n)).ToString(),
            var m when m is >= 0 and < 10 => ((Char)(0x2070 + m)).ToString(),
            _ => Build(n)
        };
        static String Build(Int32 n)
        {
            var digits = new List<String>();
            while (n >= 10) {
                (n, var digit) = Int32.DivRem(n, 10);
                digits.Add(Sup(digit));
            }
            digits.Add(Sup(n));
            digits.Reverse();
            return String.Join(String.Empty, digits);
        }
    }

    public static Int32 GreatestCommonDivisor(Int32 l, Int32 r)
    {
        var max = Int32.MaxMagnitude(l, r);
        var min = Int32.MinMagnitude(l, r);
        return EuclideanGcd(Int32.Abs(max), Int32.Abs(min));
        // https://en.wikipedia.org/wiki/Euclidean_algorithm
        static Int32 EuclideanGcd(Int32 max, Int32 min) => min == 0 ? max : EuclideanGcd(min, max % min);
    }
    public static Dim Simplify(IEnumerable<Scalar> values)
    {
        Dim? scalar;
        var result = new HashSet<Dim>();
        foreach (var item in values) {
            if ((scalar = result.SingleOrDefault(item.Is)) is null) {
                result.Add(item);
                continue;
            }
            result.Remove(scalar);
            var newItem = item * scalar;
            if (newItem.E != 0) {
                result.Add(newItem);
            }
        }
        return ReBuild(result.ToArray());
    }
    private static Dim ReBuild(ReadOnlySpan<Dim> values) => values.Length switch {
        0 => Unit.Identity,
        1 => values[0],
        2 => Product.SimplifyExponents(values[0], values[1]),
        var l => Product.SimplifyExponents(ReBuild(values[0..(l / 2)]), ReBuild(values[(l / 2)..]))
    };
}
