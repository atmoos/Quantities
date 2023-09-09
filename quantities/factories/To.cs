using Quantities.Measures;

namespace Quantities.Factories;

public readonly struct To : ICreate
{
    private readonly Quantity value;
    internal To(in Quantity value) => this.value = value;
    Quantity ICreate.Create<TMeasure>() => this.value.Project(Measure.Of<TMeasure>());
    Quantity ICreate.Create<TMeasure, TAlias>() => this.value.Project(Measure.Of<TMeasure, TAlias>());
}
