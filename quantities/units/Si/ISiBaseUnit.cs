namespace Quantities.Unit.Si;

public interface ISiBaseUnit : ISiUnit
{
    static Double ITransform.ToSi(in Double value) => value;
    static Double ITransform.FromSi(in Double value) => value;
}
