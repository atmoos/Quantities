namespace Quantities.Measures.Transformations;

internal sealed class Multiply : IFactory<IFactory<Quantity>>
{
    public IFactory<Quantity> Create<TMeasure>(in Double value) where TMeasure : IMeasure => new LeftTerm<TMeasure>(in value);
    private sealed class LeftTerm<TLeft> : IFactory<Quantity>
        where TLeft : IMeasure
    {
        private readonly Double leftFactor;
        public LeftTerm(in Double leftFactor) => this.leftFactor = leftFactor;
        public Quantity Create<TMeasure>(in Double value) where TMeasure : IMeasure => Quantity.Of<Product<TLeft, TMeasure>>(this.leftFactor * value);
    }
}
