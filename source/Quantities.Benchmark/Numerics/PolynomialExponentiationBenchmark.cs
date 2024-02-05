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

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio | 
|-------------- |--------- |----------:|----------:|------:|-
| TrivialExp    | -5       | 14.965 ns | 0.1260 ns |  1.00 | 
| PolynomialExp | -5       | 33.697 ns | 0.2481 ns |  2.25 | 
|               |          |           |           |       | 
| TrivialExp    | -2       | 13.885 ns | 0.0917 ns |  1.00 | 
| PolynomialExp | -2       | 22.154 ns | 0.1284 ns |  1.60 | 
|               |          |           |           |       | 
| TrivialExp    | 0        | 11.879 ns | 0.1437 ns |  1.00 | 
| PolynomialExp | 0        |  1.885 ns | 0.0195 ns |  0.16 | 
|               |          |           |           |       | 
| TrivialExp    | 2        | 12.562 ns | 0.1730 ns |  1.00 | 
| PolynomialExp | 2        | 10.585 ns | 0.0771 ns |  0.84 | 
|               |          |           |           |       | 
| TrivialExp    | 5        | 15.017 ns | 0.2204 ns |  1.00 | 
| PolynomialExp | 5        | 23.441 ns | 0.2045 ns |  1.56 | 
*/
