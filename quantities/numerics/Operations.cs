namespace Quantities.Numerics;

internal abstract class Operation
{
    public required Double Value { get; init; }
    public abstract Operation Invert();
}

internal sealed class Addition : Operation
{
    public override Operation Invert() => new Subtraction { Value = Value };
}
internal sealed class Subtraction : Operation
{
    public override Operation Invert() => new Addition { Value = Value };
}
internal sealed class Multiplication : Operation
{
    public override Operation Invert() => new Division { Value = Value };
}
internal sealed class Division : Operation
{
    public override Operation Invert() => new Multiplication { Value = Value };
}
