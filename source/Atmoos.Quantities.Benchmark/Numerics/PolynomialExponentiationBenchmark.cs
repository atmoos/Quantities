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

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| TrivialExp    | -5       | 28.255 ns | 0.0238 ns |  1.00 |
| PolynomialExp | -5       | 13.251 ns | 0.1318 ns |  0.47 |
|               |          |           |           |       |
| TrivialExp    | -2       | 14.668 ns | 0.0450 ns |  1.00 |
| PolynomialExp | -2       |  5.195 ns | 0.0060 ns |  0.35 |
|               |          |           |           |       |
| TrivialExp    | 0        | 11.360 ns | 0.0278 ns |  1.00 |
| PolynomialExp | 0        |  3.379 ns | 0.0057 ns |  0.30 |
|               |          |           |           |       |
| TrivialExp    | 2        | 14.654 ns | 0.0352 ns |  1.00 |
| PolynomialExp | 2        |  3.686 ns | 0.0087 ns |  0.25 |
|               |          |           |           |       |
| TrivialExp    | 5        | 28.290 ns | 0.0550 ns |  1.00 |
| PolynomialExp | 5        | 11.141 ns | 0.0393 ns |  0.39 |
*/
