using Quantities.Dimensions;
using Quantities.Measures.Imperial;
using Quantities.Measures.Si;

namespace Quantities.Measures.Math;

internal sealed class PowerOf<TDim> : ICreateLinear<Quant>
    where TDim : IDimension
{
    public Quant CreateOther<TOther>(in Double value) where TOther : IOther, ILinear
    {
        return BuildImperial<ImperialOf<TDim, TOther>>.With(in value);
    }

    public Quant CreateSi<TSi>(in Double value) where TSi : ISi, ILinear => BuildSi<SiOf<TDim, TSi>>.With(in value);
}