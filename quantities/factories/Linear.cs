using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

// Name inspired by: https://en.wikipedia.org/wiki/Degree_of_a_polynomial#Names_of_polynomials_by_degree
public readonly struct Linear<TCreate, TQuantity, TDimension> : IDefaultFactory<TQuantity, TDimension>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>, TDimension
    where TDimension : Dimensions.IDimension
{
    private readonly TCreate creator;
    internal Linear(in TCreate creator) => this.creator = creator;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.creator.Create<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.creator.Create<Si<TPrefix, TUnit>>());
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.creator.Create<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.creator.Create<Metric<TPrefix, TUnit>>());
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension => TQuantity.Create(this.creator.Create<Imperial<TUnit>>());
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension => TQuantity.Create(this.creator.Create<NonStandard<TUnit>>());
}
