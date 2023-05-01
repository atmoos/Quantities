using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct SiTo<TQuantity, TDimension> : ISiFactory<TQuantity, TDimension>
    where TDimension : Dimensions.IDimension, ILinear
    where TQuantity : IFactory<TQuantity>
{
    private readonly Quant value;
    internal SiTo(in Quant value) => this.value = value;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TPrefix, TUnit>>());
}
public readonly struct SiCreate<TQuantity, TDimension> : ISiFactory<TQuantity, TDimension>
    where TDimension : Dimensions.IDimension, ILinear
    where TQuantity : IFactory<TQuantity>
{
    private readonly Double value;
    internal SiCreate(in Double value) => this.value = value;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TPrefix, TUnit>>());
}
