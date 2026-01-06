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

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Mean        | Error    | Ratio | Allocated | Alloc Ratio |
|-------------- |------------:|---------:|------:|----------:|------------:|
| ParseDateTime |    232.8 ns |  0.15 ns |  1.00 |         - |          NA |
| ParseScalar   |  5,777.2 ns | 26.46 ns | 24.82 |     368 B |          NA |
| ParseCompound | 11,963.5 ns | 26.38 ns | 51.39 |    1352 B |          NA |
*/
