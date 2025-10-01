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
Intel Core i7-8565U CPU 1.80GHz (Max: 4.00GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean     | Error    | Ratio |
|-------------- |--------- |---------:|---------:|------:|
| TrivialExp    | -5       | 73.81 ns | 0.275 ns |  1.00 |
| PolynomialExp | -5       | 34.39 ns | 0.040 ns |  0.47 |
|               |          |          |          |       |
| TrivialExp    | -2       | 42.09 ns | 0.140 ns |  1.00 |
| PolynomialExp | -2       | 12.97 ns | 0.086 ns |  0.31 |
|               |          |          |          |       |
| TrivialExp    | 0        | 21.13 ns | 0.059 ns |  1.00 |
| PolynomialExp | 0        | 13.02 ns | 0.065 ns |  0.62 |
|               |          |          |          |       |
| TrivialExp    | 2        | 42.54 ns | 0.121 ns |  1.00 |
| PolynomialExp | 2        | 14.12 ns | 0.079 ns |  0.33 |
|               |          |          |          |       |
| TrivialExp    | 5        | 74.60 ns | 0.156 ns |  1.00 |
| PolynomialExp | 5        | 36.24 ns | 0.065 ns |  0.49 |
*/
