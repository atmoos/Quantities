namespace Atmoos.Quantities;

internal interface IMeasureEquality<TSelf>
    where TSelf : struct, IMeasureEquality<TSelf>
{
    Boolean HasSameMeasure(in TSelf other);
}
