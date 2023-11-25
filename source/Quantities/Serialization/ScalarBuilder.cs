using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using static Quantities.Serialization.Reflection;
using IDim = Quantities.Dimensions.IDimension;

namespace Quantities.Serialization;

internal static class ScalarBuilder
{
    private delegate IBuilder Creator(IInject<IBuilder> injector);
    public static IBuilder Create(in QuantityModel model, in UnitRepository repo, in TypeVerification verification, IInject<IBuilder> injector)
    {
        return Create(in repo, in verification, model.System, model.Prefix is null ? null : repo.Prefix(model.Prefix), model.Unit)(injector);

        static Creator Create(in UnitRepository repo, in TypeVerification verification, String system, Type? prefix, String unit) => system switch {
            "si" => GetMethod(nameof(CreateSi), verification.Verify(repo.Si(unit)), prefix),
            "metric" => GetMethod(nameof(CreateMetric), verification.Verify(repo.Metric(unit)), prefix),
            "imperial" => GetMethod(nameof(CreateImperial), verification.Verify(repo.Imperial(unit)), prefix),
            "any" => GetMethod(nameof(CreateNonStandard), verification.Verify(repo.NonStandard(unit)), prefix),
            _ => throw new NotImplementedException($"{system} cannot be deserialized yet :-(")
        };
    }

    private static Creator CreateSi<TSi>() where TSi : ISiUnit, IDim => i => i.Inject<Si<TSi>>();
    private static Creator CreateSiAlias<TSi, TDim>() where TSi : ISiUnit, IDim, IAlias<TDim> where TDim : IDim, ILinear => i => Injector<TSi, TDim>(i).Inject<Si<TSi>>();
    private static Creator CreateSi<TPrefix, TSi>() where TPrefix : IPrefix where TSi : ISiUnit, IDim => i => i.Inject<Si<TPrefix, TSi>>();
    private static Creator CreateSiAlias<TPrefix, TSi, TDim>() where TPrefix : IPrefix where TSi : ISiUnit, IDim, IAlias<TDim> where TDim : IDim, ILinear => i => Injector<TSi, TDim>(i).Inject<Si<TPrefix, TSi>>();
    private static Creator CreateMetric<TMetric>() where TMetric : IMetricUnit, IDim => i => i.Inject<Metric<TMetric>>();
    private static Creator CreateMetricAlias<TMetric, TDim>() where TMetric : IMetricUnit, IDim, IAlias<TDim> where TDim : IDim, ILinear => i => Injector<TMetric, TDim>(i).Inject<Metric<TMetric>>();
    private static Creator CreateMetric<TPrefix, TMetric>() where TPrefix : IPrefix where TMetric : IMetricUnit, IDim => i => i.Inject<Metric<TPrefix, TMetric>>();
    private static Creator CreateMetricAlias<TPrefix, TMetric, TDim>() where TPrefix : IPrefix where TMetric : IMetricUnit, IDim, IAlias<TDim> where TDim : IDim, ILinear => i => Injector<TMetric, TDim>(i).Inject<Metric<TPrefix, TMetric>>();
    private static Creator CreateImperial<TImperial>() where TImperial : IImperialUnit, IDim => i => i.Inject<Imperial<TImperial>>();
    private static Creator CreateImperialAlias<TImperial, TDim>() where TImperial : IImperialUnit, IDim, IAlias<TDim> where TDim : IDim, ILinear => i => Injector<TImperial, TDim>(i).Inject<Imperial<TImperial>>();
    private static Creator CreateNonStandard<TNonStandard>() where TNonStandard : INonStandardUnit, IDim => i => i.Inject<NonStandard<TNonStandard>>();
    private static Creator CreateNonStandardAlias<TNonStandard, TDim>() where TNonStandard : INonStandardUnit, IDim, IAlias<TDim> where TDim : IDim, ILinear => i => Injector<TNonStandard, TDim>(i).Inject<NonStandard<TNonStandard>>();
    private static IInject<IBuilder> Injector<TUnit, TDim>(IInject<IBuilder> injector)
        where TDim : IDim, ILinear where TUnit : IAlias<TDim> => TUnit.Inject(new AliasInjectorFactory<TDim>(injector));
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
        var genericMethod = GetGenericMethod(typeof(ScalarBuilder), name, typeParameters.Length);
        var createMethod = genericMethod.MakeGenericMethod(typeParameters) ?? throw GetException(name, "created", typeParameters);
        return createMethod.Invoke(null, null) as Creator ?? throw GetException(name, "invoked", typeParameters);

        static InvalidOperationException GetException(String name, String function, Type[] genericArgs)
        {
            return new InvalidOperationException($"Method '{name}<{String.Join(',', genericArgs.Select(t => t.Name))}>' could not be {function}.");
        }

        static List<Type> GetUnitTypeArgs(Type unit)
        {
            var aliasOf = unit.InnerTypes(typeof(IAlias<>));
            return aliasOf.Length == 0 ? new List<Type> { unit } : new List<Type> { unit, aliasOf[0] };
        }
    }
}
