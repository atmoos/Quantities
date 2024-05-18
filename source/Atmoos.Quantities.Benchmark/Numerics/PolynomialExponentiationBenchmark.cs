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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| TrivialExp    | -5       | 27.841 ns | 0.0733 ns |  1.00 |
| PolynomialExp | -5       | 40.273 ns | 0.1070 ns |  1.45 |
|               |          |           |           |       |
| TrivialExp    | -2       | 14.962 ns | 0.0555 ns |  1.00 |
| PolynomialExp | -2       | 26.410 ns | 0.0317 ns |  1.77 |
|               |          |           |           |       |
| TrivialExp    | 0        | 11.192 ns | 0.0472 ns |  1.00 |
| PolynomialExp | 0        |  2.810 ns | 0.0073 ns |  0.25 |
|               |          |           |           |       |
| TrivialExp    | 2        | 15.671 ns | 0.0405 ns |  1.00 |
| PolynomialExp | 2        | 16.801 ns | 0.0543 ns |  1.07 |
|               |          |           |           |       |
| TrivialExp    | 5        | 28.264 ns | 0.0615 ns |  1.00 |
| PolynomialExp | 5        | 31.938 ns | 0.0609 ns |  1.13 |
*/
