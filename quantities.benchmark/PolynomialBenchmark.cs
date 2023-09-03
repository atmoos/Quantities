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


|             Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------- |---------:|----------:|----------:|------:|--------:|
|    EvaluateTrivial | 1.239 ns | 0.0166 ns | 0.0156 ns |  1.00 |    0.00 |
|       EvaluateNoOp | 5.037 ns | 0.1319 ns | 0.1466 ns |  4.08 |    0.16 |
|      EvaluateShift | 5.065 ns | 0.0190 ns | 0.0159 ns |  4.09 |    0.06 |
|    EvaluateScaleUp | 5.378 ns | 0.1378 ns | 0.1289 ns |  4.34 |    0.14 |
|   EvaluateLinearUp | 5.537 ns | 0.0272 ns | 0.0255 ns |  4.47 |    0.07 |
|  EvaluateScaleDown | 5.088 ns | 0.0283 ns | 0.0221 ns |  4.11 |    0.04 |
| EvaluateLinearDown | 5.332 ns | 0.0290 ns | 0.0257 ns |  4.30 |    0.07 |
|   EvaluateFraction | 5.346 ns | 0.0216 ns | 0.0192 ns |  4.31 |    0.06 |
|       EvaluateFull | 4.817 ns | 0.0182 ns | 0.0162 ns |  3.89 |    0.06 |
*/