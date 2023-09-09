namespace Quantities.Measures.Transformations;

internal sealed class Divide : IFactory<IFactory<Quantity>>
{
    public IFactory<Quantity> Create<TMeasure>(in Double value) where TMeasure : IMeasure => new Nominator<TMeasure>(in value);
    private sealed class Nominator<TNominator> : IFactory<Quantity>
        where TNominator : IMeasure
    {
        private readonly Double nominator;
        public Nominator(in Double nominator) => this.nominator = nominator;
        public Quantity Create<TMeasure>(in Double value) where TMeasure : IMeasure => Quantity.Of<Quotient<TNominator, TMeasure>>(this.nominator / value);
    }
}
