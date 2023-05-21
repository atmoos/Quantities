using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct LinearTo : ICreate
{
    private readonly Quant value;
    internal LinearTo(in Quant value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.As<TMeasure>();
}

public readonly struct LinearCreate : ICreate
{
    private readonly Double value;
    internal LinearCreate(in Double value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.As<TMeasure>();
}
