using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities.Measures;


internal interface ISerializeMetric<TUnit> : ISerialize
    where TUnit : IUnit
{
    static void ISerialize.Write(IWriter writer)
    {
        writer.Start("metric");
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}
internal interface ISerializeMetric<TPrefix, TUnit> : ISerialize
    where TPrefix : IPrefix
    where TUnit : IUnit
{
    static void ISerialize.Write(IWriter writer)
    {
        writer.Start("metric");
        writer.Write("prefix", TPrefix.Representation);
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}

internal interface ISerializeImperial<TUnit> : ISerialize
    where TUnit : IUnit
{
    static void ISerialize.Write(IWriter writer)
    {
        writer.Start("imperial");
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}
internal interface ISerializeNonStandard<TUnit> : ISerialize
    where TUnit : IUnit
{
    static void ISerialize.Write(IWriter writer)
    {
        writer.Start("any");
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}
