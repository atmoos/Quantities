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
    private static Creator CreateSiAlias<TSi, TDim>() where TSi : ISiUnit, IDim, IPowerOf<TDim> where TDim : IDim, ILinear => i => AliasInjector<TSi, TDim>(i).Inject<Si<TSi>>();
    private static Creator CreateSiInvertible<TSi, TDim>() where TSi : ISiUnit, IDim, IInvertible<TDim> where TDim : IDim, ILinear => i => InvertibleInjector<TSi, TDim>(i).Inject<Si<TSi>>();
    private static Creator CreateSi<TPrefix, TSi>() where TPrefix : IPrefix where TSi : ISiUnit, IDim => i => i.Inject<Si<TPrefix, TSi>>();
    private static Creator CreateSiAlias<TPrefix, TSi, TDim>() where TPrefix : IPrefix where TSi : ISiUnit, IDim, IPowerOf<TDim> where TDim : IDim, ILinear => i => AliasInjector<TSi, TDim>(i).Inject<Si<TPrefix, TSi>>();
    private static Creator CreateSiInvertible<TPrefix, TSi, TDim>() where TPrefix : IPrefix where TSi : ISiUnit, IDim, IInvertible<TDim> where TDim : IDim, ILinear => i => InvertibleInjector<TSi, TDim>(i).Inject<Si<TPrefix, TSi>>();
    private static Creator CreateMetric<TMetric>() where TMetric : IMetricUnit, IDim => i => i.Inject<Metric<TMetric>>();
    private static Creator CreateMetricAlias<TMetric, TDim>() where TMetric : IMetricUnit, IDim, IPowerOf<TDim> where TDim : IDim, ILinear => i => AliasInjector<TMetric, TDim>(i).Inject<Metric<TMetric>>();
    private static Creator CreateMetricInvertible<TMetric, TDim>() where TMetric : IMetricUnit, IDim, IInvertible<TDim> where TDim : IDim, ILinear => i => InvertibleInjector<TMetric, TDim>(i).Inject<Metric<TMetric>>();
    private static Creator CreateMetric<TPrefix, TMetric>() where TPrefix : IPrefix where TMetric : IMetricUnit, IDim => i => i.Inject<Metric<TPrefix, TMetric>>();
    private static Creator CreateMetricAlias<TPrefix, TMetric, TDim>() where TPrefix : IPrefix where TMetric : IMetricUnit, IDim, IPowerOf<TDim> where TDim : IDim, ILinear => i => AliasInjector<TMetric, TDim>(i).Inject<Metric<TPrefix, TMetric>>();
    private static Creator CreateMetricInvertible<TPrefix, TMetric, TDim>() where TPrefix : IPrefix where TMetric : IMetricUnit, IDim, IInvertible<TDim> where TDim : IDim, ILinear => i => InvertibleInjector<TMetric, TDim>(i).Inject<Metric<TPrefix, TMetric>>();
    private static Creator CreateImperial<TImperial>() where TImperial : IImperialUnit, IDim => i => i.Inject<Imperial<TImperial>>();
    private static Creator CreateImperialAlias<TImperial, TDim>() where TImperial : IImperialUnit, IDim, IPowerOf<TDim> where TDim : IDim, ILinear => i => AliasInjector<TImperial, TDim>(i).Inject<Imperial<TImperial>>();
    private static Creator CreateImperialInvertible<TImperial, TDim>() where TImperial : IImperialUnit, IDim, IInvertible<TDim> where TDim : IDim, ILinear => i => InvertibleInjector<TImperial, TDim>(i).Inject<Imperial<TImperial>>();
    private static Creator CreateNonStandard<TNonStandard>() where TNonStandard : INonStandardUnit, IDim => i => i.Inject<NonStandard<TNonStandard>>();
    private static Creator CreateNonStandardAlias<TNonStandard, TDim>() where TNonStandard : INonStandardUnit, IDim, IPowerOf<TDim> where TDim : IDim, ILinear => i => AliasInjector<TNonStandard, TDim>(i).Inject<NonStandard<TNonStandard>>();
    private static Creator CreateNonStandardInvertible<TNonStandard, TDim>() where TNonStandard : INonStandardUnit, IDim, IInvertible<TDim> where TDim : IDim, ILinear => i => InvertibleInjector<TNonStandard, TDim>(i).Inject<NonStandard<TNonStandard>>();
    private static IInject<IBuilder> AliasInjector<TUnit, TDim>(IInject<IBuilder> injector)
        where TDim : IDim, ILinear where TUnit : IPowerOf<TDim> => TUnit.Inject(new InjectorFactory<AliasCreator, TDim>(injector));
    private static IInject<IBuilder> InvertibleInjector<TUnit, TDim>(IInject<IBuilder> injector)
        where TDim : IDim, ILinear where TUnit : IInvertible<TDim> => TUnit.Inject(new InjectorFactory<InversionCreator, TDim>(injector));
    private static Creator GetMethod(String name, Type unit, Type? prefix = null)
    {
        (var types, name) = GetUnitTypeArgs(unit, name);
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

        static (List<Type> types, String name) GetUnitTypeArgs(Type unit, String name)
        {
            var aliasOf = unit.InnerTypes(typeof(IPowerOf<>));
            var inversionOf = unit.InnerTypes(typeof(IInvertible<>));
            return (aliasOf, inversionOf) switch {
                ([Type alias], _) => (new List<Type> { unit, alias }, $"{name}Alias"),
                (_, [Type inverse]) => (new List<Type> { unit, inverse }, $"{name}Invertible"),
                _ => (new List<Type> { unit }, name)
            };
        }
    }
}

file readonly struct AliasCreator : ICreateInjectable
{
    public static IInject<IBuilder> Create<TMeasure>(IInject<IBuilder> builder)
        where TMeasure : IMeasure, ILinear => new AliasInjector<TMeasure>(builder);
}

file readonly struct InversionCreator : ICreateInjectable
{
    public static IInject<IBuilder> Create<TMeasure>(IInject<IBuilder> builder)
        where TMeasure : IMeasure, ILinear => new InversionInjector<TMeasure>(builder);
}
