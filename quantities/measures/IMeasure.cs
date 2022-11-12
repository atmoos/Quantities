namespace Quantities.Measures;

internal interface IMeasure : ITransform, IRepresentable
{
    static abstract T Inject<T>(in ICreate<T> create, in Double value);
}