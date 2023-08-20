namespace Quantities.Prefixes;

internal interface IPrefixScale
{
    T Scale<T>(IPrefixInject<T> injector, in Double value);
}

internal static class Metric
{
    public static IPrefixScale Scaling { get; } = new MetricScaling();
    public static IPrefixScale TriadicScaling { get; } = new MetricTriadicScaling();
}

internal static class Binary
{
    public static IPrefixScale Scaling { get; } = new BinaryScaling();
}

file sealed class MetricScaling : IPrefixScale
{
    public T Scale<T>(IPrefixInject<T> injector, in Double value)
    {
        return MetricPrefix.Scale(in value, injector);
    }
}

file sealed class MetricTriadicScaling : IPrefixScale
{
    public T Scale<T>(IPrefixInject<T> injector, in Double value)
    {
        return MetricPrefix.ScaleTriadic(in value, injector);
    }
}

file sealed class BinaryScaling : IPrefixScale
{
    public T Scale<T>(IPrefixInject<T> injector, in Double value)
    {
        return BinaryPrefix.Scale(in value, injector);
    }
}
