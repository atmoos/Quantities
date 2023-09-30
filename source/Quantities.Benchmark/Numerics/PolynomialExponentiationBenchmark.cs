using BenchmarkDotNet.Attributes;
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

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | StdDev    | Ratio | RatioSD |
|-------------- |--------- |----------:|----------:|----------:|------:|--------:|
| TrivialExp    | -5       | 28.576 ns | 0.3791 ns | 0.3546 ns |  1.00 |    0.00 |
| PolynomialExp | -5       | 46.449 ns | 0.3829 ns | 0.3394 ns |  1.63 |    0.02 |
|               |          |           |           |           |       |         |
| TrivialExp    | -2       | 15.524 ns | 0.0562 ns | 0.0526 ns |  1.00 |    0.00 |
| PolynomialExp | -2       | 30.011 ns | 0.2166 ns | 0.1920 ns |  1.93 |    0.01 |
|               |          |           |           |           |       |         |
| TrivialExp    | 0        | 12.006 ns | 0.0947 ns | 0.0790 ns |  1.00 |    0.00 |
| PolynomialExp | 0        |  4.608 ns | 0.0791 ns | 0.0740 ns |  0.38 |    0.01 |
|               |          |           |           |           |       |         |
| TrivialExp    | 2        | 16.041 ns | 0.1313 ns | 0.1228 ns |  1.00 |    0.00 |
| PolynomialExp | 2        | 21.607 ns | 0.0726 ns | 0.0643 ns |  1.35 |    0.01 |
|               |          |           |           |           |       |         |
| TrivialExp    | 5        | 29.740 ns | 0.1348 ns | 0.1261 ns |  1.00 |    0.00 |
| PolynomialExp | 5        | 37.967 ns | 0.7917 ns | 0.7405 ns |  1.28 |    0.03 |
*/
