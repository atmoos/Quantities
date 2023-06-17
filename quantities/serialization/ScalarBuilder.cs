using System.Reflection;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Serialization;

internal sealed class ScalarBuilder : IBuilder
{
    internal delegate IBuilder Create(IInject inject);
    private static readonly MethodInfo getRepresentation = GetGenericMethod(nameof(GetRepresentation), 1);
    private static readonly Dictionary<String, Type> prefixes = Scan(typeof(IPrefix));
    private static readonly Dictionary<String, Type> siUnits = Scan(typeof(ISiUnit));
    private static readonly Dictionary<String, Type> metricUnits = Scan(typeof(IMetricUnit));
    private static readonly Dictionary<String, Type> imperialUnits = Scan(typeof(IImperialUnit));
    private static readonly Dictionary<String, Type> nonStandardUnits = Scan(typeof(INoSystemUnit));
    private readonly QuantityModel model;
    public ScalarBuilder(in QuantityModel model) => this.model = model;
    public IBuilder Append(IInject inject) => Find(this.model.System, this.model.Prefix, this.model.Unit)(inject);
    public Quant Build(in Double value) => Append(new ScalarInjector()).Build(in value);
    private Create Find(String system, String? prefix, String unit)
    {
        Type? prefixType = prefix is null ? null : prefixes[prefix];
        return system switch {
            "si" => GetMethod(nameof(CreateSi), siUnits[unit], prefixType),
            "metric" => GetMethod(nameof(CreateMetric), metricUnits[unit], prefixType),
            "imperial" => GetMethod(nameof(CreateImperial), imperialUnits[unit], prefixType),
            "any" => GetMethod(nameof(CreateNonStandard), nonStandardUnits[unit], prefixType),
            _ => throw new NotImplementedException($"{system} cannot be deserialized yet :-(")
        };
    }

    private static Create CreateSi<TSi>() where TSi : ISiUnit => i => i.Inject<Si<TSi>>();
    private static Create CreateSi<TPrefix, TSi>() where TPrefix : IPrefix where TSi : ISiUnit => i => i.Inject<Si<TPrefix, TSi>>();
    private static Create CreateMetric<TMetric>() where TMetric : IMetricUnit => i => i.Inject<Metric<TMetric>>();
    private static Create CreateMetric<TPrefix, TMetric>() where TPrefix : IPrefix where TMetric : IMetricUnit => i => i.Inject<Metric<TPrefix, TMetric>>();
    private static Create CreateImperial<TImperial>() where TImperial : IImperialUnit => i => i.Inject<Imperial<TImperial>>();
    private static Create CreateNonStandard<TNonStandard>() where TNonStandard : INoSystemUnit => i => i.Inject<NonStandard<TNonStandard>>();

    private static Dictionary<String, Type> Scan(Type interfaceType)
    {
        var types = new Dictionary<String, Type>();
        var interfaceName = interfaceType.FullName ?? String.Empty;
        foreach (var type in typeof(ScalarBuilder).Assembly.GetExportedTypes().Where(t => t.IsValueType && t.GetInterface(interfaceName) is not null)) {
            var representation = GetRepresentation(type);
            types[representation] = type;
        }
        return types;
    }

    private static Create GetMethod(String name, Type unit, Type? prefix = null)
    {
        var typeParameters = prefix is null ? new[] { unit } : new[] { prefix, unit };
        var genericMethod = GetGenericMethod(name, typeParameters.Length);
        var createMethod = genericMethod.MakeGenericMethod(typeParameters) ?? throw GetException(name, "created", typeParameters);
        return createMethod.Invoke(null, null) as Create ?? throw GetException(name, "invoked", typeParameters);

        static InvalidOperationException GetException(String name, String function, Type[] genericArgs)
        {
            return new InvalidOperationException($"Method '{name}<{String.Join(',', genericArgs.Select(t => t.Name))}>' could not be {function}.");
        }
    }
    private static String GetRepresentation(Type type)
    {
        var representation = getRepresentation.MakeGenericMethod(type);
        return (representation?.Invoke(null, null)) as String ?? throw new InvalidOperationException($"Failed getting representation for: {type.Name}");
    }
    private static String GetRepresentation<T>()
        where T : IRepresentable => T.Representation;
    private static MethodInfo GetGenericMethod(String name, Int32 typeArgumentCount)
    {
        var genericMethod = typeof(ScalarBuilder).GetMethod(name, typeArgumentCount, BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Standard, Type.EmptyTypes, null);
        return genericMethod ?? throw new InvalidOperationException($"Method '{name}' not found");
    }
}

internal sealed class ScalarBuilder<TMeasure> : IBuilder
    where TMeasure : IMeasure
{
    public IBuilder Append(IInject inject) => inject.Inject<TMeasure>();
    public Quant Build(in Double value) => Build<TMeasure>.With(in value);
}
