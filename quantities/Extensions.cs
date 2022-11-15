using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Quantities.Builders;
using Quantities.Unit;
using Quantities.Unit.Si;

namespace Quantities;

public static class Extensions
{
    public static Velocity Per<TUnit>(this IBuilder<Velocity> builder)
       where TUnit : ISiAcceptedUnit, IUnitInject<ITime>, ITime => new(builder.By<Alias<SiAccepted<TUnit>, TUnit, ITime>>());
    public static Velocity Per<TPrefix, TUnit>(this IBuilder<Velocity> builder)
       where TPrefix : IPrefix, IScaleDown
       where TUnit : ISiBaseUnit, ITime => new(builder.By<Si<TPrefix, TUnit>>());
    public static Velocity PerSecond(this IBuilder<Velocity> builder) => new(builder.By<Si<Second>>());
}