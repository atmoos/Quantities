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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method                          | Mean      | Error     | Ratio |
|-------------------------------- |----------:|----------:|------:|-
| EvaluateTrivial                 | 1.3786 ns | 0.0036 ns |  1.00 |
| EvaluatePolynomial              | 0.0932 ns | 0.0337 ns |  0.07 |
| EvaluatePolynomialWithoutOffset | 0.1154 ns | 0.0054 ns |  0.08 |
*/
