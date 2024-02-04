using Quantities.Core.Numerics;

namespace Quantities.Benchmark.Numerics;

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

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux ARM
ARMv7 Processor rev 4 (v7l), 4 logical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT


| Method        | Exponent | Mean      | Error    | Ratio |
|-------------- |--------- |----------:|---------:|------:|
| MathPow       | 2        | 255.56 ns | 1.531 ns |  1.00 |
| AlgorithmsPow | 2        |  52.07 ns | 0.846 ns |  0.20 |
|               |          |           |          |       |
| MathPow       | 5        | 255.50 ns | 1.261 ns |  1.00 |
| AlgorithmsPow | 5        |  82.51 ns | 0.290 ns |  0.32 |
|               |          |           |          |       |
| MathPow       | 12       | 256.85 ns | 1.103 ns |  1.00 |
| AlgorithmsPow | 12       | 103.42 ns | 1.270 ns |  0.40 |
|               |          |           |          |       |
| MathPow       | 23       | 258.09 ns | 1.180 ns |  1.00 |
| AlgorithmsPow | 23       | 137.35 ns | 0.285 ns |  0.53 |
*/
