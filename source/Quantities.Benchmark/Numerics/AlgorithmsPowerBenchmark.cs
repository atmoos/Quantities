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
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 69.230 ns | 0.3465 ns |  1.00 |
| AlgorithmsPow | 2        |  5.005 ns | 0.0010 ns |  0.07 |
|               |          |           |           |       |
| MathPow       | 5        | 69.519 ns | 0.3334 ns |  1.00 |
| AlgorithmsPow | 5        | 11.403 ns | 0.0021 ns |  0.16 |
|               |          |           |           |       |
| MathPow       | 12       | 68.143 ns | 0.5493 ns |  1.00 |
| AlgorithmsPow | 12       | 15.918 ns | 0.0020 ns |  0.23 |
|               |          |           |           |       |
| MathPow       | 23       | 69.319 ns | 0.3042 ns |  1.00 |
| AlgorithmsPow | 23       | 25.284 ns | 0.0017 ns |  0.36 |
*/
