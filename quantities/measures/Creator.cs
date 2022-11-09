using Quantities.Measures.Other;
using Quantities.Measures.Si;

namespace Quantities.Measures;

internal readonly ref struct Creator<T>
{
    private readonly Double value;
    private readonly ICreate<T> creator;
    public Creator(in Double value, in ICreate<T> creator)
    {
        this.value = value;
        this.creator = creator;
    }
    public readonly T CreateSi<TSi>() where TSi : ISi => this.creator.CreateSi<TSi>(in this.value);
    public readonly T CreateOther<TOther>() where TOther : IOther => this.creator.CreateOther<TOther>(in this.value);
}