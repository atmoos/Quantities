namespace Quantities.Measures.Transformations;

internal sealed class Multiply : IInject<IInject<Quant>>
{
    public IInject<Quant> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => new LeftTerm<TMeasure>(in value);
    private sealed class LeftTerm<TLeft> : IInject<Quant>
        where TLeft : IMeasure
    {
        private readonly Double leftFactor;
        public LeftTerm(in Double leftFactor) => this.leftFactor = leftFactor;
        public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Product<TLeft, TMeasure>>.With(this.leftFactor * value);
    }
}
