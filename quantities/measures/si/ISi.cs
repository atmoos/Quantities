namespace Quantities.Measures.Si;

internal interface ISi : IInjectLinear, IRepresentable
{
    static abstract Double Normalise(in Double value);
    static abstract Double Renormalise(in Double value);
}

internal interface ISi<TDim, TSi> : ISi
    where TDim : IDimension
    where TSi : ISi, Dimensions.ILinear
{

}
