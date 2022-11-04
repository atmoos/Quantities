using Quantities.Prefixes;

namespace Quantities.Unit.Si;

public interface ISiDerivedUnit : ISiUnit
{
    static Int32 ISiUnit.Offset => 0;
}
public interface ISiDerivedUnit<TPrefixOffset> : ISiDerivedUnit
    where TPrefixOffset : IPrefix
{
    static Int32 ISiUnit.Offset => TPrefixOffset.Exponent;
}
