using Quantities.Measures;

namespace Quantities.Quantities.Creation;

public readonly struct To : ICreate
{
    private readonly Quant value;
    internal To(in Quant value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => Build<TMeasure>.With(in this.value);
    Quant ICreate.Create<TMeasure, TAlias>() => Build<TMeasure>.With<TAlias>(in this.value);
}