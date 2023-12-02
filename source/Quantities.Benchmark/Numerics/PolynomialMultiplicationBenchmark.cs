using Quantities.Core.Numerics;

using static Quantities.Benchmark.Convenience;
using static Quantities.Benchmark.Numerics.Trivial;

namespace Quantities.Benchmark.Numerics;

public class PolynomialMultiplicationBenchmark
{
    private static readonly (Double, Double, Double) trivialA = (3d, 4d, -1d);
    private static readonly (Double, Double, Double) trivialB = (-2d, 3d, 1d);
    private static readonly Double argument = Math.PI / Math.E;
    private static readonly Polynomial left = Poly(nominator: Math.E, denominator: Math.PI, offset: Math.Tau);
    private static readonly Polynomial right = Poly(nominator: Math.Tau, denominator: Math.E, offset: Math.PI);

    [Benchmark(Baseline = true)]
    public Double TrivialImplementation() => Poly(in trivialB, Poly(in trivialA, in argument));
    [Benchmark]
    public Double PolynomialMultiplication() => left * right * argument;
    [Benchmark]
    public Double PolynomialDivision() => left / right * argument;
    [Benchmark]
    public Double PolynomialPowerOfTwo() => left.Pow(2) * argument;
}

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


 Method                   | Mean       | Error     | StdDev    | Ratio | RatioSD |
------------------------- |-----------:|----------:|----------:|------:|--------:|
 TrivialImplementation    |  3.9180 ns | 0.0379 ns | 0.0336 ns |  1.00 |    0.00 |
 PolynomialMultiplication |  1.1009 ns | 0.0091 ns | 0.0080 ns |  0.28 |    0.00 |
 PolynomialDivision       |  0.3113 ns | 0.0063 ns | 0.0059 ns |  0.08 |    0.00 |
 PolynomialPowerOfTwo     | 17.0374 ns | 0.0603 ns | 0.0534 ns |  4.35 |    0.04 |
*/
