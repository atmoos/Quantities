using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct Create : ICreate
{
    private readonly Double value;
    internal Create(in Double value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => Build<TMeasure>.With(in this.value);
    Quant ICreate.Create<TMeasure, TAlias>() => Build<TMeasure>.With<TAlias>(in this.value);
}
