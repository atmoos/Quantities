namespace Quantities.Prefixes;

public interface IPrefix : ITransform, IRepresentable
{
    internal static abstract T Scale<T>(IPrefixInject<T> injector, in Double value);
}
public interface IMetricPrefix : IPrefix
{
    static T IPrefix.Scale<T>(IPrefixInject<T> injector, in Double value)
    {
        return MetricPrefix.Scale(in value, injector);
    }
}
public interface IBinaryPrefix : IPrefix
{
    static T IPrefix.Scale<T>(IPrefixInject<T> injector, in Double value)
    {
        return BinaryPrefix.Scale(in value, injector);
    }
}
