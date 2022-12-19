using Quantities.Measures;

namespace Quantities;

internal interface ISerialize
{
    static abstract void Write(IWriter writer);
}

internal interface ISerializable
{
    internal Quant Quant { get; }
}
