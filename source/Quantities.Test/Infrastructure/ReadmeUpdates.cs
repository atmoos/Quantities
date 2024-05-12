using Atmoos.Sphere.Text;
using Quantities.TestTools;

using static Atmoos.Sphere.Text.LineTags;
using static Quantities.TestTools.Extensions;

namespace Quantities.Test.Infrastructure;

[Trait(AutoGenerate, Kind.Documentation)]
public class ReadmeUpdates
{
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", "Quantities", "readme.md"));

    [Fact]
    public void ExportAllQuantities()
    {
        var tag = Markdown.Code("text quantities");
        var allQuantities = typeof(IQuantity<>).ExportAllImplementations().OrderBy(q => q.Name);
        readme.InsertSection(tag, Export.Section("Quantities", allQuantities, String.Empty));
    }

    [Fact]
    public void ExportAllUnits()
    {
        var mark = Markdown.Code("text units");
        readme.InsertSection(mark, Export.AllUnits("Units", typeof(Metre).Assembly));
    }
}
