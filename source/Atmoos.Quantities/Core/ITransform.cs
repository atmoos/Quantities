namespace Atmoos.Quantities.Core;

public interface ITransform
{
    static abstract Transformation ToSi(Transformation self);
}
