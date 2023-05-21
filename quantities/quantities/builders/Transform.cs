using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Transform<TNominator> : ICreate
    where TNominator : IMeasure
{
    private readonly Quant value;
    public Transform(in Quant value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => Build<Fraction<TNominator, TMeasure>>.With(in this.value);
}

public readonly struct To : ICreate<ICreate>
{
    private readonly Quant value;
    internal To(in Quant value) => this.value = value;
    ICreate ICreate<ICreate>.Create<TMeasure>() => new Transform<TMeasure>(in this.value);
}