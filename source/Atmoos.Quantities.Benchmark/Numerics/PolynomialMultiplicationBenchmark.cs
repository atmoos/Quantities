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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method                   | Mean       | Error     | Ratio | 
|------------------------- |-----------:|----------:|------:|
| TrivialImplementation    |  19.119 ns | 0.1469 ns |  1.00 | 
| PolynomialMultiplication |   1.235 ns | 0.0538 ns |  0.06 | 
| PolynomialDivision       |   1.270 ns | 0.0675 ns |  0.07 | 
| PolynomialPowerOfTwo     | 369.177 ns | 0.6220 ns | 19.31 | 
*/
