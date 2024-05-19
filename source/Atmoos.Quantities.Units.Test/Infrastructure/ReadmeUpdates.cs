using Atmoos.Sphere.Text;
using Atmoos.Quantities.Units.Si.Metric;
using Atmoos.Quantities.TestTools;

using static Atmoos.Sphere.Text.LineTags;
using static Atmoos.Quantities.TestTools.Extensions;

namespace Atmoos.Quantities.Units.Test.Infrastructure;

[Trait(AutoGenerate, Kind.Documentation)]
public class ReadmeUpdates
{
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", "Atmoos.Quantities.Units", "readme.md"));

    [Fact]
    public void ExportAllUnits()
    {
        var tag = Markdown.Code("text units");
        readme.InsertSection(tag, Export.AllUnits("Units", typeof(Gram).Assembly));
    }
}
