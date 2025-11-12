using System.Reflection;
using Atmoos.Quantities.Common;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;
using static Atmoos.Quantities.Common.Reflection;

namespace Atmoos.Quantities.Parsing;

public interface ISystems
{
    public IEnumerable<String> Prefixes { get; }
    public IEnumerable<(String, Int32)> Exponents { get; }
    public IEnumerable<(String, IEnumerable<String>)> Units { get; }
}

public sealed class Systems : ISystems
{
    public IEnumerable<String> Prefixes { get; }
    public IEnumerable<(String, Int32)> Exponents { get; }
    public IEnumerable<(String, IEnumerable<String>)> Units { get; }

    private Systems(IEnumerable<Type> types)
    {
        this.Prefixes = ExtractPrefixes(types);
        this.Units = ExtractUnits(types).Select(kv => (kv.Key, (IEnumerable<String>)kv.Value)).ToList();
        this.Exponents = Enumerable.Range(-9, 19).Where(e => e != 1).Select(e => (Tools.ExpToString(e), e)).ToList();
    }

    internal static Systems Create() => new(typeof(Systems).Assembly.GetExportedTypes());
    public static Systems Create(params Assembly[] assemblies)
        => new(new[] { typeof(Systems).Assembly }.Concat(assemblies).SelectMany(a => a.GetExportedTypes()));

    private static Dictionary<String, List<String>> ExtractUnits(IEnumerable<Type> types)
    {
        Type si = typeof(ISiUnit);
        Type metric = typeof(IMetricUnit);
        Type imperial = typeof(IImperialUnit);
        Type nonStandard = typeof(INonStandardUnit);
        return new() {
            [nameof(si)] = [.. types.Where(t => t.Implements(si)).Select(GetRepresentation)],
            [nameof(metric)] = [.. types.Where(t => t.Implements(metric)).Select(GetRepresentation)],
            [nameof(imperial)] = [.. types.Where(t => t.Implements(imperial)).Select(GetRepresentation)],
            ["any"] = [.. types.Where(t => t.Implements(nonStandard)).Select(GetRepresentation)],
        };
    }

    private static List<String> ExtractPrefixes(IEnumerable<Type> types)
    {
        Type metric = typeof(IMetricPrefix);
        Type binary = typeof(IBinaryPrefix);
        IEnumerable<Type> metricPrefixes = types.Where(t => t.Implements(metric));
        return [.. metricPrefixes.Concat(types.Where(t => t.Implements(binary))).Select(GetRepresentation)];
    }
}
