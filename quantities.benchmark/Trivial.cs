using System.Runtime.CompilerServices;
using Quantities.Units.Imperial;
using Quantities.Units.Si;

namespace Quantities.Benchmark;

[Flags]
public enum Prefix
{
    Mega = 6,
    Kilo = 3,
    Hecto = 2,
    Deca = 1,
    Unit = 0,
    Deci = -1,
    Centi = -2,
    Milli = -3,
    Micro = -6,
}

public interface ITrivialQuantity<TSelf> : ICastOperators<TSelf, Double>
    where TSelf : ITrivialQuantity<TSelf>
{
    Double ToSi();
    TSelf FromSi(in Double value);
}

public static class TrivialConvenience
{
    public static TOther To<TSelf, TOther>(this TSelf self, TOther other)
        where TSelf : ITrivialQuantity<TSelf>
        where TOther : ITrivialQuantity<TOther> => other.FromSi(self.ToSi());
}

public readonly struct Si<TUnit> : ITrivialQuantity<Si<TUnit>>
    where TUnit : ISiUnit
{
    private readonly Double value, scale;
    public Prefix Prefix { get; }
    private Si(in Double value, in Prefix prefix)
    {
        this.value = value;
        this.Prefix = prefix;
        this.scale = Math.Pow(10, (Int32)prefix);
    }
    private Si(in Double value, in Double scale, Prefix prefix)
    {
        this.value = value;
        this.scale = scale;
        this.Prefix = prefix;
    }
    public Double ToSi() => this.scale * this.value;
    public Si<TUnit> FromSi(in Double value) => new(value / this.scale, in this.scale, this.Prefix);
    public static Si<TUnit> Unit(in Double value) => new(value, Prefix.Unit);
    public static Si<TUnit> Of(Prefix prefix, in Double value) => new(value, prefix);
    public static implicit operator Double(Si<TUnit> trivial) => trivial.value;
    public static Si<TUnit> operator +(Si<TUnit> left, Si<TUnit> right)
    {
        Double sum = Add(left.ToSi(), right.ToSi());
        return left.FromSi(in sum);
    }
    public static Si<TUnit> operator -(Si<TUnit> left, Si<TUnit> right)
    {
        Double sum = Subtract(left.ToSi(), right.ToSi());
        return left.FromSi(in sum);
    }
    public static Si<TUnit> operator *(Si<TUnit> left, Si<TUnit> right)
    {
        Prefix prefix = left.Prefix | right.Prefix;
        Double product = left.ToSi() * right.ToSi() / Math.Pow(10, (Int32)prefix);
        return new Si<TUnit>(in product, in prefix);
    }
    public static Si<TUnit> operator /(Si<TUnit> left, Si<TUnit> right)
    {
        Prefix prefix = left.Prefix & ~right.Prefix;
        Double product = Math.Pow(10, (Int32)prefix) * left.ToSi() / right.ToSi();
        return new Si<TUnit>(in product, in prefix);
    }

    // Use these functions to simulate at least one call to a static function that has not been inlined.
    // Otherwise, the addition and subtraction operators of this trivial impl. may get inlined...
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Double Add(Double left, Double right) => left + right;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Double Subtract(Double left, Double right) => left - right;
}

public readonly struct Imperial<TUnit> : ITrivialQuantity<Imperial<TUnit>>
    where TUnit : IImperialUnit
{
    private readonly Double value, scale;

    public Imperial(in Double value, in Double scaleToSi)
    {
        this.value = value;
        this.scale = scaleToSi;
    }
    public Double ToSi() => this.scale * this.value;
    public Imperial<TUnit> FromSi(in Double value) => new(value / this.scale, in this.scale);
    public static implicit operator Double(Imperial<TUnit> self) => self.value;
}
