using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Serialization;

namespace Atmoos.Quantities.Core.Construction;

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
    where TQuantity : IFactory<TQuantity>, IDimension
{
    private static readonly Cache<QuantityModel, IBuilder> scalarCache = new();
    private static readonly Cache<QuantityModel, QuantityModel, IBuilder> complexCache = new();
    private readonly IBuilder builder;

    private QuantityFactory(IBuilder builder)
        : this() => this.builder = builder;

    public TQuantity Build(in Double value)
    {
        var quantity = this.builder.Build(in value);
        if (quantity.IsOf<TQuantity>()) {
            return TQuantity.Create(in quantity);
        }

        throw new InvalidOperationException($"Cannot build a quantity of type '{typeof(TQuantity).Name}' from the provided models. The dimensions do not match.");
    }

    public static QuantityFactory<TQuantity> Create(UnitRepository repository, in QuantityModel model) => new(ScalarBuilder(repository, in model));

    public static QuantityFactory<TQuantity> Create(UnitRepository repository, List<QuantityModel> models)
    {
        var builder = models switch {
            [QuantityModel model] => ScalarBuilder(repository, in model),
            [QuantityModel l, QuantityModel r] => ProductBuilder(repository, in l, in r),
            _ => throw new NotSupportedException($"Cannot build quantities with '{models.Count}' models."),
        };
        return new(builder);
    }

    private static IBuilder ScalarBuilder(UnitRepository repository, in QuantityModel model) =>
        model.Exponent switch {
            -1 => Create<Negative<One>>(repository, in model, Inject.Inverse),
            1 => Create(repository, in model, Inject.Scalar),
            2 => Create<Two>(repository, in model, Inject.Square),
            3 => Create<Three>(repository, in model, Inject.Cubic),
            4 => Create<Four>(repository, in model, Inject.Quartic),
            5 => Create<Five>(repository, in model, Inject.Quintic),
            _ => throw new NotSupportedException($"Cannot build a quantity with an exponent of '{model.Exponent}'."),
        };

    private static IBuilder ProductBuilder(UnitRepository repository, in QuantityModel left, in QuantityModel right)
    {
        const Int32 terms = 2; // a product has two terms.
        return complexCache.Get(left, right, repository, CreateBuilder);

        static IBuilder CreateBuilder(QuantityModel left, QuantityModel right, UnitRepository repo)
        {
            QuantityModel[] models = [left, right];
            ;
            IInject<IBuilder> injector = new ProductInjector(left.Exponent, right.Exponent);
            var builder = Construction.ScalarBuilder.Create(in left, in repo, injector);
            for (Int32 index = 1; index < terms; ++index) {
                injector = builder as IInject<IBuilder> ?? throw new InvalidOperationException("Need another injector...");
                builder = Construction.ScalarBuilder.Create(in models[index], in repo, injector);
            }
            return builder;
        }
    }

    private static IBuilder Create(UnitRepository repo, in QuantityModel model, IInject<IBuilder> injector)
    {
        return scalarCache.Get(model, (repo, injector), CreateBuilder);

        static IBuilder CreateBuilder(QuantityModel model, (UnitRepository repo, IInject<IBuilder> injector) arg) => Construction.ScalarBuilder.Create(in model, in arg.repo, arg.injector);
    }

    private static IBuilder Create<TExponent>(UnitRepository repo, in QuantityModel model, IInject<IBuilder> injector)
        where TExponent : INumber
    {
        return scalarCache.Get(model, (repo, injector), CreateBuilder);

        static IBuilder CreateBuilder(QuantityModel model, (UnitRepository repo, IInject<IBuilder> injector) arg) => Construction.ScalarBuilder.Create(in model, in arg.repo, arg.injector);
    }
}
