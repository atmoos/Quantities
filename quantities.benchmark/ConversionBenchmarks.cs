using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Quantities.Measures;
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
        var forward = a.Evaluate(argument);
        return b.Inverse(forward);
    }

    [Benchmark]
    public Double EvaluateCombined()
    {
        var poly = Conversion<B, A>.Polynomial;
        return poly.Evaluate(argument);
    }

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

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.110
  [Host]     : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2


|             Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |----------:|----------:|----------:|------:|--------:|
|    EvaluateTrivial |  2.629 ns | 0.0430 ns | 0.0402 ns |  1.00 |    0.00 |
| EvaluateSuccessive | 11.490 ns | 0.1667 ns | 0.1559 ns |  4.37 |    0.07 |
|   EvaluateCombined |  6.296 ns | 0.0314 ns | 0.0262 ns |  2.40 |    0.04 |
*/