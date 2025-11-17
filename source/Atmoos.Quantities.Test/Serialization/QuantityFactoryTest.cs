

using Atmoos.Quantities.Serialization;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Serialization;

public class QuantityFactoryTest
{
    private static UnitRepository unitRepository = UnitRepository.Create();

    [Fact]
    public void ScalarQuantitiesCanBeBuilt()
    {
        var model = new QuantityModel {
            System = "si",
            Prefix = "c",
            Exponent = 2,
            Unit = "m"
        };
        var expected = Area.Of(32.923728, Square(Si<Centi, Metre>()));

        var actual = QuantityFactory<Area>.Create(unitRepository, model).Build(expected.Value);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CompoundQuantitiesCanBeBuilt()
    {
        // ToDo: Consider allowing any order of the models...
        var models = new List<QuantityModel> {
            new(){
                System = "si",
                Prefix = "m",
                Exponent = 1,
                Unit = "m"
            },
            new(){
                System = "metric",
                Exponent = -1,
                Unit = "h"
            }
        };
        var expected = Velocity.Of(243.1792708715, Si<Milli, Metre>().Per(Metric<Hour>()));

        var actual = QuantityFactory<Velocity>.Create(unitRepository, models).Build(expected.Value);

        Assert.Equal(expected, actual);
    }
}
