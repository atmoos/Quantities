using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities;

internal static class Operators
{
    extension<TQuantity>(TQuantity quantity) where TQuantity : struct, IQuantity<TQuantity>, IDimension
    {
        public static Boolean operator ==(TQuantity left, TQuantity right) => left.Equals(right);
        public static Boolean operator !=(TQuantity left, TQuantity right) => !left.Equals(right);
        public static Boolean operator >(TQuantity left, TQuantity right) => left.Value > right.Value;
        public static Boolean operator >=(TQuantity left, TQuantity right) => left.Value >= right.Value;
        public static Boolean operator <(TQuantity left, TQuantity right) => left.Value < right.Value;
        public static Boolean operator <=(TQuantity left, TQuantity right) => left.Value <= right.Value;
        public static TQuantity operator +(TQuantity left, TQuantity right) => TQuantity.Create(left.Value + right.Value);
        public static TQuantity operator -(TQuantity left, TQuantity right) => TQuantity.Create(left.Value - right.Value);
        public static TQuantity operator *(Double scalar, TQuantity right) => TQuantity.Create(scalar * right.Value);
        public static TQuantity operator *(TQuantity left, Double scalar) => TQuantity.Create(scalar * left.Value);
        public static TQuantity operator /(TQuantity left, Double scalar) => TQuantity.Create(left.Value / scalar);
        public static Double operator /(TQuantity left, TQuantity right) => left.Value.Ratio(right.Value);
    }
}
