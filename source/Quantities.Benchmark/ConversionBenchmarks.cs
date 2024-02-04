using System.Runtime.CompilerServices;
using Quantities.Core.Numerics;

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

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux ARM
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method                 | Mean      | Error     | Ratio |
|----------------------- |----------:|----------:|------:|
| EvaluateTrivial        | 22.265 ns | 0.0010 ns |  1.00 |
| EvaluateSuccessive     |  9.437 ns | 0.0009 ns |  0.42 |
| EvaluateCached         |  3.335 ns | 0.0006 ns |  0.15 |
| EvaluateArithmetically |  1.272 ns | 0.0047 ns |  0.06 |
*/
