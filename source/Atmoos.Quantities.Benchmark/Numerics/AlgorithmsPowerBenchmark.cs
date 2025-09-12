using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Benchmark.Numerics;

public class AlgorithmsPowerBenchmark
{
    private const Double value = Math.Tau;

    [Params(2, 5, 12, 23)]
    public Int32 Exponent { get; set; }

    [Benchmark(Baseline = true)]
    public Double MathPow() => Math.Pow(value, Exponent);

    [Benchmark]
    public Double AlgorithmsPow() => Algorithms.Pow(value, Exponent);
}

/* Summary

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 27.855 ns | 0.1787 ns |  1.00 |
| AlgorithmsPow | 2        |  9.943 ns | 0.0783 ns |  0.36 |
|               |          |           |           |       |
| MathPow       | 5        | 27.966 ns | 0.1792 ns |  1.00 |
| AlgorithmsPow | 5        | 28.676 ns | 0.1133 ns |  1.03 |
|               |          |           |           |       |
| MathPow       | 12       | 27.981 ns | 0.2363 ns |  1.00 |
| AlgorithmsPow | 12       | 39.647 ns | 0.1023 ns |  1.42 |
|               |          |           |           |       |
| MathPow       | 23       | 27.843 ns | 0.1058 ns |  1.00 |
| AlgorithmsPow | 23       | 49.069 ns | 0.2347 ns |  1.76 |
*/
