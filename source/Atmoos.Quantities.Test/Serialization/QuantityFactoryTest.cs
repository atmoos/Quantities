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

    [Theory]
    [MemberData(nameof(ScalarBuilderModels))]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void ScalarBuilderSupportsAliasAndInvertiblePaths(QuantityModel model, Func<Double, Object> createExpected, Boolean includeTestAssembly, String quantity)
    {
        const Double value = 17.53;
        var repository = includeTestAssembly ? UnitRepository.Create([typeof(QuantityFactoryTest).Assembly]) : unitRepository;

        Object actual = quantity switch {
            "frequency" => QuantityFactory<Frequency>.Create(repository, model).Build(value),
            "volume" => QuantityFactory<Volume>.Create(repository, model).Build(value),
            _ => QuantityFactory<Area>.Create(repository, model).Build(value)
        };

        Assert.Equal(createExpected(value), actual);
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
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void ScalarQuantitiesCanBeBuiltFromSingleModelList()
    {
        List<QuantityModel> models = [
            new() {
                System = "si",
                Exponent = 1,
                Unit = "m"
            }
        ];
        var expected = Length.Of(32.923728, Si<Metre>());

        var actual = QuantityFactory<Length>.Create(unitRepository, models).Build(expected.Value);

        Assert.Equal(expected, actual);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void ScalarQuantitiesCanBeBuiltWithNegativeExponent()
    {
        var model = new QuantityModel {
            System = "si",
            Exponent = -1,
            Unit = "s"
        };

        Assert.Throws<InvalidOperationException>(() => QuantityFactory<Length>.Create(unitRepository, model).Build(2.75));
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
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void PrefixesCannotBeAppliedToImperialUnits()
    {
        var model = new QuantityModel {
            System = "imperial",
            Prefix = "k",
            Exponent = 1,
            Unit = Pint.Representation
        };

        Assert.Throws<InvalidOperationException>(() => QuantityFactory<Area>.Create(unitRepository, model).Build(3));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void PrefixesCannotBeAppliedToNonStandardUnits()
    {
        var model = new QuantityModel {
            System = "any",
            Prefix = "m",
            Exponent = 1,
            Unit = NonStandardAliasArea.Representation
        };
        var repository = UnitRepository.Create([typeof(QuantityFactoryTest).Assembly]);

        Assert.Throws<InvalidOperationException>(() => QuantityFactory<Area>.Create(repository, model).Build(3));
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

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void EmptyModelListCannotBeBuilt()
    {
        Assert.Throws<NotSupportedException>(() => QuantityFactory<Length>.Create(unitRepository, []).Build(2));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void MoreThanTwoModelsCannotBeBuilt()
    {
        List<QuantityModel> models = [
            new() {
                System = "si",
                Exponent = 1,
                Unit = "m"
            },
            new() {
                System = "si",
                Exponent = 1,
                Unit = "m"
            },
            new() {
                System = "si",
                Exponent = -1,
                Unit = "s"
            }
        ];

        Assert.Throws<NotSupportedException>(() => QuantityFactory<Velocity>.Create(unitRepository, models).Build(2));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void UnsupportedExponentCannotBeBuilt()
    {
        var model = new QuantityModel {
            System = "si",
            Exponent = 6,
            Unit = "m"
        };

        Assert.Throws<NotSupportedException>(() => QuantityFactory<Length>.Create(unitRepository, model).Build(3));
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void HigherScalarExponentsReachBuildPath(Int32 exponent)
    {
        var model = new QuantityModel {
            System = "si",
            Exponent = exponent,
            Unit = "m"
        };

        Assert.Throws<InvalidOperationException>(() => QuantityFactory<Length>.Create(unitRepository, model).Build(3));
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
    public void CompoundQuantityWithPositiveProductExponentsCanBeBuilt()
    {
        var models = new List<QuantityModel> {
            new() {
                System = "si",
                Exponent = 1,
                Unit = "m"
            },
            new() {
                System = "si",
                Exponent = 1,
                Unit = "m"
            }
        };
        var expected = Area.Of(21.4, Square(Si<Metre>()));

        var actual = QuantityFactory<Area>.Create(unitRepository, models).Build(expected.Value);

        Assert.Equal(expected, actual);
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

    public static TheoryData<QuantityModel, Func<Double, Object>, Boolean, String> ScalarBuilderModels()
        => new() {
            {
                new() {
                    System = "si",
                    Exponent = 1,
                    Unit = SiInvertibleFrequency.Representation
                },
                value => Frequency.Of(value, Si<SiInvertibleFrequency>()),
                true,
                "frequency"
            },
            {
                new() {
                    System = "si",
                    Prefix = "k",
                    Exponent = 1,
                    Unit = SiInvertibleFrequency.Representation
                },
                value => Frequency.Of(value, Si<Kilo, SiInvertibleFrequency>()),
                true,
                "frequency"
            },
            {
                new() {
                    System = "metric",
                    Exponent = 1,
                    Unit = Litre.Representation
                },
                value => Volume.Of(value, Metric<Litre>()),
                false,
                "volume"
            },
            {
                new() {
                    System = "metric",
                    Prefix = "k",
                    Exponent = 1,
                    Unit = Litre.Representation
                },
                value => Volume.Of(value, Metric<Kilo, Litre>()),
                false,
                "volume"
            },
            {
                new() {
                    System = "imperial",
                    Exponent = 1,
                    Unit = Pint.Representation
                },
                value => Volume.Of(value, Imperial<Pint>()),
                false,
                "volume"
            },
            {
                new() {
                    System = "any",
                    Exponent = 1,
                    Unit = NonStandardAliasArea.Representation
                },
                value => Area.Of(value, NonStandard<NonStandardAliasArea>()),
                true,
                "area"
            },
            {
                new() {
                    System = "si",
                    Prefix = "m",
                    Exponent = 1,
                    Unit = SiAliasArea.Representation
                },
                value => Area.Of(value, Si<Milli, SiAliasArea>()),
                true,
                "area"
            },
            {
                new() {
                    System = "si",
                    Exponent = 1,
                    Unit = SiAliasArea.Representation
                },
                value => Area.Of(value, Si<SiAliasArea>()),
                true,
                "area"
            },
            {
                new() {
                    System = "si",
                    Exponent = 1,
                    Unit = MultiAliasArea.Representation
                },
                value => Area.Of(value, Si<MultiAliasArea>()),
                true,
                "area"
            },
            {
                new() {
                    System = "si",
                    Exponent = 1,
                    Unit = MultiInvertibleFrequency.Representation
                },
                value => Frequency.Of(value, Si<MultiInvertibleFrequency>()),
                true,
                "frequency"
            },
            {
                new() {
                    System = "si",
                    Exponent = 1,
                    Unit = AliasAndInvertibleArea.Representation
                },
                value => Area.Of(value, Si<AliasAndInvertibleArea>()),
                true,
                "area"
            },
            {
                new() {
                    System = "metric",
                    Exponent = 1,
                    Unit = MetricInvertibleFrequency.Representation
                },
                value => Frequency.Of(value, Metric<MetricInvertibleFrequency>()),
                true,
                "frequency"
            },
            {
                new() {
                    System = "metric",
                    Prefix = "k",
                    Exponent = 1,
                    Unit = MetricInvertibleFrequency.Representation
                },
                value => Frequency.Of(value, Metric<Kilo, MetricInvertibleFrequency>()),
                true,
                "frequency"
            },
            {
                new() {
                    System = "imperial",
                    Exponent = 1,
                    Unit = ImperialInvertibleFrequency.Representation
                },
                value => Frequency.Of(value, Imperial<ImperialInvertibleFrequency>()),
                true,
                "frequency"
            },
            {
                new() {
                    System = "any",
                    Exponent = 1,
                    Unit = InvertibleFancyFrequency.Representation
                },
                value => Frequency.Of(value, NonStandard<InvertibleFancyFrequency>()),
                true,
                "frequency"
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

    public readonly struct SiAliasArea : ISiUnit, IArea, IPowerOf<ILength>
    {
        public static String Representation => "si-area";

        public static Transformation ToSi(Transformation self) => self * 10;

        static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    }

    public readonly struct NonStandardAliasArea : INonStandardUnit, IArea, IPowerOf<ILength>
    {
        public static String Representation => "fancy-area";

        public static Transformation ToSi(Transformation self) => self * 11;

        static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    }

    public readonly struct MultiAliasArea : ISiUnit, IArea, IPowerOf<ILength>, IPowerOf<ITime>
    {
        public static String Representation => "multi-alias-area";

        public static Transformation ToSi(Transformation self) => self * 3;

        static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();

        static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    }

    public readonly struct AliasAndInvertibleArea : ISiUnit, IArea, IPowerOf<ILength>, IInvertible<ITime>
    {
        public static String Representation => "alias-invertible-area";

        public static Transformation ToSi(Transformation self) => self * 17;

        static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();

        static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    }

    public readonly struct SiInvertibleFrequency : ISiUnit, IFrequency, IInvertible<ITime>
    {
        public static String Representation => "si-freq";

        public static Transformation ToSi(Transformation self) => self * 7;

        static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    }

    public readonly struct MultiInvertibleFrequency : ISiUnit, IFrequency, IInvertible<ITime>, IInvertible<ILength>
    {
        public static String Representation => "multi-invertible-frequency";

        public static Transformation ToSi(Transformation self) => self * 19;

        static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();

        static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    }

    public readonly struct MetricInvertibleFrequency : IMetricUnit, IFrequency, IInvertible<ITime>
    {
        public static String Representation => "metric-freq";

        public static Transformation ToSi(Transformation self) => self * 23;

        static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    }

    public readonly struct ImperialInvertibleFrequency : IImperialUnit, IFrequency, IInvertible<ITime>
    {
        public static String Representation => "imperial-freq";

        public static Transformation ToSi(Transformation self) => self * 29;

        static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    }

    public readonly struct InvertibleFancyFrequency : INonStandardUnit, IFrequency, IInvertible<ITime>
    {
        public static String Representation => "fancy-freq";

        public static Transformation ToSi(Transformation self) => self * 5;

        static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    }
}
