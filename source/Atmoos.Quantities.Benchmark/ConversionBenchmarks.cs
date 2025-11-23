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

BenchmarkDotNet v0.15.7, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method                 | Mean      | Error     | Ratio |
|----------------------- |----------:|----------:|------:|
| EvaluateTrivial        | 2.2553 ns | 0.0114 ns |  1.00 |
| EvaluateSuccessive     | 0.8516 ns | 0.0067 ns |  0.38 |
| EvaluateCached         | 1.2845 ns | 0.0072 ns |  0.57 |
| EvaluateArithmetically | 0.0991 ns | 0.0329 ns |  0.04 |
*/
