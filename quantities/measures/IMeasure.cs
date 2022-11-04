namespace Quantities.Measures;
public interface IMeasure
{
    static abstract Quant Create(in Double value);
}