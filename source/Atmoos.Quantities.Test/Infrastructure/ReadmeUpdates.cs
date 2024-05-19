using Atmoos.Quantities.TestTools;
using Atmoos.Sphere.Text;
using static Atmoos.Quantities.TestTools.Extensions;
using static Atmoos.Sphere.Text.LineTags;

namespace Atmoos.Quantities.Test.Infrastructure;

[Trait(AutoGenerate, Kind.Documentation)]
public class ReadmeUpdates
{
    private const String quantitiesNamespace = "Atmoos.Quantities";
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", quantitiesNamespace, "readme.md"));

    [Fact]
    public void ExportAllQuantities()
    {
        var tag = Markdown.Code("text quantities");
        var allQuantities = typeof(IQuantity<>).ExportAllImplementations().OrderBy(q => q.Name);
        readme.InsertSection(tag, Export.Section(quantitiesNamespace, allQuantities, String.Empty));
    }

    [Fact]
    public void ExportAllUnits()
    {
        var mark = Markdown.Code("text units");
        readme.InsertSection(mark, Export.AllUnits("Atmoos.Quantities.Units", typeof(Metre).Assembly));
    }
}
