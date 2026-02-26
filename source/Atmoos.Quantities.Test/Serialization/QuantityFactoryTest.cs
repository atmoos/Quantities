using Atmoos.Quantities.Core.Construction;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;
using Atmoos.Quantities.Units.Imperial.Volume;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Serialization;

public class QuantityFactoryTest
{
    private static readonly UnitRepository unitRepository = UnitRepository.Create();

    [Theory]
    [MemberData(nameof(ScalarModels))]
    public void ScalarQuantitiesCanBeBuiltAcrossSystems(QuantityModel model, Func<Double, Object> createExpected)
    {
        const Double value = 23.081;
        var expected = createExpected(value);

        Object actual = model.Unit switch {
            "m" => QuantityFactory<Length>.Create(unitRepository, model).Build(value),
            "h" => QuantityFactory<Time>.Create(unitRepository, model).Build(value),
            "ℓ" => QuantityFactory<Volume>.Create(unitRepository, model).Build(value),
            "pt" => QuantityFactory<Volume>.Create(unitRepository, model).Build(value),
            _ => throw new NotSupportedException(model.Unit)
        };

        Assert.Equal(expected, actual);
    }

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

    [Fact]
    // The main point here is that the type verification still works.
    public void CompoundQuantityCanBeBuiltUsingScalarValue()
    {
        var model = new QuantityModel {
            System = "any",
            Exponent = 1,
            Unit = "not a knot"
        };
        var units = UnitRepository.Create([typeof(QuantityFactoryTest).Assembly]);
        var expected = Velocity.Of(243.1792708715, NonStandard<Knot>());

        var actual = QuantityFactory<Velocity>.Create(units, model).Build(expected.Value);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoAvailableUnitToPick()
    {
        var model = new QuantityModel {
            System = "si",
            Exponent = 1,
            Unit = "something"
        };

        Assert.Throws<ArgumentException>(() => QuantityFactory<Velocity>.Create(unitRepository, model).Build(3));
    }

    [Fact]
    public void UnknownPrefixCannotBeBuilt()
    {
        var model = new QuantityModel {
            System = "si",
            Prefix = "definitely-unknown",
            Exponent = 1,
            Unit = "m"
        };

        Assert.Throws<ArgumentException>(() => QuantityFactory<Length>.Create(unitRepository, model).Build(3));
    }

    [Fact]
    public void UnknownSystemCannotBeBuilt()
    {
        var model = new QuantityModel {
            System = "mystery",
            Exponent = 1,
            Unit = "m"
        };

        Assert.Throws<NotImplementedException>(() => QuantityFactory<Length>.Create(unitRepository, model).Build(3));
    }

    [Fact]
    public void WrongExponent()
    {
        var model = new QuantityModel {
            System = "si",
            Exponent = 2,
            Unit = "m"
        };

        Assert.Throws<InvalidOperationException>(() => QuantityFactory<Velocity>.Create(unitRepository, model).Build(3));
    }

    public static TheoryData<QuantityModel, Func<Double, Object>> ScalarModels()
        => new() {
            {
                new() {
                    System = "si",
                    Exponent = 1,
                    Unit = "m"
                },
                value => Length.Of(value, Si<Metre>())
            },
            {
                new() {
                    System = "metric",
                    Prefix = "k",
                    Exponent = 1,
                    Unit = "h"
                },
                value => Time.Of(value, Metric<Kilo, Hour>())
            },
            {
                new() {
                    System = "metric",
                    Prefix = "k",
                    Exponent = 1,
                    Unit = "ℓ"
                },
                value => Volume.Of(value, Metric<Kilo, Litre>())
            },
            {
                new() {
                    System = "imperial",
                    Exponent = 1,
                    Unit = "pt"
                },
                value => Volume.Of(value, Imperial<Pint>())
            }
        };

    public readonly struct Knot : INonStandardUnit, IVelocity
    {
        public static String Representation => "not a knot";

        public static Transformation ToSi(Transformation self)
        {
            const Double notKnot = 2.193;
            return notKnot * self;
        }
    }
}
