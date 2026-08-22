using Atmoos.Quantities.Core.Numerics;
using static Atmoos.Quantities.Benchmark.Convenience;
using static Atmoos.Quantities.Benchmark.Numerics.Trivial;

namespace Atmoos.Quantities.Benchmark.Numerics;

public class PolynomialBenchmark
{
    private const Double scale = Math.E;
    private const Double offset = Math.Tau + Math.E;
    private const Double argument = 0.1321;
    private static readonly (Double, Double, Double) trivial = (3d, 4d, -1d);
    private static readonly Polynomial polynomial = Poly(nominator: scale, denominator: Math.PI, offset: offset);
    private static readonly Polynomial polynomialWithoutOffset = Poly(nominator: scale, denominator: Math.PI);

    [Benchmark(Baseline = true)]
    public Double EvaluateTrivial() => Poly(in trivial, argument);

    [Benchmark]
    public Double EvaluatePolynomial() => polynomial * argument;

    [Benchmark]
    public Double EvaluatePolynomialWithoutOffset() => polynomialWithoutOffset * argument;
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method                          | Mean      | Error     | Ratio |
|-------------------------------- |----------:|----------:|------:|
| EvaluateTrivial                 | 1.4085 ns | 0.0031 ns |  1.00 |
| EvaluatePolynomial              | 0.1306 ns | 0.0040 ns |  0.09 |
| EvaluatePolynomialWithoutOffset | 0.0562 ns | 0.0010 ns |  0.04 |
*/
