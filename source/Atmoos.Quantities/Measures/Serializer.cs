using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Measures;

internal sealed class Serializer<TUnit>(String system)
    where TUnit : IUnit
{
    private readonly String system = system.ToLowerInvariant();
    public void Write(IWriter writer, Int32 exponent)
    {
        writer.Start(this.system);
        if (exponent != 1) {
            writer.Write("exponent", exponent);
        }
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
    public void Write<TPrefix>(IWriter writer, Int32 exponent)
        where TPrefix : IPrefix
    {
        writer.Start(this.system);
        if (exponent != 1) {
            writer.Write("exponent", exponent);
        }
        writer.Write("prefix", TPrefix.Representation);
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}
