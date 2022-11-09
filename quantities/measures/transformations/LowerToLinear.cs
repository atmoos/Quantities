using Quantities.Dimensions;
using Quantities.Measures.Other;
using Quantities.Measures.Si;

namespace Quantities.Measures.Transformations;

internal sealed class LowerToLinear : ICreateLinear<Quant>
{
    public Quant CreateOther<TOther>(in Double value) where TOther : IOther, ILinear
    {
        return BuildOther<TOther>.With(in value);
    }

    public Quant CreateSi<TSi>(in Double value) where TSi : ISi, ILinear
    {
        return BuildSi<TSi>.With(in value);
    }
}