namespace Quantities.Prefixes;

public interface IPrefix : ITransform, IRepresentable
{
}
public interface IMetricPrefix : IPrefix
{
}
public interface IBinaryPrefix : IPrefix
{
}
