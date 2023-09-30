using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities.Factories;

public readonly struct SiOnly<TCreate, TQuantity, TDimension> : ISiFactory<TQuantity, TDimension>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TDimension : IDimension
{
    private readonly TCreate creator;
    internal SiOnly(in TCreate creator) => this.creator = creator;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.creator.Create<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.creator.Create<Si<TPrefix, TUnit>>());
}
