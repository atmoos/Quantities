using System.Text.Json;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Serialization;

namespace Quantities;

public static class Extensions
{
    internal static Quant To<TMeasure>(this in Double value)
      where TMeasure : IMeasure
      => Build<TMeasure>.With(in value);
    internal static Quant As<TMeasure>(this in Quant value)
      where TMeasure : IMeasure
      => Build<TMeasure>.With(in value);
    internal static Quant Alias<TMeasure, TAlias>(this in Double value)
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new()
      => Build<TMeasure>.With<TAlias>(in value);
    internal static Quant Alias<TMeasure, TAlias>(this in Quant value)
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new()
      => Build<TMeasure>.With<TAlias>(in value);
    internal static Quant To<TDim, TMeasure>(this in Double value)
      where TDim : Measures.IDimension where TMeasure : IMeasure
      => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
    internal static Quant As<TDim, TMeasure>(this in Quant value)
      where TDim : Measures.IDimension where TMeasure : IMeasure
      => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
    public static void Serialize<TQuantity>(this IQuantity<TQuantity> quantity, IWriter writer)
      where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        writer.Start(typeof(TQuantity).Name.ToLowerInvariant());
        quantity.Value.Write(writer);
        writer.End();
    }
    public static JsonSerializerOptions EnableQuantities(this JsonSerializerOptions options)
    {
        options.Converters.Add(new QuantitySerialization());
        return options;
    }
}