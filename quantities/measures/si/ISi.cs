namespace Quantities.Measures.Si;

public interface ISi : IRepresentable
{
    static abstract Double Normalise(in Double value);
    static abstract Double Renormalise(in Double value);
}

public interface ISi<TDim, TDimension> : ISi
    where TDim : IDimension
    where TDimension : Dimensions.IDimension, Dimensions.ILinear
{

}
