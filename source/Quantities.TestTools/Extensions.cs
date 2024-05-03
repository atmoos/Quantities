using System.Reflection;
using Quantities.Dimensions;

namespace Quantities.TestTools;

public static class Extensions
{
    private static readonly Type dimension = typeof(IDimension);
    private static readonly EnumerationOptions repoOptions = new() {
        RecurseSubdirectories = false,
        MatchCasing = MatchCasing.CaseSensitive,
        AttributesToSkip = FileAttributes.Archive
    };

    public static IEnumerable<Type> ExportAllImplementationsOf(this Assembly assembly, Type declaringType)
    {
        foreach (var type in assembly.ExportedTypes) {
            if (type.IsAssignableTo(declaringType) && type != declaringType) {
                yield return type;
            }
            var interfaces = type.FindInterfaces((i, _) => i.IsGenericType && i.GetGenericTypeDefinition() == declaringType, null);
            if (interfaces?.Length > 0) {
                yield return type;
            }
        }
    }

    public static IEnumerable<Type> ExportAllImplementations(this Type declaringType) => declaringType.Assembly.ExportAllImplementationsOf(declaringType);

    public static void InsertCode(FileInfo file, String marker, IEnumerable<String> lines)
    {
        const String codeMark = "```";
        String copy = Impl(file, $"{codeMark}text {marker}", lines);
        File.Move(copy, file.FullName, overwrite: true);

        static String Impl(FileInfo file, String marker, IEnumerable<String> lines)
        {
            String? line;
            Boolean deleting = false;
            var copyName = $"{file.FullName}.tmp";
            using var copy = File.CreateText(copyName);
            using var source = file.OpenText();
            while ((line = source.ReadLine()) != null) {
                if (deleting && line == codeMark) {
                    deleting = false;
                    copy.WriteLines(lines);
                }
                if (deleting) {
                    continue;
                }
                if (line == marker) {
                    deleting = true;
                }
                copy.WriteLine(line);
            }
            return copyName;
        }
    }

    public static void WriteLines(this StreamWriter writer, IEnumerable<String> lines)
    {
        foreach (var line in lines) {
            writer.WriteLine(line);
        }
    }

    public static DirectoryInfo FindRepoDir()
    {
        var myAssembly = typeof(Extensions).Assembly;
        var dir = new FileInfo(myAssembly.Location).Directory ?? throw new Exception("Assembly folder not found");
        while (dir.EnumerateDirectories(".git", repoOptions).SingleOrDefault() == null) {
            var prev = dir.FullName;
            dir = dir.Parent;
            if (dir == null || prev == dir.FullName) {
                throw new Exception($"Failed finding containing repo of assembly {myAssembly.GetName().Name}.");
            }
        }
        return dir;
    }

    public static FileInfo FindFile(this DirectoryInfo dir, String relativePath)
    {
        var file = dir.EnumerateFiles(relativePath).SingleOrDefault();
        return file ?? throw new FileNotFoundException($"Could not locate file: {relativePath}", relativePath);
    }

    public static String SystemName(this Type type)
    {
        return type.FindInterfaces(IsSystem, null).Single().Name[1..]; // remove interface "I".

        static Boolean IsSystem(Type type, Object? _) => type.IsAssignableTo(dimension) && !(type.IsGenericType || type == dimension);
    }
}
