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
    private static readonly Polynomial polynomialWithoutOffset = Poly(nominator: scale, denominator: Math.PI);

    [Benchmark(Baseline = true)]
    public Double EvaluateTrivial() => Trivial(argument);
    [Benchmark]
    public Double EvaluatePolynomial() => polynomial * argument;
    [Benchmark]
    public Double EvaluatePolynomialWithoutOffset() => polynomialWithoutOffset * argument;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private static Double Trivial(Double value) => scale * value / Math.PI + offset;
}

/*

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                          | Mean      | Error     | StdDev    | Ratio | RatioSD |
|-------------------------------- |----------:|----------:|----------:|------:|--------:|
| EvaluateTrivial                 | 0.9766 ns | 0.0133 ns | 0.0117 ns |  1.00 |    0.00 |
| EvaluatePolynomial              | 0.4931 ns | 0.0413 ns | 0.0442 ns |  0.52 |    0.04 |
| EvaluatePolynomialWithoutOffset | 0.4047 ns | 0.0076 ns | 0.0067 ns |  0.41 |    0.01 |
*/
