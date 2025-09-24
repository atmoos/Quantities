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

BenchmarkDotNet v0.15.3, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error    | Ratio | 
|-------------- |--------- |----------:|---------:|------:|
| TrivialExp    | -5       |  74.13 ns | 0.313 ns |  1.00 | 
| PolynomialExp | -5       | 403.30 ns | 1.290 ns |  5.44 | 
|               |          |           |          |       | 
| TrivialExp    | -2       |  42.73 ns | 0.060 ns |  1.00 | 
| PolynomialExp | -2       | 379.76 ns | 1.124 ns |  8.89 | 
|               |          |           |          |       | 
| TrivialExp    | 0        |  21.40 ns | 0.101 ns |  1.00 | 
| PolynomialExp | 0        | 378.73 ns | 1.628 ns | 17.70 | 
|               |          |           |          |       | 
| TrivialExp    | 2        |  43.15 ns | 0.178 ns |  1.00 | 
| PolynomialExp | 2        | 380.28 ns | 1.052 ns |  8.81 | 
|               |          |           |          |       | 
| TrivialExp    | 5        |  75.35 ns | 0.237 ns |  1.00 | 
| PolynomialExp | 5        | 401.27 ns | 1.569 ns |  5.33 | 
*/
