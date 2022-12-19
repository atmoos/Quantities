namespace Quantities;

internal interface ISerialize
{
    static abstract void Write(IWriter writer);
}

internal interface ISerializable
{
    public void Write(IWriter writer);
}
