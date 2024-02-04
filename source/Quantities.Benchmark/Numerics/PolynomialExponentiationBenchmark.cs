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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| TrivialExp    | -5       | 28.487 ns | 0.0435 ns |  1.00 |
| PolynomialExp | -5       | 39.455 ns | 0.0633 ns |  1.39 |
|               |          |           |           |       |
| TrivialExp    | -2       | 15.077 ns | 0.0132 ns |  1.00 |
| PolynomialExp | -2       | 26.505 ns | 0.0163 ns |  1.76 |
|               |          |           |           |       |
| TrivialExp    | 0        | 11.868 ns | 0.0240 ns |  1.00 |
| PolynomialExp | 0        |  2.921 ns | 0.0078 ns |  0.25 |
|               |          |           |           |       |
| TrivialExp    | 2        | 15.117 ns | 0.0443 ns |  1.00 |
| PolynomialExp | 2        | 17.293 ns | 0.0481 ns |  1.14 |
|               |          |           |           |       |
| TrivialExp    | 5        | 29.160 ns | 0.0399 ns |  1.00 |
| PolynomialExp | 5        | 33.342 ns | 0.0597 ns |  1.14 |
*/
