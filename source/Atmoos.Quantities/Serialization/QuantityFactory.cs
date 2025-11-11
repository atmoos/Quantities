using Atmoos.Quantities.Common;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Serialization;

file static class Inject
{
    public static IInject<IBuilder> Square { get; } = new PowerInjector<Two>();
    public static IInject<IBuilder> Cubic { get; } = new PowerInjector<Three>();
    public static IInject<IBuilder> Quartic { get; } = new PowerInjector<Four>();
    public static IInject<IBuilder> Quintic { get; } = new PowerInjector<Five>();
    public static IInject<IBuilder> Scalar { get; } = new ScalarInjector();
    public static IInject<IBuilder> Inverse { get; } = new InverseInjector();
}

public readonly struct QuantityFactory<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private static readonly Type dimension = typeof(IDimension);
    private static readonly Type genericDimension = typeof(IDimension<,>);
    private static readonly Type typeofQuantity = typeof(TQuantity);
    private static readonly Cache<QuantityModel, IBuilder> scalarCache = new();
    private static readonly Cache<QuantityModel, QuantityModel, IBuilder> complexCache = new();
    private static readonly Type scalarVerification = typeof(TQuantity).MostDerivedOf(typeof(IDimension));
    private static readonly Type[] manyVerifications = [.. typeof(TQuantity).InnerTypes(typeof(IProduct<,>)).SelectMany(Unwrap)];
    private readonly IBuilder builder;
    private QuantityFactory(IBuilder builder) : this() => this.builder = builder;
    public TQuantity Build(in Double value) => TQuantity.Create(this.builder.Build(in value));

    public static QuantityFactory<TQuantity> Create(UnitRepository repository, in QuantityModel model)
        => new(ScalarBuilder(repository, in model));
    public static QuantityFactory<TQuantity> Create(UnitRepository repository, List<QuantityModel> models)
    {
        var builder = models switch {
            [QuantityModel model] => ScalarBuilder(repository, in model),
            [QuantityModel l, QuantityModel r] => ProductBuilder(repository, in l, in r),
            _ => throw new NotSupportedException($"Cannot build quantities with '{models.Count}' models.")
        };
        return new(builder);
    }

    private static IBuilder ScalarBuilder(UnitRepository repository, in QuantityModel model) => model.Exponent switch {
        -1 => Create<Negative<One>>(repository, in model, Inject.Inverse),
        1 => Create(repository, in model, scalarVerification, Inject.Scalar),
        2 => Create<Two>(repository, in model, Inject.Square),
        3 => Create<Three>(repository, in model, Inject.Cubic),
        4 => Create<Four>(repository, in model, Inject.Quartic),
        5 => Create<Five>(repository, in model, Inject.Quintic),
        _ => throw new NotSupportedException($"Cannot build a quantity with an exponent of '{model.Exponent}'.")
    };
    private static IBuilder ProductBuilder(UnitRepository repository, in QuantityModel left, in QuantityModel right)
    {
        return complexCache.Get(left, right, repository, static (left, right, repo) =>
        {
            QuantityModel[] models = [left, right];
            var verification = new TypeVerification(manyVerifications[0]);
            IInject<IBuilder> injector = new ProductInjector(left.Exponent, right.Exponent);
            var builder = Serialization.ScalarBuilder.Create(in left, in repo, in verification, injector);
            for (Int32 index = 1; index < manyVerifications.Length; ++index) {
                verification = new TypeVerification(manyVerifications[index]);
                injector = builder as IInject<IBuilder> ?? throw new InvalidOperationException("Need another injector...");
                builder = Serialization.ScalarBuilder.Create(in models[index], in repo, in verification, injector);
            }
            return builder;
        });
    }
    private static IBuilder Create(UnitRepository repo, in QuantityModel model, Type verification, IInject<IBuilder> injector)
    {
        return scalarCache.Get(model, (repo, verification, injector), static (model, arg)
                 => Serialization.ScalarBuilder.Create(in model, in arg.repo, new TypeVerification(arg.verification), arg.injector));
    }

    private static IBuilder Create<TExponent>(UnitRepository repo, in QuantityModel model, IInject<IBuilder> injector)
        where TExponent : INumber
    {
        return scalarCache.Get(model, (repo, injector), static (model, arg) =>
        {
            var verification = new TypeVerification(PowerOf<TExponent>(typeofQuantity));
            return Serialization.ScalarBuilder.Create(in model, in arg.repo, verification, arg.injector);
        });
    }
    private static Type PowerOf<TExponent>(Type type)
        where TExponent : INumber
    {
        return type.InnerTypes(genericDimension) switch {
            { Length: < 2 } => throw new InvalidOperationException($"Expected '{type.Name}' to implement {nameof(IDimension)}<~,{typeof(TExponent).Name}>, but found less than 2 generic arguments."),
            [Type linear, Type exponent] when exponent == typeof(TExponent) => linear,
            [Type linear, Type exp, ..] => throw new InvalidOperationException($"Expected '{type.Name}' to be {linear.Name} to the power of {typeof(TExponent).Name}, but got a power of '{exp.Name}'."),
        };
    }
    private static IEnumerable<Type> Unwrap(Type type)
    {
        if (type.ImplementsGeneric(genericDimension)) {
            return type.InnerTypes(genericDimension).Where(t => t.Implements(dimension));
        }
        return [type];
    }
}
