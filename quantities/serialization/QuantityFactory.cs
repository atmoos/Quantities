using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Serialization;

public readonly struct QuantityFactory<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private static readonly Type typeofQuantity = typeof(TQuantity);
    private static readonly Type scalarVerification = typeof(TQuantity).MostDerivedOf<Dimensions.IDimension>();
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
        var verification = new TypeVerification(this.verifications[0]);
        var builder = ScalarBuilder.Create(in deserializedQuantity, verification, this.injector);
        return TQuantity.Create(builder.Build(in value));
    }
    public TQuantity Build(in Double value, QuantityModel[] deserializedQuantity)
    {
        if (deserializedQuantity.Length != this.verifications.Length) {
            throw new ArgumentOutOfRangeException(nameof(deserializedQuantity), $"Expected '{this.verifications.Length}' quantity sub models, but received only '{deserializedQuantity.Length}'.");
        }
        var injector = this.injector;
        var verification = new TypeVerification(this.verifications[0]);
        var builder = ScalarBuilder.Create(in deserializedQuantity[0], verification, injector);
        for (Int32 index = 1; index < this.verifications.Length; ++index) {
            verification = new TypeVerification(this.verifications[index]);
            injector = builder as IInject ?? throw new InvalidOperationException("Need another injector...");
            builder = ScalarBuilder.Create(in deserializedQuantity[index], verification, injector);
        }
        return TQuantity.Create(builder.Build(in value));
    }

    public static QuantityFactory<TQuantity> Create(String system) => system switch {
        "frac" => new(new FractionInjector(), typeofQuantity.InnerTypes(typeof(IFraction<,>))),
        "prod" => new(new ProductInjector(), typeofQuantity.InnerTypes(typeof(IProduct<,>))),
        "square" => new(new PowerInjector<Square>(), typeofQuantity.InnerType(typeof(ISquare<>))),
        "cubic" => new(new PowerInjector<Cubic>(), typeofQuantity.InnerType(typeof(ICubic<>))),
        _ => new(new ScalarInjector(), scalarVerification)
    };
}
