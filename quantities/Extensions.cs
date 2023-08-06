using Quantities.Measures;

namespace Quantities;

public static class Extensions
{
    internal static Quant To<TMeasure>(this in Double value)
      where TMeasure : IMeasure
      => Build<TMeasure>.With(in value);
    public static void Serialize<TQuantity>(this IQuantity<TQuantity> quantity, IWriter writer)
      where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        writer.Start(typeof(TQuantity).Name.ToLowerInvariant());
        quantity.Value.Write(writer);
        writer.End();
    }
}