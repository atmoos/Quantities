using System.Globalization;
using System.Runtime.CompilerServices;
using Quantities.Measures;

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

  public static NotImplementedException NotImplemented(Object self, [CallerMemberName] String memberName = "", [CallerLineNumber] Int32 line = 0)
  {
    return NotImplemented(self.GetType(), memberName, line);
  }
  public static NotImplementedException NotImplemented<T>([CallerMemberName] String memberName = "", [CallerLineNumber] Int32 line = 0)
  {
    return NotImplemented(typeof(T), memberName, line);
  }

  private static NotImplementedException NotImplemented(Type type, String memberName, Int32 line)
  {
    return new NotImplementedException($"{type.ClassName()} is missing '{memberName}' on line #{line}.");
  }

  private static String ClassName(this Type t)
  {
    const Char arityTick = '`';
    if (t.IsGenericType) {
      var typeParams = t.GenericTypeArguments.Select(ClassName);
      return $"{t.Name.Split(arityTick)[0]}<{String.Join(", ", typeParams)}>";
    }
    return t.Name;
  }
}
