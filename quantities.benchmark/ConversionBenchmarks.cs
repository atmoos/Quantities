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
        var forward = a.Evaluate(argument);
        return b.Inverse(forward);
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


|                 Method |      Mean |     Error |    StdDev | Ratio |
|----------------------- |----------:|----------:|----------:|------:|
|        EvaluateTrivial | 2.6057 ns | 0.0393 ns | 0.0368 ns |  1.00 |
|     EvaluateSuccessive | 1.5372 ns | 0.0171 ns | 0.0143 ns |  0.59 |
|       EvaluateCombined | 0.3344 ns | 0.0128 ns | 0.0119 ns |  0.13 |
| EvaluateArithmetically | 1.1948 ns | 0.0079 ns | 0.0074 ns |  0.46 |
*/