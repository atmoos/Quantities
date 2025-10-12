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

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 4.00GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method                          | Mean      | Error     | Ratio |
|-------------------------------- |----------:|----------:|------:|
| EvaluateTrivial                 | 9.4015 ns | 0.0592 ns |  1.00 |
| EvaluatePolynomial              | 0.5623 ns | 0.0593 ns |  0.06 |
| EvaluatePolynomialWithoutOffset | 0.3874 ns | 0.0602 ns |  0.04 |
*/
