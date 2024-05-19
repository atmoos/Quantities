using Atmoos.Quantities.TestTools;
using Atmoos.Quantities.Units.Si.Metric;
using Atmoos.Sphere.Text;
using static Atmoos.Quantities.TestTools.Extensions;
using static Atmoos.Sphere.Text.LineTags;

namespace Atmoos.Quantities.Units.Test.Infrastructure;

[Trait(AutoGenerate, Kind.Documentation)]
public class ReadmeUpdates
{
    private const String unitsNamespace = "Atmoos.Quantities.Units";
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", unitsNamespace, "readme.md"));

    [Fact]
    public void ExportAllUnits()
    {
        var tag = Markdown.Code("text units");
        readme.InsertSection(tag, Export.AllUnits(unitsNamespace, typeof(Gram).Assembly));
    }
}
