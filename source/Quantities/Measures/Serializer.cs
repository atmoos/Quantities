using Quantities.Core.Serialization;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities.Measures;

internal sealed class Serializer<TUnit>
    where TUnit : IUnit
{
    private readonly String system;
    public Serializer(String system) => this.system = system.ToLowerInvariant();
    public void Write(IWriter writer)
    {
        writer.Start(this.system);
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}
internal sealed class Serializer<TUnit, TPrefix>
    where TPrefix : IPrefix
    where TUnit : IUnit
{
    private readonly String system;
    public Serializer(String system) => this.system = system.ToLowerInvariant();
    public void Write(IWriter writer)
    {
        writer.Start(this.system);
        writer.Write("prefix", TPrefix.Representation);
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}
