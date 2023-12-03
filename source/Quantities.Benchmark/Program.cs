using BenchmarkDotNet.Running;
using Quantities.Benchmark;

// dotnet run -c Release --project Quantities.Benchmark/
//var summary = BenchmarkRunner.Run<PolynomialExponentiationBenchmark>();
var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
Exporter.Export(summary);
