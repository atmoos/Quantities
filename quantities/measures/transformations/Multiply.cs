namespace Quantities.Measures.Transformations;

internal sealed class Multiply : ICreate<ICreate<Quant>>
{
    public ICreate<Quant> Create<TMeasure>(in Double value) where TMeasure : IMeasure => new LeftTerm<TMeasure>(in value);
    private sealed class LeftTerm<TLeft> : ICreate<Quant>
        where TLeft : IMeasure
    {
        private readonly Double leftFactor;
        public LeftTerm(in Double nominator) => this.leftFactor = nominator;
        public Quant Create<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Product<TLeft, TMeasure>>.With(this.leftFactor * value);
    }
}
