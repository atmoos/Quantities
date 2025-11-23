using Atmoos.Quantities.Core.Numerics;

using static Atmoos.Quantities.Benchmark.Convenience;
using static Atmoos.Quantities.Benchmark.Numerics.Trivial;

namespace Atmoos.Quantities.Benchmark.Numerics;

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

/* Summary

BenchmarkDotNet v0.15.7, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method                   | Mean     | Error     | Ratio |
|------------------------- |---------:|----------:|------:|
| TrivialImplementation    | 4.007 ns | 0.0124 ns |  1.00 |
| PolynomialMultiplication | 1.182 ns | 0.0075 ns |  0.30 |
| PolynomialDivision       | 1.207 ns | 0.0103 ns |  0.30 |
| PolynomialPowerOfTwo     | 3.783 ns | 0.0050 ns |  0.94 |
*/
