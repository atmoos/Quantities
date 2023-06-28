namespace Quantities;

internal interface ISerialize
{
    static abstract void Write(IWriter writer);
}
