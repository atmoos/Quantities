using System.Reflection;
using Quantities.Units;

namespace Quantities.TestTools;

public class Export
{
    private const String tail = "└── ";
    private const String child = "├── ";
    private const String empty = "    ";
    private const String separator = "│   ";

    public static IEnumerable<String> AllUnits(String title, Assembly assembly)
    {
        yield return title;
        yield return $"{child}Si";
        foreach (var line in SiUnits(assembly)) {
            yield return $"{separator}{line}";
        }
        var imperialUnits = assembly.ExportAllImplementationsOf(typeof(IImperialUnit));
        var nonStandardUnits = assembly.ExportAllImplementationsOf(typeof(INonStandardUnit)).ToArray();
        var (prefix, separation) = nonStandardUnits.Length == 0 ? (tail, empty) : (child, separator);
        foreach (var line in GroupBySystem($"{prefix}Imperial", imperialUnits, separation)) {
            yield return line;
        }
        foreach (var line in GroupBySystem($"{tail}NonStandard", nonStandardUnits, empty)) {
            yield return line;
        }
    }

    public static IEnumerable<String> Section(String title, IEnumerable<Type> units, String prefix)
    {
        var all = units.OrderBy(u => u.Name).ToArray();
        if (all.Length == 0) {
            yield break;
        }
        yield return title;
        foreach (var (unit, count) in all.Select((u, no) => (u, no + 1))) {
            var middle = count < all.Length ? child : tail;
            yield return $"{prefix}{middle}{unit.Name}";
        }
    }

    private static IEnumerable<String> SiUnits(Assembly assembly)
    {
        var siUnits = assembly.ExportAllImplementationsOf(typeof(ISiUnit)).ToHashSet();
        var siDerivedUnits = siUnits.Where(u => u.Namespace?.Contains("Derived") == true).ToHashSet();
        siUnits.RemoveWhere(siDerivedUnits.Contains);
        var metricUnits = assembly.ExportAllImplementationsOf(typeof(IMetricUnit));
        foreach (var si in siUnits.OrderBy(u => u.Name)) {
            yield return $"{child}{si.Name}";
        }
        foreach (var line in GroupBySystem($"{child}Derived", siDerivedUnits, separator)) {
            yield return line;
        }
        foreach (var line in GroupBySystem($"{tail}Metric", metricUnits, empty)) {
            yield return line;
        }
    }

    private static IEnumerable<String> GroupBySystem(String title, IEnumerable<Type> units, String prefix)
    {
        var groups = units.GroupBy(Extensions.SystemName).OrderBy(g => g.Key).ToArray();
        if (groups.Length == 0) {
            yield break;
        }
        yield return title;
        foreach (var (system, group, groupNo) in groups.Select((g, no) => (g.Key, g, no + 1))) {
            var (titlePrefix, sep) = groupNo < groups.Length ? (child, separator) : (tail, empty);
            foreach (var line in Section($"{titlePrefix}{system}", group, sep)) {
                yield return $"{prefix}{line}";
            }
        }
    }
}
