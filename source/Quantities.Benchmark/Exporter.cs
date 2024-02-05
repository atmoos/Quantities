using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;

namespace Quantities.Benchmark;

public static class Exporter
{
    private static readonly String mark = "/* Summary *";
    private static readonly ILogger logger = new Logger();

    public static void Export(IEnumerable<Summary> summaries)
    {
        var sourceFiles = FindSourceFiles();
        foreach (var summary in summaries) {
            Export(summary, sourceFiles);
        }
    }
    public static void Export(Summary summary) => Export(summary, FindSourceFiles());
    private static void Export(Summary summary, List<FileInfo> allFiles)
        => Export(summary, MarkdownExporter.Console, allFiles);
    private static void Export(Summary summary, IExporter exporter, List<FileInfo> allFiles)
    {
        foreach (var file in exporter.ExportToFiles(summary, logger)) {
            var name = BenchmarkName(file);
            var fileName = $"{name}.cs";
            Console.WriteLine($"Exporting: {name}");
            var sourceFile = allFiles.Single(f => f.Name.EndsWith(fileName));
            UpdateSourceFile(sourceFile, File.ReadAllText(file));
            File.Delete(file);
        }
    }
    private static void UpdateSourceFile(FileInfo source, String result)
    {
        var tmpFile = $"{source.FullName}.tmp";
        try {
            Update(source, tmpFile, result);
        }
        finally {
            File.Move(tmpFile, source.FullName, overwrite: true);
        }

        static void Update(FileInfo source, String dest, String result)
        {
            String line;
            using var reader = source.OpenText();
            using var writer = File.CreateText(dest);
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

            static void Append(TextWriter writer, String result)
            {
                writer.WriteLine(mark);
                writer.Write(result);
                writer.WriteLine("*/");
            }
        }
    }

    private static List<FileInfo> FindSourceFiles()
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
        return dir.EnumerateFiles("*.cs", SearchOption.AllDirectories).ToList();
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
    public String Id => "ConsoleLogger";
    public Int32 Priority => 1;
    public void WriteLine() => Console.WriteLine();
    public void Write(LogKind logKind, String text) => Console.Write(text);
    public void WriteLine(LogKind logKind, String text) => Console.WriteLine(text);
    public void Flush() { }
}
