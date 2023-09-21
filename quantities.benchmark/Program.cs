using BenchmarkDotNet.Running;
using Quantities.Benchmark;

// dotnet run -c Release --project quantities.benchmark/quantities.benchmark.csproj
BenchmarkRunner.Run<MeasureBenchmark>();
