using System.Collections;
using System.Numerics;
using Atmoos.Quantities.Core.Numerics;
using static Atmoos.Quantities.Dimensions.Convenience;

namespace Atmoos.Quantities.Dimensions;

internal abstract class Dimension
    : IEquatable<Dimension>
    , IEnumerable<Scalar>
    , IEqualityOperators<Dimension, Dimension, Boolean>
    , IMultiplyOperators<Dimension, Dimension, Dimension>
    , IDivisionOperators<Dimension, Dimension, Dimension>
{
    public abstract Int32 E { get; }
    protected Dimension() { }
    public abstract Dimension Pow(Int32 n);
    public abstract Dimension Root(Int32 n);
    internal abstract Dimension Swap();
    protected abstract Dimension Multiply(Dimension other);
    public abstract Boolean CommonRoot(Dimension other);
    public Boolean Equals(Dimension? other) => other is Dimension d && E == d.E && Equal(d);
    public override Boolean Equals(Object? other) => Equals(other as Dimension);
    public abstract override Int32 GetHashCode();
    protected abstract Boolean Equal(Dimension other);
    public abstract IEnumerator<Scalar> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Dimension operator /(Dimension left, Dimension right) => left * right.Pow(-1);
    public static Dimension operator *(Dimension left, Dimension right) => left.Multiply(right);

    public static Boolean operator ==(Dimension? left, Dimension? right) => (left, right) switch {
        (null, null) => true,
        (null, _) or (_, null) => false,
        (Dimension l, Dimension r) => l.Equal(r)
    };
    public static Boolean operator !=(Dimension? left, Dimension? right) => !(left == right);
}

internal sealed class Unit : Dimension
{
    private const String unitSymbol = "𝟙";
    public static Unit Identity { get; } = new Unit();
    private Unit() { }
    public override Int32 E => 0;
    public override Dimension Pow(Int32 n) => this;
    public override Dimension Root(Int32 n) => this;
    internal override Dimension Swap() => this;
    protected override Dimension Multiply(Dimension other) => other;
    public override Boolean CommonRoot(Dimension other) => ReferenceEquals(other, this);
    protected override Boolean Equal(Dimension other) => ReferenceEquals(this, other);
    public override Int32 GetHashCode() => unitSymbol.GetHashCode();
    public override IEnumerator<Scalar> GetEnumerator()
    {
        yield break;
    }
    public override String ToString() => unitSymbol;
}

internal abstract class Scalar : Dimension
{
    private readonly Int32 m;
    public override Int32 E => this.m;
    protected Scalar(Int32 m) => this.m = m;
    internal override Dimension Swap() => this;
    public abstract Boolean Is(Dimension other);
    public override Boolean CommonRoot(Dimension other) => this.Is(other);
    public static Scalar Of<TDim>(Int32 multiplicity = 1)
        where TDim : IDimension => new Impl<TDim>(multiplicity);
    private sealed class Impl<T> : Scalar
         where T : IDimension
    {
        public Impl(Int32 multiplicity) : base(multiplicity) { }
        protected override Dimension Multiply(Dimension other) => other switch {
            Unit => this,
            Impl<T> s => this.m + s.m == 0 ? Unit.Identity : new Impl<T>(this.m + s.m),
            Scalar s => new Product(this, s),
            _ => (other * this).Swap()
        };
        public override Dimension Pow(Int32 n) => n switch {
            0 => Unit.Identity,
            1 => this,
            _ => new Impl<T>(this.m * n)
        };
        public override Dimension Root(Int32 n) => n switch {
            0 => throw new DivideByZeroException("Can't take zeroth root."),
            1 => this,
            _ => new Impl<T>(this.m / n)
        };
        public override Boolean Is(Dimension other) => other is Impl<T>;
        protected override Boolean Equal(Dimension other) => other is Impl<T> s && E == s.E;
        public override IEnumerator<Scalar> GetEnumerator()
        {
            yield return this;
        }
        public override Int32 GetHashCode() => HashCode.Combine(typeof(Impl<T>), this.m);
        public override String ToString()
        {
            var scalar = typeof(T).Name[1..];
            return $"{scalar}{Tools.ExpToString(this.m)}";
        }
    }
}

internal sealed class Product : Dimension
{
    private const String narrowSpace = "\u2006";
    private readonly Int32 e = 1;
    private readonly Dimension left, right;
    public override Int32 E => this.e;
    public Dimension L => this.left;
    public Dimension R => this.right;
    public Product(Dimension left, Dimension right) => (this.left, this.right, this.e) = (left, right, 1);
    private Product(Dimension left, Dimension right, Int32 e) => (this.left, this.right, this.e) = (left, right, e);
    public override Dimension Pow(Int32 n) => n switch {
        0 => Unit.Identity,
        1 => this,
        _ => new Product(this.left, this.right, n * this.e)
    };
    public override Dimension Root(Int32 n) => n switch {
        0 => throw new DivideByZeroException("Can't take zeroth root."),
        1 => this,
        _ => new Product(this.left, this.right, this.e / n)
    };
    internal override Dimension Swap() => new Product(this.right, this.left, this.e);
    protected override Boolean Equal(Dimension other) => other is Product p && E == p.E && Same(p);
    public override Boolean CommonRoot(Dimension other)
    {
        // Enumeration adds the multiplicities (by design).
        // As the common root only cares about the inner dimensions,
        // we need to take the root of the outer exponent to get the correct comparison.
        var these = this.Select(t => t.Root(this.E)).ToHashSet();
        return these.SetEquals(other.Select(o => o.Root(other.E)));
    }
    protected override Dimension Multiply(Dimension other) => other switch {
        Unit => this,
        Scalar s when this.Any(t => t.Is(s)) => Simplify(this.Concat(s)),
        Dimension p when this.Any(t => p.Any(pp => t.Is(pp))) => Simplify(this.Concat(p)),
        _ => new Product(this, other)
    };
    private Boolean Same(Product other) => this.ToHashSet().SetEquals(other);
    public override IEnumerator<Scalar> GetEnumerator()
        => WithMultiplicity(this.left, this.e).Concat(WithMultiplicity(this.right, this.e)).GetEnumerator();
    private static IEnumerable<Scalar> WithMultiplicity(Dimension dim, Int32 e)
        => e == 1 ? dim : dim.Select(d => d.Pow(e)).SelectMany(l => l);
    public override String ToString()
    {
        var product = $"{this.left}{narrowSpace}{this.right}";
        return this.e == 1 ? product : $"[{product}]{Tools.ExpToString(this.e)}";
    }
    public override Int32 GetHashCode() => HashCode.Combine(this.left, this.right, this.e);
    public static Product SimplifyExponents(Dimension l, Dimension r)
    {
        var sign = l.E < 0 && r.E < 0 ? -1 : 1;
        var gcd = sign * Algorithms.Gcd(l.E, r.E);
        return gcd == 1 ? new Product(l, r) : new Product(l.Root(gcd), r.Root(gcd), gcd);
    }
}
file static class Convenience
{
    public static Dimension Simplify(IEnumerable<Scalar> values)
    {
        Dimension? scalar;
        var result = new HashSet<Dimension>();
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
    private static Dimension ReBuild(ReadOnlySpan<Dimension> values) => values.Length switch {
        0 => Unit.Identity,
        1 => values[0],
        2 => Product.SimplifyExponents(values[0], values[1]),
        var l => Product.SimplifyExponents(ReBuild(values[0..(l / 2)]), ReBuild(values[(l / 2)..]))
    };
}
