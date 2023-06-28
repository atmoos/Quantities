using System;
using System.Runtime.CompilerServices;
using Quantities.Units;

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
        Double sum = Add(left.ToSi(), right.ToSi());
        return left.FromSi(in sum);
    }
    public static Trivial<TUnit> operator -(Trivial<TUnit> left, Trivial<TUnit> right)
    {
        Double sum = Subtract(left.ToSi(), right.ToSi());
        return left.FromSi(in sum);
    }
    public static Trivial<TUnit> operator *(Trivial<TUnit> left, Trivial<TUnit> right)
    {
        Prefix prefix = left.Prefix | right.Prefix;
        Double product = left.ToSi() * right.ToSi() / Math.Pow(10, (Int32)prefix);
        return new Trivial<TUnit>(in product, in prefix);
    }
    public static Trivial<TUnit> operator /(Trivial<TUnit> left, Trivial<TUnit> right)
    {
        Prefix prefix = left.Prefix & ~right.Prefix;
        Double product = Math.Pow(10, (Int32)prefix) * left.ToSi() / right.ToSi();
        return new Trivial<TUnit>(in product, in prefix);
    }

    // Use these functions to simulate at least one call to a static function that has not been inlined.
    // Otherwise, the addition and subtraction operators of this trivial impl. may get inlined...
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Double Add(Double left, Double right) => left + right;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Double Subtract(Double left, Double right) => left - right;
}
