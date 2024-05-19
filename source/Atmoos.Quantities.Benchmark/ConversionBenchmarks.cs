using System.Runtime.CompilerServices;
using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Benchmark;

public class ConversionBenchmarks
{
    private const Double argument = 3.43719;
    private static readonly Polynomial a = Polynomial.Of<A>();
    private static readonly Polynomial b = Polynomial.Of<B>();

    [Benchmark(Baseline = true)]
    public Double EvaluateTrivial() => Trivial(argument);

    [Benchmark]
    public Double EvaluateSuccessive()
    {
        var forward = a * argument;
        return b / forward;
    }

    [Benchmark]
    // There is only marginal benefit to be gained by cached conversion.
    public Double EvaluateCached() => Cache<A, B>.Convert(argument);

    [Benchmark]
    public Double EvaluateArithmetically() => a / b * argument;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private Double Trivial(Double value)
    {
        var forward = Math.PI * value / Math.E + Math.Tau;
        return Math.E * forward / Math.Tau + Math.PI;
    }
}

file readonly struct A : ITransform
{
    public static Transformation ToSi(Transformation self) => Math.PI * self / Math.E + Math.Tau;
}

file readonly struct B : ITransform
{
    public static Transformation ToSi(Transformation self) => Math.E * self / Math.Tau + Math.PI;
}

file static class Cache<TFrom, TTo>
    where TFrom : ITransform where TTo : ITransform
{
    private static readonly Polynomial polynomial = Polynomial.Of<TFrom>() / Polynomial.Of<TTo>();
    public static Double Convert(in Double value) => polynomial * value;
}

/* Summary

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method                 | Mean      | Error     | Ratio |
|----------------------- |----------:|----------:|------:|-
| EvaluateTrivial        | 2.4951 ns | 0.0268 ns |  1.00 |
| EvaluateSuccessive     | 0.8791 ns | 0.0064 ns |  0.35 |
| EvaluateCached         | 1.5942 ns | 0.0632 ns |  0.64 |
| EvaluateArithmetically | 0.2955 ns | 0.0082 ns |  0.12 |
*/
