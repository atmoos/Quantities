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

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                   | Mean      | Error     | StdDev    | Ratio | RatioSD |
|------------------------- |----------:|----------:|----------:|------:|--------:|
| TrivialImplementation    |  4.169 ns | 0.0851 ns | 0.0796 ns |  1.00 |    0.00 |
| PolynomialMultiplication |  1.643 ns | 0.0158 ns | 0.0123 ns |  0.39 |    0.01 |
| PolynomialDivision       |  1.735 ns | 0.0218 ns | 0.0204 ns |  0.42 |    0.01 |
| PolynomialPowerOfTwo     | 21.582 ns | 0.1396 ns | 0.1238 ns |  5.17 |    0.12 |
*/
