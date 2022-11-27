namespace Quantities;

public interface ITransform
{
    static abstract Double ToSi(in Double self);
    static abstract Double FromSi(in Double value);
}