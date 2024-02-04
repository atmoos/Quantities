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

BenchmarkDotNet v0.13.12, Arch Linux ARM
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method        | Exponent | Mean     | Error    | Ratio |
|-------------- |--------- |---------:|---------:|------:|
| TrivialExp    | -5       | 89.98 ns | 0.107 ns |  1.00 |
| PolynomialExp | -5       | 78.83 ns | 0.061 ns |  0.88 |
|               |          |          |          |       |
| TrivialExp    | -2       | 43.16 ns | 0.009 ns |  1.00 |
| PolynomialExp | -2       | 53.00 ns | 0.029 ns |  1.23 |
|               |          |          |          |       |
| TrivialExp    | 0        | 15.05 ns | 0.003 ns |  1.00 |
| PolynomialExp | 0        | 10.81 ns | 0.006 ns |  0.72 |
|               |          |          |          |       |
| TrivialExp    | 2        | 43.16 ns | 0.014 ns |  1.00 |
| PolynomialExp | 2        | 38.07 ns | 0.005 ns |  0.88 |
|               |          |          |          |       |
| TrivialExp    | 5        | 89.88 ns | 0.031 ns |  1.00 |
| PolynomialExp | 5        | 69.28 ns | 0.234 ns |  0.77 |
*/
