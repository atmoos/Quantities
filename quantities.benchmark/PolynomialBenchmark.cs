using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Quantities.Numerics;
using static Quantities.Benchmark.Convenience;

namespace Quantities.Benchmark;

public class PolynomialBenchmark
{
    private const Double scale = Math.E;
    private const Double offset = Math.Tau + Math.E;
    private const Double argument = 0.1321;
    private static readonly Polynomial noOp = Polynomial.NoOp;
    private static readonly Polynomial shift = Poly(offset: offset);
    private static readonly Polynomial scaleUp = Poly(nominator: scale);
    private static readonly Polynomial linearUp = Poly(nominator: scale, offset: offset);
    private static readonly Polynomial scaleDown = Poly(denominator: scale);
    private static readonly Polynomial linearDown = Poly(denominator: scale, offset: offset);
    private static readonly Polynomial fraction = Poly(nominator: scale, denominator: offset);
    private static readonly Polynomial full = Poly(nominator: scale, denominator: Math.PI, offset: offset);

    [Benchmark(Baseline = true)]
    public Double EvaluateTrivial() => Trivial(argument);
    [Benchmark]
    public Double EvaluateNoOp() => noOp.Evaluate(argument);
    [Benchmark]
    public Double EvaluateShift() => shift.Evaluate(argument);
    [Benchmark]
    public Double EvaluateScaleUp() => scaleUp.Evaluate(argument);
    [Benchmark]
    public Double EvaluateLinearUp() => linearUp.Evaluate(argument);
    [Benchmark]
    public Double EvaluateScaleDown() => scaleDown.Evaluate(argument);
    [Benchmark]
    public Double EvaluateLinearDown() => linearDown.Evaluate(argument);
    [Benchmark]
    public Double EvaluateFraction() => fraction.Evaluate(argument);
    [Benchmark]
    public Double EvaluateFull() => full.Evaluate(argument);

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private Double Trivial(Double value) => scale * value / Math.PI + offset;
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.110
  [Host]     : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2


|             Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |----------:|----------:|----------:|------:|--------:|
|    EvaluateTrivial | 1.2533 ns | 0.0484 ns | 0.0429 ns |  1.00 |    0.00 |
|       EvaluateNoOp | 0.1984 ns | 0.0077 ns | 0.0072 ns |  0.16 |    0.01 |
|      EvaluateShift | 0.1886 ns | 0.0029 ns | 0.0024 ns |  0.15 |    0.01 |
|    EvaluateScaleUp | 0.2053 ns | 0.0117 ns | 0.0104 ns |  0.16 |    0.01 |
|   EvaluateLinearUp | 0.2340 ns | 0.0251 ns | 0.0210 ns |  0.19 |    0.02 |
|  EvaluateScaleDown | 0.2362 ns | 0.0089 ns | 0.0079 ns |  0.19 |    0.01 |
| EvaluateLinearDown | 0.2720 ns | 0.0055 ns | 0.0046 ns |  0.22 |    0.01 |
|   EvaluateFraction | 0.1794 ns | 0.0086 ns | 0.0080 ns |  0.14 |    0.01 |
|       EvaluateFull | 0.2137 ns | 0.0075 ns | 0.0071 ns |  0.17 |    0.01 |
*/