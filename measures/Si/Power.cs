using Quantities.Unit.Si;
using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Measures.Core;
using Quantities.Measures.Si.Core;

namespace Quantities.Measures.Si
{
    internal sealed class Power<TPrefix, TUnit> : SiMeasure<Linear, Si<TPrefix, TUnit>>, IPower
        where TPrefix : Prefix, new()
        where TUnit : SiUnit, IPower, new()
    {
    }
}