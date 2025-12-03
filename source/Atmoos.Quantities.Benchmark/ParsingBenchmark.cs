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
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Mean        | Error    | Ratio | Allocated | Alloc Ratio |
|-------------- |------------:|---------:|------:|----------:|------------:|
| ParseDateTime |    273.4 ns |  0.38 ns |  1.00 |         - |          NA |
| ParseScalar   |  6,285.1 ns | 84.26 ns | 22.99 |     480 B |          NA |
| ParseCompound | 13,078.7 ns | 27.37 ns | 47.84 |    1464 B |          NA |
*/
