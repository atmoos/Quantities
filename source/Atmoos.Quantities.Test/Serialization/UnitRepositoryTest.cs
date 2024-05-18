using System.Reflection;
using Atmoos.Quantities.Serialization;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Test.Serialization;

public class UnitRepositoryTest
{
    [Fact]
    public void TypeRetrievalThrowsUserFriendlyMessageWhenDefiningAssemblyHasNotBeenReferenced()
    {
        var fancy = MyFancyUnit.Representation;
        var repo = UnitRepository.Create(Array.Empty<Assembly>());

        var actual = Assert.Throws<ArgumentException>(() => repo.NonStandard(MyFancyUnit.Representation));
        var expectedMessage = $"Could not find the non standard unit '{fancy}'. Has the assembly that defines '{fancy}' been registered with the deserializer?";
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Fact]
    public void TypeRetrievalRetrievesCorrectTypeWhenAssemblyHasBeenRegistered()
    {
        var expected = typeof(MyFancyUnit);
        var repo = UnitRepository.Create(new[] { expected.Assembly });

        var actual = repo.NonStandard(MyFancyUnit.Representation);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TypeRetrievalOfBaseSiUnitsWorksWithoutRegistrationOfQuantityAssembly()
    {
        var expected = typeof(Metre);
        var repo = UnitRepository.Create(Array.Empty<Assembly>());

        var actual = repo.Si(Metre.Representation);

        Assert.Equal(expected, actual);
    }

    // This must be a public struct...
    public readonly struct MyFancyUnit : INonStandardUnit
    {
        public static String Representation => "Fancy!";
        public static Transformation ToSi(Transformation self) => self * 42;
    }
}
