namespace Quantities.Measures;

internal readonly ref struct Creator<T>
{
    private readonly Double value;
    private readonly ICreate<T> creator;
    public Creator(in Double value, in ICreate<T> creator) => (this.value, this.creator) = (value, creator);
    public readonly T Create<TMeasure>() where TMeasure : IMeasure => this.creator.Create<TMeasure>(in this.value);
}