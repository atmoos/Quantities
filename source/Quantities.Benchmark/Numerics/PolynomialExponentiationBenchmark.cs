using Quantities.Core.Numerics;

using static Quantities.Benchmark.Convenience;
using static Quantities.Benchmark.Numerics.Trivial;

namespace Quantities.Benchmark.Numerics;

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

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | StdDev    | Ratio | RatioSD |
|-------------- |--------- |----------:|----------:|----------:|------:|--------:|
| TrivialExp    | -5       | 28.213 ns | 0.2441 ns | 0.2283 ns |  1.00 |    0.00 |
| PolynomialExp | -5       | 39.076 ns | 0.2090 ns | 0.1745 ns |  1.39 |    0.01 |
|               |          |           |           |           |       |         |
| TrivialExp    | -2       | 15.249 ns | 0.0569 ns | 0.0505 ns |  1.00 |    0.00 |
| PolynomialExp | -2       | 26.262 ns | 0.3425 ns | 0.3036 ns |  1.72 |    0.02 |
|               |          |           |           |           |       |         |
| TrivialExp    | 0        | 12.116 ns | 0.1405 ns | 0.1314 ns |  1.00 |    0.00 |
| PolynomialExp | 0        |  2.801 ns | 0.0134 ns | 0.0125 ns |  0.23 |    0.00 |
|               |          |           |           |           |       |         |
| TrivialExp    | 2        | 15.237 ns | 0.1125 ns | 0.0997 ns |  1.00 |    0.00 |
| PolynomialExp | 2        | 16.232 ns | 0.0855 ns | 0.0714 ns |  1.07 |    0.01 |
|               |          |           |           |           |       |         |
| TrivialExp    | 5        | 28.041 ns | 0.2425 ns | 0.2150 ns |  1.00 |    0.00 |
| PolynomialExp | 5        | 31.696 ns | 0.1655 ns | 0.1292 ns |  1.13 |    0.01 |
*/
