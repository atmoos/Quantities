namespace Quantities.Prefixes;

public interface IPrefix : ITransform, IRepresentable { /* marker interface */ }

// ToDo: Consider renaming to IMetricPrefix
public interface ISiPrefix : IPrefix { /* marker interface */ }

// ToDo: Consider renaming to IBinaryPrefix
public interface IIecPrefix : IPrefix { /* marker interface */ }
