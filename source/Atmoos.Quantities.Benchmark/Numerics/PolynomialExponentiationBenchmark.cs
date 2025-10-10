using Atmoos.Quantities.Core.Numerics;

using static Atmoos.Quantities.Benchmark.Convenience;
using static Atmoos.Quantities.Benchmark.Numerics.Trivial;

namespace Atmoos.Quantities.Benchmark.Numerics;

public class PolynomialExponentiationBenchmark
{
    private static readonly (Double, Double, Double) trivial = (3d, 4d, -1d);
    private static readonly Double argument = Math.PI / Math.E;
    private static readonly Polynomial polynomial = Poly(nominator: Math.E, denominator: Math.PI, offset: Math.Tau);

    [Params(-5, -2, 0, 2, 5)]
    public Int32 Exponent { get; set; }

    [Benchmark(Baseline = true)]
    public Double TrivialExp() => Poly(PolyExp(in trivial, Exponent), argument);
    [Benchmark]
    public Double PolynomialExp() => polynomial.Pow(Exponent) * argument;
}

/* Summary

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error    | Ratio |
|-------------- |--------- |----------:|---------:|------:|
| TrivialExp    | -5       |  73.67 ns | 0.239 ns |  1.00 |
| PolynomialExp | -5       | 397.93 ns | 1.286 ns |  5.40 |
|               |          |           |          |       |
| TrivialExp    | -2       |  42.21 ns | 0.147 ns |  1.00 |
| PolynomialExp | -2       | 377.65 ns | 0.993 ns |  8.95 |
|               |          |           |          |       |
| TrivialExp    | 0        |  20.61 ns | 0.066 ns |  1.00 |
| PolynomialExp | 0        | 377.15 ns | 1.438 ns | 18.30 |
|               |          |           |          |       |
| TrivialExp    | 2        |  42.64 ns | 0.093 ns |  1.00 |
| PolynomialExp | 2        | 377.47 ns | 1.068 ns |  8.85 |
|               |          |           |          |       |
| TrivialExp    | 5        |  74.74 ns | 0.312 ns |  1.00 |
| PolynomialExp | 5        | 399.25 ns | 0.841 ns |  5.34 |
*/
