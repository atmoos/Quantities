
using Quantities.Prefixes;

namespace Quantities.Unit.Si;

public interface ISiDerivedUnit : ISiUnit
{
    static Double ITransform.ToSi(in Double value) => value;
    static Double ITransform.FromSi(in Double value) => value;
}

public interface ISiDerivedUnit<TPrefix> : ISiDerivedUnit
    where TPrefix : IPrefix
{
    static Double ITransform.ToSi(in Double value) => TPrefix.ToSi(in value);
    static Double ITransform.FromSi(in Double value) => TPrefix.FromSi(in value);
}
