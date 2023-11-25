using System.Collections.Concurrent;
using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Serialization;

file static class Inject
{
    public static IInject<IBuilder> Quotient { get; } = new QuotientInjector();
    public static IInject<IBuilder> Product { get; } = new ProductInjector();
    public static IInject<IBuilder> Square { get; } = new PowerInjector<Square>();
    public static IInject<IBuilder> Cubic { get; } = new PowerInjector<Cubic>();
    public static IInject<IBuilder> Scalar { get; } = new ScalarInjector();
}

public readonly struct QuantityFactory<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private static readonly Type typeofQuantity = typeof(TQuantity);
    private static readonly ConcurrentDictionary<Int32, IBuilder> complexCache = new();
    private static readonly ConcurrentDictionary<QuantityModel, IBuilder> scalarCache = new();
    private static readonly Type scalarVerification = typeof(TQuantity).MostDerivedOf(typeof(Dimensions.IDimension));
    private readonly IInject<IBuilder> injector;
    private readonly Type[] verifications;
    private readonly UnitRepository repository;
    public Int32 ExpectedModelCount => this.verifications.Length;
    public Boolean IsScalarQuantity { get; }
    private QuantityFactory(IInject<IBuilder> injector, UnitRepository repository, params Type[] verifications) : this()
    {
        this.injector = injector;
        this.repository = repository;
        this.verifications = verifications;
        this.IsScalarQuantity = injector is ScalarInjector;
    }
    public TQuantity Build(in Double value, in QuantityModel deserializedQuantity)
    {
        if (this.verifications.Length != 1) {
            throw new InvalidOperationException($"Expected to build a quantity using '{this.verifications.Length}' sub models, but attempted to build with only one.");
        }
        var builder = Create(this.repository, in deserializedQuantity, this.verifications[0], this.injector);
        return TQuantity.Create(builder.Build(in value));
    }
    public TQuantity Build(in Double value, QuantityModel[] deserializedQuantity)
    {
        if (deserializedQuantity.Length != this.verifications.Length) {
            throw new ArgumentOutOfRangeException(nameof(deserializedQuantity), $"Expected '{this.verifications.Length}' quantity sub models, but received only '{deserializedQuantity.Length}'.");
        }
        var builder = Create(this.repository, deserializedQuantity, this.verifications, this.injector);
        return TQuantity.Create(builder.Build(in value));
    }

    public static QuantityFactory<TQuantity> Create(String system, UnitRepository repository) => system switch {
        "quotient" => new(Inject.Quotient, repository, typeofQuantity.InnerTypes(typeof(IQuotient<,>))),
        "product" => new(Inject.Product, repository, typeofQuantity.InnerTypes(typeof(IProduct<,>))),
        "square" => new(Inject.Square, repository, typeofQuantity.InnerType(typeof(ISquare<>))),
        "cubic" => new(Inject.Cubic, repository, typeofQuantity.InnerType(typeof(ICubic<>))),
        _ => new(Inject.Scalar, repository, scalarVerification)
    };

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
