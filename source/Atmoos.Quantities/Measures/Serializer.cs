using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Measures;

internal sealed class Serializer<TUnit>(String system)
    where TUnit : IUnit
{
    private readonly String system = system.ToLowerInvariant();
    public void Write(IWriter writer, Int32 exponent)
        => this.Write(writer, exponent, static _ => { });
    public void Write<TPrefix>(IWriter writer, Int32 exponent)
        where TPrefix : IPrefix
            => this.Write(writer, exponent, static w => w.Write("prefix", TPrefix.Representation));
    private void Write(IWriter writer, Int32 exponent, Action<IWriter> prefix)
    {
        writer.Start(this.system);
        if (exponent != 1) {
            writer.Write("exponent", exponent);
        }
        prefix(writer);
        writer.Write("unit", TUnit.Representation);
        writer.End();
    }
}
