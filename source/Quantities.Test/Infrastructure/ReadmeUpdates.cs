using Quantities.TestTools;

using static Quantities.TestTools.Extensions;

namespace Quantities.Test.Infrastructure;

public class ReadmeUpdates
{
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", "Quantities", "readme.md"));

    [Fact]
    public void ExportAllQuantities()
    {
        var allQuantities = typeof(IQuantity<>).ExportAllImplementations().OrderBy(q => q.Name);
        InsertCode(readme, "quantities", Export.Section("Quantities", allQuantities, String.Empty));
    }

    [Fact]
    public void ExportAllUnits()
    {
        InsertCode(readme, "units", Export.AllUnits("Units", typeof(Metre).Assembly));
    }
}
