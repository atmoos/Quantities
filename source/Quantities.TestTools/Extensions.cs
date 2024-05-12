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

    public static DirectoryInfo RepoDir { get; } = FindRepoDirOf(typeof(Extensions).Assembly);

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

    private static DirectoryInfo FindRepoDirOf(Assembly assembly)
    {
        var dir = new FileInfo(assembly.Location).Directory ?? throw new Exception("Assembly folder not found");
        while (dir.EnumerateDirectories(".git", repoOptions).SingleOrDefault() == null) {
            var prev = dir.FullName;
            dir = dir.Parent;
            if (dir == null || prev == dir.FullName) {
                throw new Exception($"Failed finding containing repo of assembly {assembly.GetName().Name}.");
            }
        }
        return dir;
    }
}
