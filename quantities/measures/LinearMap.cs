using Quantities.Measures.Imperial;
using Quantities.Measures.Si;

namespace Quantities.Measures;

internal sealed class LinearMap<T> : ICreate<T>
{
    private readonly ICreateLinear<T> linear;
    public LinearMap(ICreateLinear<T> linear) => this.linear = linear;
    public T CreateSi<TSi>(in Double value) where TSi : ISi => TSi.Inject(in this.linear, in value);
    public T CreateOther<TOther>(in Double value) where TOther : IOther => TOther.Inject(in this.linear, in value);
}
