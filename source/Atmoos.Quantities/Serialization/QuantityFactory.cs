using System.Collections.Concurrent;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;

namespace Atmoos.Quantities.Serialization;

file static class Inject
{
    public static IInject<IBuilder> Quotient { get; } = new QuotientInjector();
    public static IInject<IBuilder> Product { get; } = new ProductInjector();
    public static IInject<IBuilder> Square { get; } = new PowerInjector<Numerator<Two>>();
    public static IInject<IBuilder> Cubic { get; } = new PowerInjector<Numerator<Three>>();
    public static IInject<IBuilder> Scalar { get; } = new ScalarInjector();
    public static IInject<IBuilder> Inverse { get; } = new InverseInjector();
}

public readonly struct QuantityFactory<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private static readonly Type dimension = typeof(IDimension<,>);
    private static readonly Type typeofQuantity = typeof(TQuantity);
    private static readonly ConcurrentDictionary<Int32, IBuilder> complexCache = new();
    private static readonly ConcurrentDictionary<QuantityModel, IBuilder> scalarCache = new();
    private static readonly Type scalarVerification = typeof(TQuantity).MostDerivedOf(typeof(IDimension));
    private readonly IBuilder builder;
    private QuantityFactory(IBuilder builder) : this() => this.builder = builder;
    public TQuantity Build(in Double value)
    {
        return TQuantity.Create(this.builder.Build(in value));
    }

    public static QuantityFactory<TQuantity> Create(UnitRepository repository, in QuantityModel model)
        => new(Builder(repository, in model));
    public static QuantityFactory<TQuantity> Create(UnitRepository repository, List<QuantityModel> models)
    {
        var builder = models switch {
        [QuantityModel model] => Builder(repository, model),
        [QuantityModel l, QuantityModel r] => Builder(repository, l, r),
            _ => throw new NotSupportedException($"Cannot build quantities with '{models.Count}' models.")
        };
        return new(builder);
    }

    private static IBuilder Builder(UnitRepository repository, in QuantityModel model) => model.Exponent switch {
        -1 => Create(repository, in model, PowerOf<Negative<One>>(typeofQuantity), Inject.Inverse),
        1 => Create(repository, in model, scalarVerification, Inject.Scalar),
        2 => Create(repository, in model, PowerOf<Two>(typeofQuantity), Inject.Square),
        3 => Create(repository, in model, PowerOf<Three>(typeofQuantity), Inject.Cubic),
        _ => throw new NotSupportedException($"Cannot build a quantity with an exponent of '{model.Exponent}'.")
    };
    private static IBuilder Builder(UnitRepository repository, in QuantityModel left, in QuantityModel right)
    {
        var (verifications, injector) = (left.Exponent, right.Exponent) switch {
            ( > 0, > 0) => (typeofQuantity.InnerTypes(typeof(IProduct<,>)), Inject.Product),
            ( > 0, < 0) => (typeofQuantity.InnerTypes(typeof(IQuotient<,>)), Inject.Quotient),
            _ => throw new NotSupportedException($"Cannot build a quantity with an exponents of '[{left.Exponent}, {right.Exponent}]'.")
        };
        return Create(repository, [left, right], verifications, injector);
    }

    private static IBuilder Create(UnitRepository repo, in QuantityModel model, Type verification, IInject<IBuilder> injector)
    {
        if (scalarCache.TryGetValue(model, out var builder)) {
            return builder;
        }
        return scalarCache[model] = ScalarBuilder.Create(in model, in repo, new TypeVerification(verification), injector);
    }
    private static IBuilder Create(UnitRepository repo, QuantityModel[] models, Type[] verifications, IInject<IBuilder> injector)
    {
        Int32 key = ComputeAggregateKey(models);
        if (complexCache.TryGetValue(key, out var builder)) {
            return builder;
        }
        var verification = new TypeVerification(verifications[0]);
        builder = ScalarBuilder.Create(in models[0], in repo, in verification, injector);
        for (Int32 index = 1; index < verifications.Length; ++index) {
            verification = new TypeVerification(verifications[index]);
            injector = builder as IInject<IBuilder> ?? throw new InvalidOperationException("Need another injector...");
            builder = ScalarBuilder.Create(in models[index], in repo, in verification, injector);
        }
        return complexCache[key] = builder;

        // ToDo: Check if this is still required...
        static Int32 ComputeAggregateKey(QuantityModel[] models)
        {
            const Int32 notZero = 2;
            Int32 key = models.Length;
            for (Int32 modelNumber = 0; modelNumber < models.Length; modelNumber++) {
                unchecked {
                    // The hash of each model is multiplied by it's position to get
                    // a key that is sensitive to the order of the models
                    // i.e. such that "km/h" and "h/km" result in different keys.
                    key ^= (modelNumber + notZero) * models[modelNumber].GetHashCode();
                }
            }
            return key;
        }
    }

    // ToDo: This is executed for every quantity, so it might be worth to cache the results...
    private static Type PowerOf<TExponent>(Type type)
        where TExponent : INumber
    {
        return type.InnerTypes(dimension) switch {
            { Length: < 2 } => throw new InvalidOperationException($"Expected '{type.Name}' to implement {nameof(IDimension)}<~,{typeof(TExponent).Name}>, but found less than 2 generic arguments."),
            [Type linear, Type exponent] when exponent == typeof(TExponent) => linear,
            [Type linear, Type exp, ..] => throw new InvalidOperationException($"Expected '{type.Name}' to be {linear.Name} to the power of {typeof(TExponent).Name}, but got a power of '{exp.Name}'."),
        };
    }
}
