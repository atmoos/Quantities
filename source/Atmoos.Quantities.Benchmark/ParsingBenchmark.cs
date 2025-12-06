using Atmoos.Quantities.Parsing;

namespace Atmoos.Quantities.Benchmark;

[MemoryDiagnoser(displayGenColumns: false)]
public class ParsingBenchmark
{
    private static readonly UnitRepository repository = UnitRepository.Create();
    private static readonly String time = DateTime.UtcNow.ToString("o");
    private static readonly IParser<Length> length = Parser.Create<Length>(repository);
    private static readonly IParser<Velocity> velocity = Parser.Create<Velocity>(repository);

    [Benchmark(Baseline = true)]
    public DateTime ParseDateTime() => DateTime.Parse(time);

    [Benchmark]
    public Length ParseScalar() => length.Parse("-25.4 mm");

    [Benchmark]
    public Velocity ParseCompound() => velocity.Parse("12 km/h");
}

/* Summary

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Mean        | Error     | Ratio | Allocated | Alloc Ratio |
|-------------- |------------:|----------:|------:|----------:|------------:|
| ParseDateTime |    228.7 ns |   0.28 ns |  1.00 |         - |          NA |
| ParseScalar   |  5,724.0 ns |  12.69 ns | 25.03 |     368 B |          NA |
| ParseCompound | 12,039.6 ns | 236.00 ns | 52.65 |    1352 B |          NA |
*/
