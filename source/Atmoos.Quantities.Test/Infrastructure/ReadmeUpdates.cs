using Atmoos.Sphere.Text;
using Atmoos.Quantities.TestTools;

using static Atmoos.Sphere.Text.LineTags;
using static Atmoos.Quantities.TestTools.Extensions;

namespace Atmoos.Quantities.Test.Infrastructure;

[Trait(AutoGenerate, Kind.Documentation)]
public class ReadmeUpdates
{
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", "Atmoos.Quantities", "readme.md"));

    [Fact]
    public void ExportAllQuantities()
    {
        var tag = Markdown.Code("text quantities");
        var allQuantities = typeof(IQuantity<>).ExportAllImplementations().OrderBy(q => q.Name);
        readme.InsertSection(tag, Export.Section("Atmoos.Quantities", allQuantities, String.Empty));
    }

    [Fact]
    public void ExportAllUnits()
    {
        var mark = Markdown.Code("text units");
        readme.InsertSection(mark, Export.AllUnits("Atmoos.Quantities.Units", typeof(Metre).Assembly));
    }
}
