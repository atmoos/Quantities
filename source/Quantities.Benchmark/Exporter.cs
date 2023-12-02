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

    public static void Export(Summary summary) => Export(summary, MarkdownExporter.Console);

    private static void Export(Summary summary, IExporter exporter)
    {
        var dir = FindSourceDir();
        foreach (var file in exporter.ExportToFiles(summary, new Logger())) {
            var name = BenchmarkName(file);
            var fileName = $"{name}.cs";
            Console.WriteLine($"Exporting: {name}");
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
    private static String BenchmarkName(ReadOnlySpan<Char> reportPath)
    {
        var index = reportPath.IndexOf('-');
        var sourceName = reportPath[..index];
        index = sourceName.LastIndexOf('.') + 1;
        return new String(sourceName[index..]);
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
