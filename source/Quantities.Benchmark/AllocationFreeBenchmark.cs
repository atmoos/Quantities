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

[MemoryDiagnoser]
public class AllocationFreeBenchmark
{
    [Benchmark(Baseline = true)]
    public Object Constructor() => new MyClass();

    [Benchmark]
    public Object AllocationFree() => AllocationFree<MyClass>.Item;

    [Benchmark]
    public Object AllocationFreeFactory() => AllocationFreeFactory<MyClass>.Item;
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                | Mean      | Error     | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|----------:|------:|-------:|----------:|------------:|
| Constructor           | 6.3850 ns | 0.0400 ns | 0.0374 ns |  1.00 | 0.0057 |      24 B |        1.00 |
| AllocationFree        | 0.5961 ns | 0.0192 ns | 0.0179 ns |  0.09 |      - |         - |        0.00 |
| AllocationFreeFactory | 0.5799 ns | 0.0207 ns | 0.0194 ns |  0.09 |      - |         - |        0.00 |
*/
