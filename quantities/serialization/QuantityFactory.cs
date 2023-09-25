using System.Collections.Concurrent;
using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Serialization;

file static class Inject
{
    public static IInject Quotient { get; } = new QuotientInjector();
    public static IInject Product { get; } = new ProductInjector();
    public static IInject Square { get; } = new PowerInjector<Square>();
    public static IInject Cubic { get; } = new PowerInjector<Cubic>();
    public static IInject Scalar { get; } = new ScalarInjector();
}

public readonly struct QuantityFactory<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private static readonly Type typeofQuantity = typeof(TQuantity);
    private static readonly ConcurrentDictionary<Int32, IBuilder> complexCache = new();
    private static readonly ConcurrentDictionary<QuantityModel, IBuilder> scalarCache = new();
    private static readonly Type scalarVerification = typeof(TQuantity).MostDerivedOf(typeof(Dimensions.IDimension));
    private readonly IInject injector;
    private readonly Type[] verifications;
    public Int32 ExpectedModelCount => this.verifications.Length;
    public Boolean IsScalarQuantity { get; }
    private QuantityFactory(IInject injector, params Type[] verifications) : this()
    {
        this.injector = injector;
        this.verifications = verifications;
        this.IsScalarQuantity = injector is ScalarInjector;
    }
    public TQuantity Build(in Double value, in QuantityModel deserializedQuantity)
    {
        if (this.verifications.Length != 1) {
            throw new InvalidOperationException($"Expected to build a quantity using '{this.verifications.Length}' sub models, but attempted to build with only one.");
        }
        var builder = Create(in deserializedQuantity, this.verifications[0], this.injector);
        return TQuantity.Create(builder.Build(in value));
    }
    public TQuantity Build(in Double value, QuantityModel[] deserializedQuantity)
    {
        if (deserializedQuantity.Length != this.verifications.Length) {
            throw new ArgumentOutOfRangeException(nameof(deserializedQuantity), $"Expected '{this.verifications.Length}' quantity sub models, but received only '{deserializedQuantity.Length}'.");
        }
        var builder = Create(deserializedQuantity, this.verifications, this.injector);
        return TQuantity.Create(builder.Build(in value));
    }

    public static QuantityFactory<TQuantity> Create(String system) => system switch {
        "quotient" => new(Inject.Quotient, typeofQuantity.InnerTypes(typeof(IQuotient<,>))),
        "product" => new(Inject.Product, typeofQuantity.InnerTypes(typeof(IProduct<,>))),
        "square" => new(Inject.Square, typeofQuantity.InnerType(typeof(ISquare<>))),
        "cubic" => new(Inject.Cubic, typeofQuantity.InnerType(typeof(ICubic<>))),
        _ => new(Inject.Scalar, scalarVerification)
    };

    private static IBuilder Create(in QuantityModel model, Type verification, IInject injector)
    {
        if (scalarCache.TryGetValue(model, out var builder)) {
            return builder;
        }
        return scalarCache[model] = ScalarBuilder.Create(in model, new TypeVerification(verification), injector);
    }
    private static IBuilder Create(QuantityModel[] models, Type[] verifications, IInject injector)
    {
        Int32 key = ComputeAggregateKey(models);
        if (complexCache.TryGetValue(key, out var builder)) {
            return builder;
        }
        var verification = new TypeVerification(verifications[0]);
        builder = ScalarBuilder.Create(in models[0], in verification, injector);
        for (Int32 index = 1; index < verifications.Length; ++index) {
            verification = new TypeVerification(verifications[index]);
            injector = builder as IInject ?? throw new InvalidOperationException("Need another injector...");
            builder = ScalarBuilder.Create(in models[index], in verification, injector);
        }
        return complexCache[key] = builder;

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
}
