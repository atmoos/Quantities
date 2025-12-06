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
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| TrivialExp    | -5       | 27.686 ns | 0.0809 ns |  1.00 |
| PolynomialExp | -5       | 12.689 ns | 0.0134 ns |  0.46 |
|               |          |           |           |       |
| TrivialExp    | -2       | 14.767 ns | 0.1461 ns |  1.00 |
| PolynomialExp | -2       |  4.840 ns | 0.0074 ns |  0.33 |
|               |          |           |           |       |
| TrivialExp    | 0        | 11.157 ns | 0.0718 ns |  1.00 |
| PolynomialExp | 0        |  3.497 ns | 0.0140 ns |  0.31 |
|               |          |           |           |       |
| TrivialExp    | 2        | 14.514 ns | 0.0818 ns |  1.00 |
| PolynomialExp | 2        |  3.639 ns | 0.0054 ns |  0.25 |
|               |          |           |           |       |
| TrivialExp    | 5        | 27.648 ns | 0.2600 ns |  1.00 |
| PolynomialExp | 5        | 11.130 ns | 0.0601 ns |  0.40 |
*/
