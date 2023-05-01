using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct SquareTo<TQuantity> : IPowerCreate<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private readonly Quant value;
    internal SquareTo(in Quant value) => this.value = value;
    TQuantity IPowerCreate<TQuantity>.Create<TMeasure>() => TQuantity.Create(this.value.To<Square, TMeasure>());
    TQuantity IPowerCreate<TQuantity>.Create<TMeasure, TAlias>() => TQuantity.Create(this.value.As<TMeasure, TAlias>());
}

public readonly struct SquareCreate<TQuantity> : IPowerCreate<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private readonly Double value;
    internal SquareCreate(in Double value) => this.value = value;
    TQuantity IPowerCreate<TQuantity>.Create<TMeasure>() => TQuantity.Create(this.value.To<Square, TMeasure>());
    TQuantity IPowerCreate<TQuantity>.Create<TMeasure, TAlias>() => TQuantity.Create(this.value.As<TMeasure, TAlias>());
}
