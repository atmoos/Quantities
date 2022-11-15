using System;
using Quantities.Prefixes;
using Quantities.Unit;

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

public readonly struct Trivial<TUnit>
    where TUnit : IUnit
{
    private readonly Double scale;
    private readonly Double value;
    public Prefix Prefix { get; }
    private Trivial(in Double value, in Prefix prefix)
    {
        this.value = value;
        this.Prefix = prefix;
        this.scale = Math.Pow(10, (Int32)prefix);
    }
    private Trivial(in Double value, in Double scale, Prefix prefix)
    {
        this.value = value;
        this.scale = scale;
        this.Prefix = prefix;
    }
    private Double ToSi() => this.scale * this.value;
    private Trivial<TUnit> FromSi(in Double value) => new(value / this.scale, in this.scale, this.Prefix);
    public static Trivial<TUnit> Unit(in Double value) => new(value, Prefix.Unit);
    public static Trivial<TUnit> Si(Prefix prefix, in Double value) => new(value, prefix);
    public static implicit operator Double(Trivial<TUnit> trivial) => trivial.value;
    public static Trivial<TUnit> operator +(Trivial<TUnit> left, Trivial<TUnit> right)
    {
        Double sum = left.ToSi() + right.ToSi();
        return left.FromSi(in sum);
    }
    public static Trivial<TUnit> operator -(Trivial<TUnit> left, Trivial<TUnit> right)
    {
        Double sum = left.ToSi() - right.ToSi();
        return left.FromSi(in sum);
    }
    public static Trivial<TUnit> operator *(Trivial<TUnit> left, Trivial<TUnit> right)
    {
        Prefix prefix = left.Prefix | right.Prefix;
        Double product = left.ToSi() * right.ToSi() / Math.Pow(10, (Int32)prefix);
        return new Trivial<TUnit>(in product, in prefix);
    }
}
