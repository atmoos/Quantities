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
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method                | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |-----------:|----------:|------:|----------:|------------:|
| Constructor           | 14.7488 ns | 0.0076 ns |  1.00 |      24 B |        1.00 |
| AllocationFree        |  0.2513 ns | 0.0135 ns |  0.02 |         - |        0.00 |
| AllocationFreeFactory |  0.4629 ns | 0.0393 ns |  0.03 |         - |        0.00 |
*/
