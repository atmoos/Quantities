using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct To : ICreate
{
    private readonly Quantity value;
    internal To(in Quantity value) => this.value = value;
    Quantity ICreate.Create<TMeasure>() => Build<TMeasure>.With(in this.value);
    Quantity ICreate.Create<TMeasure, TAlias>() => Build<TMeasure>.With<TAlias>(in this.value);
}
