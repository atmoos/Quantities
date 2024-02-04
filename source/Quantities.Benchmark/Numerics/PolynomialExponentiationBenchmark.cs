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

BenchmarkDotNet v0.13.12, Arch Linux ARM
ARMv7 Processor rev 4 (v7l), 4 logical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT


| Method        | Exponent | Mean        | Error     | Ratio | 
|-------------- |--------- |------------:|----------:|------:|-
| TrivialExp    | -5       |   332.22 ns |  7.199 ns |  1.00 | 
| PolynomialExp | -5       | 1,209.42 ns | 24.788 ns |  3.64 | 
|               |          |             |           |       | 
| TrivialExp    | -2       |   177.67 ns |  4.399 ns |  1.00 | 
| PolynomialExp | -2       |   686.88 ns |  6.383 ns |  3.86 | 
|               |          |             |           |       | 
| TrivialExp    | 0        |    76.01 ns |  2.333 ns |  1.00 | 
| PolynomialExp | 0        |   261.32 ns |  5.812 ns |  3.45 | 
|               |          |             |           |       | 
| TrivialExp    | 2        |   178.78 ns |  4.387 ns |  1.00 | 
| PolynomialExp | 2        |   656.45 ns | 11.097 ns |  3.67 | 
|               |          |             |           |       | 
| TrivialExp    | 5        |   326.12 ns |  7.333 ns |  1.00 | 
| PolynomialExp | 5        | 1,188.24 ns | 23.415 ns |  3.64 | 
*/
