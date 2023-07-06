using Quantities.Measures;

namespace Quantities.Quantities.Creation;

public readonly struct To : ICreate<ICreate>
{
    private readonly Quant value;
    internal To(in Quant value) => this.value = value;
    ICreate ICreate<ICreate>.Create<TMeasure>() => new Build<TMeasure>(in this.value);

    private sealed class Build<TNominator> : ICreate
        where TNominator : IMeasure
    {
        private readonly Quant value;
        public Build(in Quant value) => this.value = value;
        Quant ICreate.Create<TMeasure>() => Measures.Build<Quotient<TNominator, TMeasure>>.With(in this.value);
    }
}