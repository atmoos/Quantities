using Quantities.Measures;

namespace Quantities.Serialization;
internal sealed class ScalarInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new ScalarBuilder<TMeasure>();
}
