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

BenchmarkDotNet v0.15.7, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| TrivialExp    | -5       | 27.124 ns | 0.0453 ns |  1.00 |
| PolynomialExp | -5       | 13.026 ns | 0.0290 ns |  0.48 |
|               |          |           |           |       |
| TrivialExp    | -2       | 14.664 ns | 0.0978 ns |  1.00 |
| PolynomialExp | -2       |  6.090 ns | 0.0172 ns |  0.42 |
|               |          |           |           |       |
| TrivialExp    | 0        | 11.633 ns | 0.0351 ns |  1.00 |
| PolynomialExp | 0        |  3.505 ns | 0.0104 ns |  0.30 |
|               |          |           |           |       |
| TrivialExp    | 2        | 14.744 ns | 0.0961 ns |  1.00 |
| PolynomialExp | 2        |  3.811 ns | 0.0117 ns |  0.26 |
|               |          |           |           |       |
| TrivialExp    | 5        | 27.016 ns | 0.0724 ns |  1.00 |
| PolynomialExp | 5        | 11.010 ns | 0.0247 ns |  0.41 |
*/
