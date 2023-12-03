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

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method                | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|------:|----------:|------------:|
| Constructor           | 6.5466 ns | 0.0194 ns |  1.00 |      24 B |        1.00 |
| AllocationFree        | 0.6569 ns | 0.0077 ns |  0.10 |         - |        0.00 |
| AllocationFreeFactory | 0.6458 ns | 0.0728 ns |  0.10 |         - |        0.00 |
*/
