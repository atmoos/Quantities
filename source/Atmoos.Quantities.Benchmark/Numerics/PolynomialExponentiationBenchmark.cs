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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error    | Ratio |
|-------------- |--------- |----------:|---------:|------:|
| TrivialExp    | -5       |  74.51 ns | 0.350 ns |  1.00 |
| PolynomialExp | -5       | 112.35 ns | 1.036 ns |  1.51 |
|               |          |           |          |       |
| TrivialExp    | -2       |  42.60 ns | 0.087 ns |  1.00 |
| PolynomialExp | -2       |  52.58 ns | 0.492 ns |  1.23 |
|               |          |           |          |       |
| TrivialExp    | 0        |  21.48 ns | 0.107 ns |  1.00 |
| PolynomialExp | 0        |  13.04 ns | 0.089 ns |  0.61 |
|               |          |           |          |       |
| TrivialExp    | 2        |  43.24 ns | 0.083 ns |  1.00 |
| PolynomialExp | 2        |  50.19 ns | 0.458 ns |  1.16 |
|               |          |           |          |       |
| TrivialExp    | 5        |  75.56 ns | 0.291 ns |  1.00 |
| PolynomialExp | 5        | 112.07 ns | 0.894 ns |  1.48 |
*/
