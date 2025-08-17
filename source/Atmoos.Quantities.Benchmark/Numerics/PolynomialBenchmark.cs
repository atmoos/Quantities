using Atmoos.Quantities.Core.Numerics;

using static Atmoos.Quantities.Benchmark.Convenience;
using static Atmoos.Quantities.Benchmark.Numerics.Trivial;

namespace Atmoos.Quantities.Benchmark.Numerics;

public class PolynomialBenchmark
{
    private const Double scale = Math.E;
    private const Double offset = Math.Tau + Math.E;
    private const Double argument = 0.1321;
    private static readonly (Double, Double, Double) trivial = (3d, 4d, -1d);
    private static readonly Polynomial polynomial = Poly(nominator: scale, denominator: Math.PI, offset: offset);
    private static readonly Polynomial polynomialWithoutOffset = Poly(nominator: scale, denominator: Math.PI);

    [Benchmark(Baseline = true)]
    public Double EvaluateTrivial() => Poly(in trivial, argument);
    [Benchmark]
    public Double EvaluatePolynomial() => polynomial * argument;
    [Benchmark]
    public Double EvaluatePolynomialWithoutOffset() => polynomialWithoutOffset * argument;
}

/* Summary

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method                          | Mean      | Error     | Ratio |
|-------------------------------- |----------:|----------:|------:|
| EvaluateTrivial                 | 9.5954 ns | 0.0745 ns |  1.00 |
| EvaluatePolynomial              | 0.6407 ns | 0.0568 ns |  0.07 |
| EvaluatePolynomialWithoutOffset | 0.3769 ns | 0.0449 ns |  0.04 |
*/
