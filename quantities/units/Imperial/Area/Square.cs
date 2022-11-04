using Quantities.Dimensions;

namespace Quantities.Unit.Imperial.Area;

/* ToDo: Delete Comment
public class Square : IArea<ILength> { // I don't understand why this class is required :-(      }
*/

// ToDo: Add IImperial to generic constraints again.
public readonly struct Square<TUnit> : IImperial, IArea<TUnit>
    where TUnit : IUnit, ITransform, ILength
{
    private static readonly Double toSi = Math.Pow(TUnit.ToSi(1d), 2);
    private static readonly Double fromSi = Math.Pow(TUnit.FromSi(1d), 2);
    public static Double FromSi(in Double siValue) => siValue * fromSi;
    public static Double ToSi(in Double nonSiValue) => nonSiValue * toSi;
    public static String Representation { get; } = $"sq {TUnit.Representation}";
}
