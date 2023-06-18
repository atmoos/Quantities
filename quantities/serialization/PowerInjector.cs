using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class PowerInjector<TDim> : IInject
    where TDim : IDimension
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new ScalarBuilder<Power<TDim, TMeasure>>();
}
