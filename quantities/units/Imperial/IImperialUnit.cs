using Quantities.Dimensions;

namespace Quantities.Units.Imperial;

public interface IImperialUnit : IUnit, ITransform { /* marker interface */ }

public interface IImperialUnit<TSelf, TDimension> : IDerived<TDimension>, IImperialUnit
    where TSelf : IImperialUnit<TSelf, TDimension>, TDimension, IImperialUnit
    where TDimension : IDimension
{
    static Transformation ITransform.ToSi(Transformation self) => TSelf.Derived(new From<TDimension>(self));
}
