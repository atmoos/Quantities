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

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | StdDev    | Ratio |
|-------------- |--------- |----------:|----------:|----------:|------:|
| MathPow       | 2        | 14.894 ns | 0.2901 ns | 0.2714 ns |  1.00 |
| AlgorithmsPow | 2        |  2.701 ns | 0.0320 ns | 0.0300 ns |  0.18 |
|               |          |           |           |           |       |
| MathPow       | 5        | 14.651 ns | 0.1154 ns | 0.1023 ns |  1.00 |
| AlgorithmsPow | 5        |  5.964 ns | 0.0886 ns | 0.0829 ns |  0.41 |
|               |          |           |           |           |       |
| MathPow       | 12       | 15.290 ns | 0.0986 ns | 0.0922 ns |  1.00 |
| AlgorithmsPow | 12       | 10.163 ns | 0.1701 ns | 0.1591 ns |  0.66 |
|               |          |           |           |           |       |
| MathPow       | 23       | 15.696 ns | 0.1079 ns | 0.1009 ns |  1.00 |
| AlgorithmsPow | 23       | 10.620 ns | 0.0610 ns | 0.0571 ns |  0.68 |
*/
