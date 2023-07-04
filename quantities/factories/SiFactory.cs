using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct SiFac<TCreate, TQuantity, TDimension> : ISiFactory<TQuantity, TDimension>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TDimension : Dimensions.IDimension, ILinear
{
    private readonly TCreate create;
    internal SiFac(in TCreate create) => this.create = create;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.create.Create<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.create.Create<Si<TPrefix, TUnit>>());
}
