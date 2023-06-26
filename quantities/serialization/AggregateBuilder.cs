using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class AggregateBuilder : IBuilder
{
    readonly List<IBuilder> scalarBuilders;
    public AggregateBuilder(List<IBuilder> scalarBuilders) => this.scalarBuilders = scalarBuilders;
    public IBuilder Append(IInject inject)
    {
        IBuilder result = this.scalarBuilders[0].Append(inject);
        foreach (var builder in this.scalarBuilders.Skip(1)) {
            inject = result as IInject ?? throw new InvalidOperationException("Need another injector...");
            result = builder.Append(inject);
        }
        return result;
    }
    public Quant Build(in Double value) => throw new KeepUnusedException(this);

    public IBuilder With<TAlias>() where TAlias : IInjector, new() => throw new KeepUnusedException(this);
}
