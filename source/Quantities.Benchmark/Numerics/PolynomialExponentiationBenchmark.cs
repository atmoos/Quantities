using BenchmarkDotNet.Attributes;
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

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | StdDev    | Ratio | RatioSD |
|-------------- |--------- |----------:|----------:|----------:|------:|--------:|
| TrivialExp    | -5       | 29.656 ns | 0.4883 ns | 0.4568 ns |  1.00 |    0.00 |
| PolynomialExp | -5       | 76.709 ns | 0.5676 ns | 0.5309 ns |  2.59 |    0.05 |
|               |          |           |           |           |       |         |
| TrivialExp    | -2       | 16.293 ns | 0.0348 ns | 0.0272 ns |  1.00 |    0.00 |
| PolynomialExp | -2       | 58.795 ns | 0.1202 ns | 0.0938 ns |  3.61 |    0.01 |
|               |          |           |           |           |       |         |
| TrivialExp    | 0        | 12.298 ns | 0.1006 ns | 0.0941 ns |  1.00 |    0.00 |
| PolynomialExp | 0        |  3.775 ns | 0.0143 ns | 0.0127 ns |  0.31 |    0.00 |
|               |          |           |           |           |       |         |
| TrivialExp    | 2        | 16.106 ns | 0.0319 ns | 0.0299 ns |  1.00 |    0.00 |
| PolynomialExp | 2        | 54.181 ns | 0.2202 ns | 0.1719 ns |  3.36 |    0.01 |
|               |          |           |           |           |       |         |
| TrivialExp    | 5        | 30.530 ns | 0.1160 ns | 0.1085 ns |  1.00 |    0.00 |
| PolynomialExp | 5        | 65.918 ns | 0.5967 ns | 0.5582 ns |  2.16 |    0.02 |
*/
