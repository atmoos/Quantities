using Quantities.Core.Numerics;

using static Quantities.Benchmark.Convenience;
using static Quantities.Benchmark.Numerics.Trivial;

namespace Quantities.Benchmark.Numerics;

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

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method                          | Mean      | Error     | Ratio |
|-------------------------------- |----------:|----------:|------:|
| EvaluateTrivial                 | 1.4199 ns | 0.0090 ns |  1.00 |
| EvaluatePolynomial              | 0.0906 ns | 0.0008 ns |  0.06 |
| EvaluatePolynomialWithoutOffset | 0.1325 ns | 0.0025 ns |  0.09 |
*/
