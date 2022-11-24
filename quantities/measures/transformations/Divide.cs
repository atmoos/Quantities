using Quantities;
using Quantities.Measures;

namespace Quantities.Measures.Transformations;

internal sealed class Divide : ICreate<ICreate<Quant>>
{
    public ICreate<Quant> Create<TMeasure>(in Double value) where TMeasure : IMeasure => new Nominator<TMeasure>(in value);
    private sealed class Nominator<TNominator> : ICreate<Quant>
        where TNominator : IMeasure
    {
        private readonly Double nominator;
        public Nominator(in Double nominator) => this.nominator = nominator;
        public Quant Create<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Divide<TNominator, TMeasure>>.With(this.nominator / value);
    }
}