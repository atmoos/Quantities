namespace Quantities;

public interface ITransform
{
    static abstract Transformation ToSi(Transformation self);
}