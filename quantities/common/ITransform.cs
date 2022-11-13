namespace Quantities;

public interface ITransform
{
    static abstract Double ToSi(in Double value);
    static abstract Double FromSi(in Double value);
}