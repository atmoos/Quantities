using Quantities.Core.Numerics;

using static Quantities.Benchmark.Convenience;
using static Quantities.Benchmark.Numerics.Trivial;

namespace Quantities.Benchmark.Numerics;

public class PolynomialMultiplicationBenchmark
{
    private Double argument = 1d;
    private (Double, Double, Double) trivialA = (3d, 4d, -1d);
    private (Double, Double, Double) trivialB = (-2d, 3d, 1d);
    private Polynomial left = Poly(nominator: Math.E, denominator: Math.PI, offset: Math.Tau);
    private Polynomial right = Poly(nominator: Math.Tau, denominator: Math.E, offset: Math.PI);

    [GlobalSetup]
    public void Setup()
    {
        this.argument = Next();
        this.trivialA = (Next(), Next(), Next());
        this.trivialB = (Next(), Next(), Next());
        this.left = Poly(nominator: Next(), denominator: Next(), offset: Next());
        this.right = Poly(nominator: Next(), denominator: Next(), offset: Next());
    }

    [Benchmark(Baseline = true)]
    public Double TrivialImplementation() => Poly(in this.trivialB, Poly(in this.trivialA, in this.argument));
    [Benchmark]
    public Double PolynomialMultiplication() => this.left * this.right * this.argument;
    [Benchmark]
    public Double PolynomialDivision() => this.left / this.right * this.argument;
    [Benchmark]
    public Double PolynomialPowerOfTwo() => this.left.Pow(2) * this.argument;

    private static Double Next() => Math.E * (Random.Shared.NextDouble() - 0.5) + Math.Tau;
}

/* Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method                   | Mean       | Error     | Ratio | 
|------------------------- |-----------:|----------:|------:|-
| TrivialImplementation    |  2.1605 ns | 0.0468 ns |  1.00 | 
| PolynomialMultiplication |  0.5191 ns | 0.0113 ns |  0.24 | 
| PolynomialDivision       |  0.5174 ns | 0.0153 ns |  0.24 | 
| PolynomialPowerOfTwo     | 11.1278 ns | 0.1433 ns |  5.15 | 
*/
