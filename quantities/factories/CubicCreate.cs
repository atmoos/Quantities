using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct CubicTo : ICreate, IAliasingCreate
{
    private readonly Quant value;
    internal CubicTo(in Quant value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.As<Cube, TMeasure>();
    Quant IAliasingCreate.Create<TMeasure, TAlias>() => this.value.Alias<TMeasure, TAlias>();
}

public readonly struct CubicCreate : ICreate, IAliasingCreate
{
    private readonly Double value;
    internal CubicCreate(in Double value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.To<Cube, TMeasure>();
    Quant IAliasingCreate.Create<TMeasure, TAlias>() => this.value.Alias<TMeasure, TAlias>();
}
