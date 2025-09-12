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
| TrivialExp    | -5       |  73.73 ns | 0.218 ns |  1.00 | 
| PolynomialExp | -5       | 403.12 ns | 1.101 ns |  5.47 | 
|               |          |           |          |       | 
| TrivialExp    | -2       |  42.28 ns | 0.149 ns |  1.00 | 
| PolynomialExp | -2       | 380.56 ns | 1.225 ns |  9.00 | 
|               |          |           |          |       | 
| TrivialExp    | 0        |  22.40 ns | 0.157 ns |  1.00 | 
| PolynomialExp | 0        | 379.27 ns | 1.812 ns | 16.93 | 
|               |          |           |          |       | 
| TrivialExp    | 2        |  42.76 ns | 0.129 ns |  1.00 | 
| PolynomialExp | 2        | 377.14 ns | 1.491 ns |  8.82 | 
|               |          |           |          |       | 
| TrivialExp    | 5        |  75.13 ns | 0.255 ns |  1.00 | 
| PolynomialExp | 5        | 402.22 ns | 1.335 ns |  5.35 | 
*/
