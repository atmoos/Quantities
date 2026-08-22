using Atmoos.Quantities.Prefixes;

namespace Atmoos.Quantities.Benchmark;

public class PrefixScalingBenchmarks
{
    private static readonly Random rand = new();
    private static readonly IPrefixInject<Double> toDouble = new ToDouble();
    private Double value;

    [Params(-4, 0, 5)]
    public Int32 Exponent { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        this.value = rand.Next((Int32)1e5, (Int32)1e6) * Math.Pow(10, Exponent) / 1e5;
    }

    [Benchmark(Baseline = true)]
    public Double Baseline()
    {
        // Warning! This is not correct!
        // it does the minimal required amount needed to resemble scaling
        // thus serving as a reasonable baseline for proper scaling.
        var fractionalExponent = Math.Log10(this.value);
        var exp = Math.Round(fractionalExponent);
        return this.value * Math.Pow(10, exp);
    }

    [Benchmark]
    public Double MetricPrefixScaling() => MetricPrefix.Scale(in this.value, toDouble);

    [Benchmark]
    public Double BinaryPrefixScaling() => BinaryPrefix.Scale(in this.value, toDouble);

    private sealed class ToDouble : IPrefixInject<Double>
    {
        public Double Identity(in Double value) => value;

        public Double Inject<TPrefix>(in Double value)
            where TPrefix : IPrefix => value;
    }
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method              | Exponent | Mean       | Error     | Ratio |
|-------------------- |--------- |-----------:|----------:|------:|
| Baseline            | -4       | 25.2634 ns | 0.0899 ns |  1.00 |
| MetricPrefixScaling | -4       |  8.2408 ns | 0.0199 ns |  0.33 |
| BinaryPrefixScaling | -4       |  0.8818 ns | 0.0056 ns |  0.03 |
|                     |          |            |           |       |
| Baseline            | 0        | 23.2577 ns | 0.0564 ns |  1.00 |
| MetricPrefixScaling | 0        |  1.2259 ns | 0.0565 ns |  0.05 |
| BinaryPrefixScaling | 0        |  0.8150 ns | 0.0039 ns |  0.04 |
|                     |          |            |           |       |
| Baseline            | 5        | 22.9735 ns | 0.0634 ns |  1.00 |
| MetricPrefixScaling | 5        | 10.0208 ns | 0.0280 ns |  0.44 |
| BinaryPrefixScaling | 5        |  8.5271 ns | 0.0859 ns |  0.37 |
*/
