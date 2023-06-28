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
    private static readonly MethodInfo getRepresentation = GetGenericMethod(nameof(GetRepresentation), 1);
    private static readonly Dictionary<String, Type> prefixes = Scan(typeof(IPrefix));
    private static readonly Dictionary<String, Type> siUnits = Scan(typeof(ISiUnit));
    private static readonly Dictionary<String, Type> metricUnits = Scan(typeof(IMetricUnit));
    private static readonly Dictionary<String, Type> imperialUnits = Scan(typeof(IImperialUnit));
    private static readonly Dictionary<String, Type> nonStandardUnits = Scan(typeof(INoSystemUnit));
    public static IBuilder Create(in QuantityModel model, TypeVerification verification)
    {
        return Create(verification, model.System, model.Prefix is null ? null : prefixes[model.Prefix], model.Unit);

        static IBuilder Create(TypeVerification verification, String system, Type? prefix, String unit) => system switch {
            "si" => GetMethod(nameof(CreateSi), verification.Verify(siUnits[unit]), prefix),
            "metric" => GetMethod(nameof(CreateMetric), verification.Verify(metricUnits[unit]), prefix),
            "imperial" => GetMethod(nameof(CreateImperial), verification.Verify(imperialUnits[unit]), prefix),
            "any" => GetMethod(nameof(CreateNonStandard), verification.Verify(nonStandardUnits[unit]), prefix),
            _ => throw new NotImplementedException($"{system} cannot be deserialized yet :-(")
        };
    }

    private static IBuilder CreateSi<TSi>() where TSi : ISiUnit => BuilderOf<Si<TSi>>();
    private static IBuilder CreateSiAlias<TSi, TDim>() where TSi : ISiUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => BuilderOf<Si<TSi>, Alias<TSi, TDim>>();
    private static IBuilder CreateSi<TPrefix, TSi>() where TPrefix : IPrefix where TSi : ISiUnit => BuilderOf<Si<TPrefix, TSi>>();
    private static IBuilder CreateSiAlias<TPrefix, TSi, TDim>() where TPrefix : IPrefix where TSi : ISiUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => BuilderOf<Si<TPrefix, TSi>, Alias<TPrefix, TSi, TDim>>();
    private static IBuilder CreateMetric<TMetric>() where TMetric : IMetricUnit => BuilderOf<Metric<TMetric>>();
    private static IBuilder CreateMetricAlias<TMetric, TDim>() where TMetric : IMetricUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => BuilderOf<Metric<TMetric>, Alias<TMetric, TDim>>();
    private static IBuilder CreateMetric<TPrefix, TMetric>() where TPrefix : IPrefix where TMetric : IMetricUnit => BuilderOf<Metric<TPrefix, TMetric>>();
    private static IBuilder CreateMetricAlias<TPrefix, TMetric, TDim>() where TPrefix : IPrefix where TMetric : IMetricUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => BuilderOf<Metric<TPrefix, TMetric>, Alias<TPrefix, TMetric, TDim>>();
    private static IBuilder CreateImperial<TImperial>() where TImperial : IImperialUnit => BuilderOf<Imperial<TImperial>>();
    private static IBuilder CreateImperialAlias<TImperial, TDim>() where TImperial : IImperialUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => BuilderOf<Imperial<TImperial>, Alias<TImperial, TDim>>();
    private static IBuilder CreateNonStandard<TNonStandard>() where TNonStandard : INoSystemUnit => BuilderOf<NonStandard<TNonStandard>>();
    private static IBuilder CreateNonStandardAlias<TNonStandard, TDim>() where TNonStandard : INoSystemUnit, IInjectUnit<TDim> where TDim : Dimensions.IDimension => BuilderOf<NonStandard<TNonStandard>, Alias<TNonStandard, TDim>>();
    private static IBuilder BuilderOf<TMeasure>() where TMeasure : IMeasure => new Builder<TMeasure>();
    private static IBuilder BuilderOf<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => new AliasedBuilder<TMeasure, TAlias>();

    private static IBuilder GetMethod(String name, Type unit, Type? prefix = null)
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
        var createMethod = genericMethod.MakeGenericMethod(typeParameters) ?? throw GetException(name, "created", typeParameters);
        return createMethod.Invoke(null, null) as IBuilder ?? throw GetException(name, "invoked", typeParameters);

        static InvalidOperationException GetException(String name, String function, Type[] genericArgs)
        {
            return new InvalidOperationException($"Method '{name}<{String.Join(',', genericArgs.Select(t => t.Name))}>' could not be {function}.");
        }

        static List<Type> GetUnitTypeArgs(Type unit)
        {
            var aliasOf = unit.InnerTypes(typeof(IInjectUnit<>));
            return aliasOf.Length == 0 ? new List<Type> { unit } : new List<Type> { unit, aliasOf[0] };
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
    private static Dictionary<String, Type> Scan(Type interfaceType)
    {
        var types = new Dictionary<String, Type>();
        foreach (var type in typeof(ScalarBuilder).Assembly.GetExportedTypes().Where(t => t.IsValueType && t.IsAssignableTo(interfaceType))) {
            var representation = GetRepresentation(type);
            types[representation] = type;
        }
        return types;
    }
}
