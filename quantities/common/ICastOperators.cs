namespace Quantities;

public interface ICastOperators<in TSelf>
    where TSelf : ICastOperators<TSelf>
{
    static abstract implicit operator Double(TSelf self);
}
