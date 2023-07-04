using Quantities.Measures;

namespace Quantities.Quantities.Creation;

public readonly struct Create : ICreate<ICreate>
{
    private readonly Double value;
    internal Create(in Double value) => this.value = value;
    ICreate ICreate<ICreate>.Create<TMeasure>() => new Build<TMeasure>(in this.value);

    private sealed class Build<TNominator> : ICreate
        where TNominator : IMeasure
    {
        private readonly Double value;
        public Build(in Double value) => this.value = value;
        Quant ICreate.Create<TMeasure>() => Measures.Build<Quotient<TNominator, TMeasure>>.With(in this.value);
    }
}