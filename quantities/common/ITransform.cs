namespace Quantities;

public interface ITransform
{
    // ToDo: Make this method internal!
    static abstract Transformation ToSi(Transformation self);
}
