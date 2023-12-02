using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;

namespace Quantities.Benchmark;

public static class Exporter
{
    private static readonly String mark = "/* Summary *";

    public static void Export(IEnumerable<Summary> summaries)
    {
        foreach (var summary in summaries) {
            Export(summary);
        }
    }

    public static void Export(Summary summary)
    {
        var dir = FindSourceDir();
        foreach (var file in DefaultExporters.Markdown.ExportToFiles(summary, new Logger())) {
            var fileName = $"{new FileInfo(file).Name.Replace("-report-default.md", String.Empty).Split(".")[^1]}.cs";
            var sourceFile = dir.EnumerateFiles("*.cs", SearchOption.AllDirectories).Single(f => f.Name.EndsWith(fileName));
            UpdateSourceFile(sourceFile, File.ReadAllText(file));
            File.Delete(file);
        }
    }

    private static void UpdateSourceFile(FileInfo source, String result)
    {
        var tmpFile = $"{source.FullName}.tmp";
        using var reader = source.OpenText();
        using var writer = File.CreateText(tmpFile);
        try {
            String line;
            while ((line = reader.ReadLine()) != null) {
                if (line.StartsWith(mark)) {
                    // overwrite existing result
                    Append(writer, result);
                    return;
                }
                writer.WriteLine(line);
            }
            // No mark found, first time appending a result
            writer.WriteLine();
            Append(writer, result);
        }
        finally {
            File.Move(tmpFile, source.FullName, overwrite: true);
        }

        static void Append(TextWriter writer, String result)
        {
            writer.WriteLine(mark);
            writer.Write(result);
            writer.WriteLine("*/");
        }
    }
    private static DirectoryInfo FindSourceDir()
    {
        var myAssembly = typeof(Exporter).Assembly;
        var assemblyName = myAssembly.GetName().Name;
        var dir = new DirectoryInfo(myAssembly.Location);
        while (!dir.Name.EndsWith(assemblyName)) {
            var prev = dir.FullName;
            dir = dir.Parent;
            if (prev == dir.FullName) {
                throw new Exception($"Failed finding source dir @ {prev}");
            }
        }
        return dir;
    }
}

file sealed class Logger : ILogger
{
    public String Id => "SomeId";
    public Int32 Priority => 1;
    public void WriteLine() => Console.WriteLine();
    public void Write(LogKind logKind, String text) => Console.Write(text);
    public void WriteLine(LogKind logKind, String text) => Console.WriteLine(text);
    public void Flush() { }
}
