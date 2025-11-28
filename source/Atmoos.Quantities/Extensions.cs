using System.Globalization;
using System.Runtime.CompilerServices;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public static class Extensions
{
    internal static String RoundTripFormat = "G17"; // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#RFormatString
    public static Double ValueOf<T>(Int32 exponent = 1) where T : ITransform => Math.Pow(Polynomial.Of<T>() * 1d, exponent);
    public static Transformation RootedIn<TSi>(this Transformation self) where TSi : ISiUnit => self;
    internal static Transformation From<TBasis>(this Transformation self) where TBasis : IPrefix => TBasis.ToSi(self);
    public static Transformation DerivedFrom<TBasis>(this Transformation self) where TBasis : IUnit, ITransform => TBasis.ToSi(self);
    public static String ToString(this IFormattable formattable, String format) => formattable.ToString(format, CultureInfo.InvariantCulture);
    internal static Quantity To<TMeasure>(this in Double value)
      where TMeasure : IMeasure => Quantity.Of<TMeasure>(in value);
    public static void Serialize<TQuantity>(this IQuantity<TQuantity> quantity, IWriter writer)
      where TQuantity : struct, IQuantity<TQuantity>, IDimension
    {
        quantity.Value.Write(writer, typeof(TQuantity).Name.ToLowerInvariant());
    }
    public static void Deconstruct<TQuantity>(this TQuantity quantity, out Double value, out String unit)
        where TQuantity : struct, IQuantity<TQuantity>, IDimension => quantity.Value.Deconstruct(out value, out unit);
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

    internal static String NameOf<T>() => ClassName(typeof(T));
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
