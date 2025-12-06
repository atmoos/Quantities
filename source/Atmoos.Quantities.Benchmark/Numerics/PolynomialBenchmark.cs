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
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method                          | Mean      | Error     | Ratio |
|-------------------------------- |----------:|----------:|------:|
| EvaluateTrivial                 | 1.4201 ns | 0.0116 ns |  1.00 |
| EvaluatePolynomial              | 0.1059 ns | 0.0028 ns |  0.07 |
| EvaluatePolynomialWithoutOffset | 0.0688 ns | 0.0107 ns |  0.05 |
*/
