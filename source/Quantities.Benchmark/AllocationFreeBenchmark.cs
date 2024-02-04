namespace Quantities.Benchmark;

file interface IFactory<out T>
{
    static abstract T Create();
}

file class MyClass : IFactory<MyClass>
{
    public static MyClass Create() => new();
}

// This pattern turns out to not really be any
// faster than using the new constraint.
file static class AllocationFreeFactory<T>
    where T : IFactory<T>
{
    public static T Item { get; } = T.Create();
}

[MemoryDiagnoser(displayGenColumns: false)]
public class AllocationFreeBenchmark
{
    [Benchmark(Baseline = true)]
    public Object Constructor() => new MyClass();

    [Benchmark]
    public Object AllocationFree() => AllocationFree<MyClass>.Item;

    [Benchmark]
    public Object AllocationFreeFactory() => AllocationFreeFactory<MyClass>.Item;
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux ARM
ARMv7 Processor rev 4 (v7l), 4 logical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT


| Method                | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|------:|----------:|------------:|
| Constructor           | 99.183 ns | 0.7802 ns |  1.00 |      12 B |        1.00 |
| AllocationFree        |  1.502 ns | 0.0057 ns |  0.02 |         - |        0.00 |
| AllocationFreeFactory |  6.915 ns | 0.4671 ns |  0.07 |         - |        0.00 |
*/
