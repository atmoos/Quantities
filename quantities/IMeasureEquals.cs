namespace Quantities;

internal interface IMeasureEquals<TSelf>
    where TSelf : struct, IMeasureEquals<TSelf>
{
    Boolean EqualMeasure(in TSelf other);
}
