using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct Create : ICreate
{
    private readonly Double value;
    internal Create(in Double value) => this.value = value;
    Quantity ICreate.Create<TMeasure>() => Quantity.Of<TMeasure>(in this.value);
}
