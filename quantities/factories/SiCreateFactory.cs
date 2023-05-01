using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct SiCreateFactory<TSelf, TDimension> : ISiFactory<TSelf, TDimension>
    where TDimension : Dimensions.IDimension, ILinear
    where TSelf : struct, IQuantity<TSelf>, IQuantityFactory<TSelf, TDimension>, TDimension
{
    private readonly Double value;
    internal SiCreateFactory(in Double value) => this.value = value;
    public TSelf Si<TUnit>() where TUnit : ISiUnit, TDimension => TSelf.Create(this.value.As<Si<TUnit>>());
    public TSelf Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TSelf.Create(this.value.As<Si<TPrefix, TUnit>>());
}
