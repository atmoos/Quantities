namespace Quantities.Measures;

// ToDo: This can potentially be removed by use of clever conversions!
internal readonly ref struct Operands
{
    public readonly Double Left;
    public readonly Double Right;
    public Operands(in Double left, in Double right) => (this.Left, this.Right) = (left, right);
}
