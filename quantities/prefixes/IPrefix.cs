namespace Quantities.Prefixes;

public interface IPrefix : ITransform, IRepresentable { /* marker interface */ }
public interface IMetricPrefix : IPrefix { /* marker interface */ }
public interface IBinaryPrefix : IPrefix { /* marker interface */ }
