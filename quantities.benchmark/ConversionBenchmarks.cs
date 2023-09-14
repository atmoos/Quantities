using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Quantities.Numerics;

namespace Quantities.Benchmark;

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

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                 | Mean     | Error     | StdDev    | Ratio |
|----------------------- |---------:|----------:|----------:|------:|
| EvaluateTrivial        | 2.629 ns | 0.0279 ns | 0.0261 ns |  1.00 |
| EvaluateSuccessive     | 1.597 ns | 0.0267 ns | 0.0237 ns |  0.61 |
| EvaluateCached         | 1.543 ns | 0.0148 ns | 0.0131 ns |  0.59 |
| EvaluateArithmetically | 2.057 ns | 0.0123 ns | 0.0115 ns |  0.78 |
*/
