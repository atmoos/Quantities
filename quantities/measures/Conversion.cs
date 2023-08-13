using Quantities.Numerics;

using static Quantities.Numerics.Polynomial;

namespace Quantities.Measures;

internal static class Conversion<ToSi, FromSi>
    where ToSi : ITransform
    where FromSi : ITransform
{
    public static Polynomial Polynomial { get; } = Of(Of<FromSi>().Inverse(ToSi.ToSi(new Transformation())));
}
