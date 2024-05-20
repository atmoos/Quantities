using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.System;

public readonly struct Measure<TQuantity>
    where TQuantity : IFactory<TQuantity>
{
    private readonly ICreator value;
    internal Measure(ICreator value) => this.value = value;
    public static TQuantity operator *(Double value, Measure<TQuantity> unit) => TQuantity.Create(unit.value.Create(value));
}
