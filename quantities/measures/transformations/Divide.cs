namespace Quantities.Measures.Transformations;

internal sealed class Divide : IInject<IInject<Quant>>
{
    public IInject<Quant> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => new Nominator<TMeasure>(in value);
    private sealed class Nominator<TNominator> : IInject<Quant>
        where TNominator : IMeasure
    {
        private readonly Double nominator;
        public Nominator(in Double nominator) => this.nominator = nominator;
        public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Fraction<TNominator, TMeasure>>.With(this.nominator / value);
    }
}