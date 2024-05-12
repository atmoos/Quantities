using Atmoos.Sphere.BenchmarkDotNet;
using Atmoos.Sphere.Text;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

using static BenchmarkDotNet.Columns.StatisticColumn;

// dotnet run -c Release --project Quantities.Benchmark/

var assembly = typeof(Program).Assembly;
var config = DefaultConfig.Instance.HideColumns(StdDev, Median, Kurtosis, BaselineRatioColumn.RatioStdDev);

var summary = BenchmarkSwitcher.FromAssembly(assembly).Run(args, config);

var summaryTag = new LineTag { Start = "/* Summary", End = "*/" };
await assembly.Export(summary, new ExportConfig { Tag = summaryTag });
