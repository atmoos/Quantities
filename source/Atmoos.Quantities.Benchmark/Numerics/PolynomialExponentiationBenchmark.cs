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
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| TrivialExp    | -5       | 27.177 ns | 0.0605 ns |  1.00 |
| PolynomialExp | -5       | 12.552 ns | 0.0898 ns |  0.46 |
|               |          |           |           |       |
| TrivialExp    | -2       | 14.964 ns | 0.0209 ns |  1.00 |
| PolynomialExp | -2       |  5.930 ns | 0.0189 ns |  0.40 |
|               |          |           |           |       |
| TrivialExp    | 0        | 11.397 ns | 0.0254 ns |  1.00 |
| PolynomialExp | 0        |  3.389 ns | 0.0403 ns |  0.30 |
|               |          |           |           |       |
| TrivialExp    | 2        | 14.882 ns | 0.0368 ns |  1.00 |
| PolynomialExp | 2        |  3.457 ns | 0.0021 ns |  0.23 |
|               |          |           |           |       |
| TrivialExp    | 5        | 27.829 ns | 0.0916 ns |  1.00 |
| PolynomialExp | 5        | 10.910 ns | 0.0438 ns |  0.39 |
*/
