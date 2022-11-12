namespace Quantities.Measures.Transformations;

internal sealed class LinearMap<T> : ICreate<T>
{
    private readonly ICreateLinear<T> linear;
    public LinearMap(ICreateLinear<T> linear) => this.linear = linear;
    public T Create<TMeasure>(in Double value) where TMeasure : IMeasure => TMeasure.Inject(in this.linear, in value);
}
