using System.Reflection;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Serialization;

internal static class ScalarBuilder
{
    private delegate IBuilder Creator(IInject inject);
    private static readonly IInject scalarInjector = new ScalarInjector();
    private static readonly MethodInfo getRepresentation = GetGenericMethod(nameof(GetRepresentation), 1);
    private static readonly Dictionary<String, Type> prefixes = Scan(typeof(IPrefix));
    private static readonly Dictionary<String, Type> siUnits = Scan(typeof(ISiUnit));
    private static readonly Dictionary<String, Type> metricUnits = Scan(typeof(IMetricUnit));
    private static readonly Dictionary<String, Type> imperialUnits = Scan(typeof(IImperialUnit));
    private static readonly Dictionary<String, Type> nonStandardUnits = Scan(typeof(INoSystemUnit));
    public static IBuilder Create(in QuantityModel model, ITypeVerification verification) => Find(verification, model.System, model.Prefix, model.Unit)(scalarInjector);

    private static Creator Find(ITypeVerification verification, String system, String? prefix, String unit)
    {
        Type? prefixType = prefix is null ? null : prefixes[prefix];
        return system switch {
            "si" => GetMethod(nameof(CreateSi), verification.Verify(siUnits[unit]), prefixType),
            "metric" => GetMethod(nameof(CreateMetric), verification.Verify(metricUnits[unit]), prefixType),
            "imperial" => GetMethod(nameof(CreateImperial), verification.Verify(imperialUnits[unit]), prefixType),
            "any" => GetMethod(nameof(CreateNonStandard), verification.Verify(nonStandardUnits[unit]), prefixType),
            _ => throw new NotImplementedException($"{system} cannot be deserialized yet :-(")
        };
    }

    private static Creator CreateSi<TSi>() where TSi : ISiUnit => i => i.Inject<Si<TSi>>();
    private static Creator CreateSiAlias<TSi, TDim>() where TSi : ISiUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => i => i.Inject<Si<TSi>, Alias<TSi, TDim>>();
    private static Creator CreateSi<TPrefix, TSi>() where TPrefix : IPrefix where TSi : ISiUnit => i => i.Inject<Si<TPrefix, TSi>>();
    private static Creator CreateSiAlias<TPrefix, TSi, TDim>() where TPrefix : IPrefix where TSi : ISiUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => i => i.Inject<Si<TPrefix, TSi>, Alias<TPrefix, TSi, TDim>>();
    private static Creator CreateMetric<TMetric>() where TMetric : IMetricUnit => i => i.Inject<Metric<TMetric>>();
    private static Creator CreateMetricAlias<TMetric, TDim>() where TMetric : IMetricUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => i => i.Inject<Metric<TMetric>, Alias<TMetric, TDim>>();
    private static Creator CreateMetric<TPrefix, TMetric>() where TPrefix : IPrefix where TMetric : IMetricUnit => i => i.Inject<Metric<TPrefix, TMetric>>();
    private static Creator CreateMetricAlias<TPrefix, TMetric, TDim>() where TPrefix : IPrefix where TMetric : IMetricUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => i => i.Inject<Metric<TPrefix, TMetric>, Alias<TPrefix, TMetric, TDim>>();
    private static Creator CreateImperial<TImperial>() where TImperial : IImperialUnit => i => i.Inject<Imperial<TImperial>>();
    private static Creator CreateImperialAlias<TImperial, TDim>() where TImperial : IImperialUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => i => i.Inject<Imperial<TImperial>, Alias<TImperial, TDim>>();
    private static Creator CreateNonStandard<TNonStandard>() where TNonStandard : INoSystemUnit => i => i.Inject<NonStandard<TNonStandard>>();
    private static Creator CreateNonStandardAlias<TNonStandard, TDim>() where TNonStandard : INoSystemUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => i => i.Inject<NonStandard<TNonStandard>, Alias<TNonStandard, TDim>>();

    private static Dictionary<String, Type> Scan(Type interfaceType)
    {
        var types = new Dictionary<String, Type>();
        foreach (var type in typeof(ScalarBuilder).Assembly.GetExportedTypes().Where(t => t.IsValueType && t.IsAssignableTo(interfaceType))) {
            var representation = GetRepresentation(type);
            types[representation] = type;
        }
        return types;
    }

    private static Creator GetMethod(String name, Type unit, Type? prefix = null)
    {
        var types = GetUnitTypeArgs(unit);
        if (types.Count > 1) {
            name = $"{name}Alias";
        }
        if (prefix != null) {
            types.Insert(0, prefix);
        }
        var typeParameters = types.ToArray();
        var genericMethod = GetGenericMethod(name, typeParameters.Length);
        var createMethod = genericMethod.MakeGenericMethod(typeParameters.ToArray()) ?? throw GetException(name, "created", typeParameters);
        return createMethod.Invoke(null, null) as Creator ?? throw GetException(name, "invoked", typeParameters);

        static InvalidOperationException GetException(String name, String function, Type[] genericArgs)
        {
            return new InvalidOperationException($"Method '{name}<{String.Join(',', genericArgs.Select(t => t.Name))}>' could not be {function}.");
        }

        static List<Type> GetUnitTypeArgs(Type unit)
        {
            var typeArgs = new List<Type> { unit };
            var aliasOf = unit.InnerTypes(typeof(IInjectUnit<>));
            if (aliasOf.Length > 0) {
                typeArgs.Add(aliasOf[0]);
            }
            return typeArgs;
        }
    }

    private static String GetRepresentation(Type type)
    {
        var representation = getRepresentation.MakeGenericMethod(type);
        return (representation?.Invoke(null, null)) as String ?? throw new InvalidOperationException($"Failed getting representation for: {type.Name}");
    }
    private static String GetRepresentation<T>() where T : IRepresentable => T.Representation;
    private static MethodInfo GetGenericMethod(String name, Int32 typeArgumentCount)
    {
        var genericMethod = typeof(ScalarBuilder).GetMethod(name, typeArgumentCount, BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Standard, Type.EmptyTypes, null);
        return genericMethod ?? throw new InvalidOperationException($"Method '{name}' not found");
    }
}
