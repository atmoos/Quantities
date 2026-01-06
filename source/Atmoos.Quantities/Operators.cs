using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities;

public static class Operators
{
    extension<TQuantity>(TQuantity)
        where TQuantity : struct, IQuantity<TQuantity>, IDimension
    {
        public static Boolean operator ==(in TQuantity left, in TQuantity right) => left.Equals(right);

        public static Boolean operator !=(in TQuantity left, in TQuantity right) => !left.Equals(right);

        public static Boolean operator >(in TQuantity left, in TQuantity right) => left.Value > right.Value;

        public static Boolean operator >=(in TQuantity left, in TQuantity right) => left.Value >= right.Value;

        public static Boolean operator <(in TQuantity left, in TQuantity right) => left.Value < right.Value;

        public static Boolean operator <=(in TQuantity left, in TQuantity right) => left.Value <= right.Value;

        public static TQuantity operator +(in TQuantity left, in TQuantity right) => TQuantity.Create(left.Value + right.Value);

        public static TQuantity operator -(in TQuantity left, in TQuantity right) => TQuantity.Create(left.Value - right.Value);

        public static TQuantity operator *(Double scalar, in TQuantity right) => TQuantity.Create(scalar * right.Value);

        public static TQuantity operator *(in TQuantity left, Double scalar) => TQuantity.Create(scalar * left.Value);

        public static TQuantity operator /(in TQuantity left, Double scalar) => TQuantity.Create(left.Value / scalar);

        public static Double operator /(in TQuantity left, in TQuantity right) => left.Value.Ratio(right.Value);
    }
}
