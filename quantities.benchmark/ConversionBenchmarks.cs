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
    public Double EvaluateCombined() => Polynomial.Convert<A, B>(argument);

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

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.110
  [Host]     : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2


|                 Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------- |---------:|----------:|----------:|------:|--------:|
|        EvaluateTrivial | 2.463 ns | 0.0393 ns | 0.0367 ns |  1.00 |    0.00 |
|     EvaluateSuccessive | 1.547 ns | 0.0280 ns | 0.0262 ns |  0.63 |    0.02 |
|       EvaluateCombined | 1.628 ns | 0.0114 ns | 0.0107 ns |  0.66 |    0.01 |
| EvaluateArithmetically | 1.741 ns | 0.0272 ns | 0.0255 ns |  0.71 |    0.01 |
*/