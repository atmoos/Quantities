using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Builder<TNominator> : ICreate
    where TNominator : IMeasure
{
    private readonly Double value;
    public Builder(in Double value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => Build<Fraction<TNominator, TMeasure>>.With(in this.value);
}

public readonly struct Create : ICreate<ICreate>
{
    private readonly Double value;
    internal Create(in Double value) => this.value = value;
    ICreate ICreate<ICreate>.Create<TMeasure>() => new Builder<TMeasure>(in this.value);
}