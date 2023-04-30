using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities.Roots;

// ToDo: This should be used as basis for some kind of root object to also be used 
internal sealed class UnitRoot<TSiUnit> : IRoot
    where TSiUnit : struct, ISiUnit
{
    public static Quant One { get; } = 1d.As<Si<TSiUnit>>();
    public static Quant Zero { get; } = 0d.As<Si<TSiUnit>>();
    Quant IPrefixInject<Quant>.Identity(in Double value) => value.As<Si<TSiUnit>>();
    Quant IPrefixInject<Quant>.Inject<TPrefix>(in Double value) => value.As<Si<TPrefix, TSiUnit>>();
}