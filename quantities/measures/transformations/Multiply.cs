namespace Quantities.Measures.Transformations;

internal sealed class Multiply : IInject<IInject<Quantity>>
{
    public IInject<Quantity> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => new LeftTerm<TMeasure>(in value);
    private sealed class LeftTerm<TLeft> : IInject<Quantity>
        where TLeft : IMeasure
    {
        private readonly Double leftFactor;
        public LeftTerm(in Double leftFactor) => this.leftFactor = leftFactor;
        public Quantity Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Product<TLeft, TMeasure>>.With(this.leftFactor * value);
    }
}
