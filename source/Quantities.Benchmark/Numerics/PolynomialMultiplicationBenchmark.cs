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

BenchmarkDotNet v0.13.12, Arch Linux ARM
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method                   | Mean     | Error    | Ratio |
|------------------------- |---------:|---------:|------:|
| TrivialImplementation    | 15.90 ns | 0.011 ns |  1.00 |
| PolynomialMultiplication | 10.80 ns | 0.005 ns |  0.68 |
| PolynomialDivision       | 10.14 ns | 0.007 ns |  0.64 |
| PolynomialPowerOfTwo     | 37.59 ns | 0.009 ns |  2.36 |
*/
