using System.Reflection;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Serialization;

internal delegate Quant Create(in Double value);

internal static class Deserialization
{
    private static readonly Dictionary<String, Type> prefixes = Scan(typeof(IPrefix));
    private static readonly Dictionary<String, Type> siUnits = Scan(typeof(ISiUnit));
    private static readonly Dictionary<String, Type> metricUnits = Scan(typeof(IMetricUnit));
    private static readonly Dictionary<String, Type> imperialUnits = Scan(typeof(IImperialUnit));
    private static readonly Dictionary<String, Type> nonStandardUnits = Scan(typeof(INoSystemUnit));
    public static Create Find(String system, String unit, String? prefix)
    {
        Type? prefixType = prefix is null ? null : prefixes[prefix];
        return system switch {
            "si" => GetMethod(nameof(CreateSi), siUnits[unit], prefixType),
            "metric" => GetMethod(nameof(CreateMetric), metricUnits[unit], prefixType),
            "imperial" => GetMethod(nameof(CreateImperial), imperialUnits[unit], prefixType),
            "any" => GetMethod(nameof(CreateNonStandard), nonStandardUnits[unit], prefixType),
            _ => throw new NotImplementedException("{} cannot be deserialized yet :-(")
        };
    }

    private static Create CreateSi<TSi>() where TSi : ISiUnit => Build<Si<TSi>>.With;
    private static Create CreateSi<TPrefix, TSi>() where TPrefix : IPrefix where TSi : ISiUnit => Build<Si<TPrefix, TSi>>.With;
    private static Create CreateMetric<TMetric>() where TMetric : IMetricUnit => Build<Metric<TMetric>>.With;
    private static Create CreateMetric<TPrefix, TMetric>() where TPrefix : IPrefix where TMetric : IMetricUnit => Build<Metric<TPrefix, TMetric>>.With;
    private static Create CreateImperial<TImperial>() where TImperial : IImperialUnit => Build<Imperial<TImperial>>.With;
    private static Create CreateNonStandard<TNonStandard>() where TNonStandard : INoSystemUnit => Build<NonStandard<TNonStandard>>.With;

    private static Dictionary<String, Type> Scan(Type interfaceType)
    {
        var types = new Dictionary<String, Type>();
        var interfaceName = interfaceType.FullName ?? String.Empty;
        foreach (var type in typeof(Deserialization).Assembly.GetExportedTypes().Where(t => t.IsValueType && t.GetInterface(interfaceName) is not null)) {
            var representation = GetRepresentation(type);
            types[representation] = type;
        }
        return types;
    }

    private static Create GetMethod(String name, Type unit, Type? prefix = null)
    {
        Int32 genericArgumentCount = prefix is null ? 1 : 2;
        var genericMethod = typeof(Deserialization).GetMethod(name, genericArgumentCount, BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Standard, Type.EmptyTypes, null);
        if (genericMethod is null) {
            throw new InvalidOperationException($"Method '{name}' not found");
        }
        var typeParameters = prefix switch {
            null => new[] { unit },
            var p => new[] { p, unit }
        };

        var createMethod = genericMethod.MakeGenericMethod(typeParameters) ?? throw GetException(name, "created", typeParameters);


        return createMethod.Invoke(null, null) as Create ?? throw new InvalidOperationException();

        static InvalidOperationException GetException(String name, String function, Type[] genericArgs)
        {
            return new InvalidOperationException($"Method '{name}<{String.Join(',', genericArgs.Select(t => t.Name))}>' could not be {function}.");
        }
    }

    private static String GetRepresentation(Type type)
    {
        var representation = type.GetProperties().Where(p => p.Name == nameof(IRepresentable.Representation)).SingleOrDefault()?.GetGetMethod();
        return (representation?.Invoke(null, null)) as String ?? throw new InvalidOperationException($"Failed getting representation for: {type.Name}");
    }

    private static String GetRepresentation<T>()
        where T : IRepresentable => T.Representation;

}