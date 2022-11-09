using Quantities.Dimensions;
using Quantities.Measures.Other;
using Quantities.Measures.Si;

namespace Quantities.Measures.Math;

internal sealed class PowerOf<TDim> : ICreateLinear<Quant>
    where TDim : IDimension
{
    public Quant CreateOther<TOther>(in Double value) where TOther : IOther, ILinear
    {
        return BuildOther<OtherOf<TDim, TOther>>.With(in value);
    }

    public Quant CreateSi<TSi>(in Double value) where TSi : ISi, ILinear => BuildSi<SiOf<TDim, TSi>>.With(in value);
}