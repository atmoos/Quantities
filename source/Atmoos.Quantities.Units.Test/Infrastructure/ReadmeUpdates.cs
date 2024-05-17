using Atmoos.Sphere.Text;
using Quantities.Units.Si.Metric;
using Quantities.TestTools;

using static Atmoos.Sphere.Text.LineTags;
using static Quantities.TestTools.Extensions;

namespace Quantities.Units.Test.Infrastructure;

[Trait(AutoGenerate, Kind.Documentation)]
public class ReadmeUpdates
{
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", "Quantities.Units", "readme.md"));

    [Fact]
    public void ExportAllUnits()
    {
        var tag = Markdown.Code("text units");
        readme.InsertSection(tag, Export.AllUnits("Units", typeof(Gram).Assembly));
    }
}
