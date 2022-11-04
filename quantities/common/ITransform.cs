
namespace Quantities;
public interface ITransform
{
    static abstract Double ToSi(in Double nonSiValue);
    static abstract Double FromSi(in Double siValue);
}