using Quantities.Units.Si.Metric;
using Quantities.TestTools;

using static Quantities.TestTools.Extensions;

namespace Quantities.Units.Test.Infrastructure;

[Trait(AutoGenerate, Kind.Documentation)]
public class ReadmeUpdates
{
    private static readonly FileInfo readme = RepoDir.FindFile(Path.Combine("source", "Quantities.Units", "readme.md"));

    [Fact]
    public void ExportAllUnits()
    {
        InsertCode(readme, "units", Export.AllUnits("Units", typeof(Gram).Assembly));
    }
}
