using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct SquareTo<TQuantity> : ILinearCreate<TQuantity>, ILinearInjectCreate<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private readonly Quant value;
    internal SquareTo(in Quant value) => this.value = value;
    TQuantity ILinearCreate<TQuantity>.Create<TMeasure>() => TQuantity.Create(this.value.To<Square, TMeasure>());
    TQuantity ILinearInjectCreate<TQuantity>.Create<TMeasure, TAlias>() => TQuantity.Create(this.value.As<TMeasure, TAlias>());
}

public readonly struct SquareCreate<TQuantity> : ILinearCreate<TQuantity>, ILinearInjectCreate<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private readonly Double value;
    internal SquareCreate(in Double value) => this.value = value;
    TQuantity ILinearCreate<TQuantity>.Create<TMeasure>() => TQuantity.Create(this.value.To<Square, TMeasure>());
    TQuantity ILinearInjectCreate<TQuantity>.Create<TMeasure, TAlias>() => TQuantity.Create(this.value.As<TMeasure, TAlias>());
}
