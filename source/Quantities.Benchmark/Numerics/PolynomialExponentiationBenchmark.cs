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

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio | 
|-------------- |--------- |----------:|----------:|------:|-
| TrivialExp    | -5       | 28.339 ns | 0.1630 ns |  1.00 | 
| PolynomialExp | -5       | 39.249 ns | 0.1135 ns |  1.38 | 
|               |          |           |           |       | 
| TrivialExp    | -2       | 15.256 ns | 0.3313 ns |  1.00 | 
| PolynomialExp | -2       | 27.132 ns | 0.1232 ns |  1.78 | 
|               |          |           |           |       | 
| TrivialExp    | 0        | 11.624 ns | 0.0746 ns |  1.00 | 
| PolynomialExp | 0        |  2.878 ns | 0.0136 ns |  0.25 | 
|               |          |           |           |       | 
| TrivialExp    | 2        | 15.483 ns | 0.0366 ns |  1.00 | 
| PolynomialExp | 2        | 17.187 ns | 0.0808 ns |  1.11 | 
|               |          |           |           |       | 
| TrivialExp    | 5        | 29.299 ns | 0.1510 ns |  1.00 | 
| PolynomialExp | 5        | 33.043 ns | 0.1744 ns |  1.13 | 
*/
