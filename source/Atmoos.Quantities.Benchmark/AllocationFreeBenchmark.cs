namespace Atmoos.Quantities.Benchmark;

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

/* Summary

BenchmarkDotNet v0.15.7, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method                | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|------:|----------:|------------:|
| Constructor           | 6.8076 ns | 0.0274 ns |  1.00 |      24 B |        1.00 |
| AllocationFree        | 0.5690 ns | 0.0082 ns |  0.08 |         - |        0.00 |
| AllocationFreeFactory | 0.6064 ns | 0.0540 ns |  0.09 |         - |        0.00 |
*/
