using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Quantities.Benchmark;
using static BenchmarkDotNet.Columns.StatisticColumn;

// dotnet run -c Release --project Quantities.Benchmark/
var config = DefaultConfig.Instance.HideColumns(StdDev, Median, Kurtosis, BaselineRatioColumn.RatioStdDev);

var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);

Exporter.Export(summary);
