namespace Atmoos.Quantities.Core.Serialization;

internal interface ISerialize
{
    static abstract void Write(IWriter writer, Int32 exponent);
}
