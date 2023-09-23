using System.Globalization;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Si;

namespace Quantities;

public static class Extensions
{
    internal static Transformation RootedIn<TSi>(this Transformation self) where TSi : ISiUnit => self;
    internal static Transformation From<TBasis>(this Transformation self) where TBasis : IPrefix => TBasis.ToSi(self);
    internal static Transformation DerivedFrom<TBasis>(this Transformation self) where TBasis : IUnit, ITransform => TBasis.ToSi(self);
    public static String ToString(this IFormattable formattable, String format) => formattable.ToString(format, CultureInfo.InvariantCulture);
    internal static Quantity To<TMeasure>(this in Double value)
      where TMeasure : IMeasure
      => Quantity.Of<TMeasure>(in value);
    public static void Serialize<TQuantity>(this IQuantity<TQuantity> quantity, IWriter writer)
      where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        writer.Start(typeof(TQuantity).Name.ToLowerInvariant());
        quantity.Value.Write(writer);
        writer.End();
    }
}
