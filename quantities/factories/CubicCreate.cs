using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct CubicTo : ICreate, IInjectCreate
{
    private readonly Quant value;
    internal CubicTo(in Quant value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.To<Cube, TMeasure>();
    Quant IInjectCreate.Create<TMeasure, TAlias>() => this.value.As<TMeasure, TAlias>();
}

public readonly struct CubicCreate : ICreate, IInjectCreate
{
    private readonly Double value;
    internal CubicCreate(in Double value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.To<Cube, TMeasure>();
    Quant IInjectCreate.Create<TMeasure, TAlias>() => this.value.As<TMeasure, TAlias>();
}
