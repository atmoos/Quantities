using System.Globalization;
using Quantities.Measures;
using Quantities.Numerics;

namespace Quantities;

public static class Extensions
{
  public static String ToString(this IFormattable formattable, String format) => formattable.ToString(format, CultureInfo.InvariantCulture);
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
