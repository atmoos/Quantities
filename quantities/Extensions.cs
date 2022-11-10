using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Quantities.Builders;
using Quantities.Unit.Si;

namespace Quantities;

public static class Extensions
{
    public static Velocity Per<TUnit>(this IBuilder<Velocity> builder)
       where TUnit : ISiAcceptedUnit, ITransform, ITime => new(builder.By<TUnit>());
    public static Velocity Per<TPrefix, TUnit>(this IBuilder<Velocity> builder)
       where TPrefix : IPrefix, IScaleDown
       where TUnit : ISiUnit, ITime => new(builder.By<TPrefix, TUnit>());
    public static Velocity PerSecond(this IBuilder<Velocity> builder) => new(builder.By<UnitPrefix, Second>());
}