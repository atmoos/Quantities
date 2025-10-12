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

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 4.00GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 27.787 ns | 0.1130 ns |  1.00 |
| AlgorithmsPow | 2        |  9.847 ns | 0.0829 ns |  0.35 |
|               |          |           |           |       |
| MathPow       | 5        | 27.740 ns | 0.0671 ns |  1.00 |
| AlgorithmsPow | 5        | 28.057 ns | 0.1082 ns |  1.01 |
|               |          |           |           |       |
| MathPow       | 12       | 27.775 ns | 0.0718 ns |  1.00 |
| AlgorithmsPow | 12       | 39.408 ns | 0.0845 ns |  1.42 |
|               |          |           |           |       |
| MathPow       | 23       | 27.745 ns | 0.1207 ns |  1.00 |
| AlgorithmsPow | 23       | 48.422 ns | 0.2260 ns |  1.75 |
*/
