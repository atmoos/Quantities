namespace Quantities.Measures.Transformations;

internal sealed class Divide : IInject<IInject<Quantity>>
{
    public IInject<Quantity> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => new Nominator<TMeasure>(in value);
    private sealed class Nominator<TNominator> : IInject<Quantity>
        where TNominator : IMeasure
    {
        private readonly Double nominator;
        public Nominator(in Double nominator) => this.nominator = nominator;
        public Quantity Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Quotient<TNominator, TMeasure>>.With(this.nominator / value);
    }
}
