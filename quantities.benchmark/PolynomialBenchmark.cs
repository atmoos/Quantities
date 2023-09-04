using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Quantities.Numerics;
using static Quantities.Benchmark.Convenience;

namespace Quantities.Benchmark;

public class PolynomialBenchmark
{
    private const Double scale = Math.E;
    private const Double offset = Math.Tau + Math.E;
    private const Double argument = 0.1321;
    private static readonly Polynomial polynomial = Poly(nominator: scale, denominator: Math.PI, offset: offset);

    [Benchmark(Baseline = true)]
    public Double EvaluateTrivial() => Trivial(argument);
    [Benchmark]
    public Double EvaluatePolynomial() => polynomial * argument;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private static Double Trivial(Double value) => scale * value / Math.PI + offset;
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
|    EvaluateTrivial | 1.2088 ns | 0.0562 ns | 0.0525 ns |  1.00 |    0.00 |
| EvaluatePolynomial | 0.4153 ns | 0.0176 ns | 0.0164 ns |  0.34 |    0.02 |
*/