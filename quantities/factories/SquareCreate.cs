using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct SquareTo : ICreate, IInjectCreate, ISquare
{
    private readonly Quant value;
    internal SquareTo(in Quant value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.To<Square, TMeasure>();
    Quant IInjectCreate.Create<TMeasure, TAlias>() => this.value.As<TMeasure, TAlias>();
}

public readonly struct SquareCreate : ICreate, IInjectCreate, ISquare
{
    private readonly Double value;
    internal SquareCreate(in Double value) => this.value = value;
    Quant ICreate.Create<TMeasure>() => this.value.To<Square, TMeasure>();
    Quant IInjectCreate.Create<TMeasure, TAlias>() => this.value.As<TMeasure, TAlias>();
}
