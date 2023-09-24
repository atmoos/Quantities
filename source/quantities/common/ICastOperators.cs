namespace Quantities;

public interface ICastOperators<in TSelf, out TResult>
    where TSelf : ICastOperators<TSelf, TResult>
{
    static abstract implicit operator TResult(TSelf self);
}
