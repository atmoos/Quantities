
using Quantities.Prefixes;

namespace Quantities.Units.Si;

public interface ISiDerivedUnit : ISiUnit, ITransform
{
    static Double ITransform.ToSi(in Double value) => value;
    static Double ITransform.FromSi(in Double value) => value;
}

public interface ISiDerivedUnit<TPrefix> : ISiDerivedUnit
    where TPrefix : IMetricPrefix
{
    static Double ITransform.ToSi(in Double value) => TPrefix.ToSi(in value);
    static Double ITransform.FromSi(in Double value) => TPrefix.FromSi(in value);
}
