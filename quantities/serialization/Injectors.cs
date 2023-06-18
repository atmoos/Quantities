using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class ScalarInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new ScalarBuilder<TMeasure>();
}

internal sealed class PowerInjector<TDim> : IInject
    where TDim : IDimension
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new ScalarBuilder<Power<TDim, TMeasure>>();
}

internal sealed class FractionInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Outer<TMeasure>();

    private sealed class Outer<TNominator> : IInject, IBuilder
        where TNominator : IMeasure
    {
        public IBuilder Append(IInject inject) => new ScalarBuilder<TNominator>();
        public Quant Build(in Double value) => Build<TNominator>.With(in value);
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new FractionalBuilder<TNominator, TMeasure>();
    }

    private sealed class FractionalBuilder<TNominator, TDenominator> : IBuilder
        where TNominator : IMeasure
        where TDenominator : IMeasure
    {
        public IBuilder Append(IInject inject) => throw new NotImplementedException("Please don't use me...");
        public Quant Build(in Double value) => Build<Fraction<TNominator, TDenominator>>.With(in value);
    }
}
