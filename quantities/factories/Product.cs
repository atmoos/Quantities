using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;
using IDimension = Quantities.Dimensions.IDimension;

namespace Quantities.Factories;

public interface IProductFactory<TProduct, TLeftTerm, TRightTerm> : IFactory
    where TProduct : IProduct<TLeftTerm, TRightTerm>, IDimension
    where TLeftTerm : IDimension, ILinear
    where TRightTerm : IDimension, ILinear
{ /* marker interface */ }

public readonly struct LeftTerm<TCreate, TQuantity, TRightTerm>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TRightTerm : IDimension, ILinear
{
    public Composite<TCreate, TQuantity, TRightTerm> Times { get; }
    internal LeftTerm(in TCreate creator, IInject<TCreate> inject) => this.Times = new(in creator, inject);
}

public readonly struct Product<TCreate, TQuantity, TProduct, TLeftTerm, TRightTerm> : IDefaultFactory<LeftTerm<TCreate, TQuantity, TRightTerm>, TLeftTerm>, IProductFactory<TProduct, TLeftTerm, TRightTerm>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>, TProduct
    where TProduct : IProduct<TLeftTerm, TRightTerm>, IDimension
    where TLeftTerm : IDimension, ILinear
    where TRightTerm : IDimension, ILinear
{
    private readonly TCreate creator;
    public Linear<TCreate, TQuantity, TProduct> Linear => new(in this.creator);
    internal Product(in TCreate creator) => this.creator = creator;
    public LeftTerm<TCreate, TQuantity, TRightTerm> Imperial<TUnit>() where TUnit : IImperialUnit, TLeftTerm => new(in this.creator, AllocationFree<ProductInjector<TCreate, Imperial<TUnit>>>.Item);
    public LeftTerm<TCreate, TQuantity, TRightTerm> Metric<TUnit>() where TUnit : IMetricUnit, TLeftTerm => new(in this.creator, AllocationFree<ProductInjector<TCreate, Metric<TUnit>>>.Item);
    public LeftTerm<TCreate, TQuantity, TRightTerm> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TLeftTerm => new(in this.creator, AllocationFree<ProductInjector<TCreate, Metric<TPrefix, TUnit>>>.Item);
    public LeftTerm<TCreate, TQuantity, TRightTerm> NonStandard<TUnit>() where TUnit : INoSystemUnit, TLeftTerm => new(in this.creator, AllocationFree<ProductInjector<TCreate, NonStandard<TUnit>>>.Item);
    public LeftTerm<TCreate, TQuantity, TRightTerm> Si<TUnit>() where TUnit : ISiUnit, TLeftTerm => new(in this.creator, AllocationFree<ProductInjector<TCreate, Si<TUnit>>>.Item);
    public LeftTerm<TCreate, TQuantity, TRightTerm> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TLeftTerm => new(in this.creator, AllocationFree<ProductInjector<TCreate, Si<TPrefix, TUnit>>>.Item);
}
