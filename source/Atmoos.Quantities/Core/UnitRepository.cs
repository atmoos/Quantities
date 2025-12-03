using System.Collections;
using System.Reflection;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;
using static Atmoos.Quantities.Common.Reflection;
using static Atmoos.Quantities.Dimensions.Tools;

namespace Atmoos.Quantities.Core;

internal interface ISystems
{
    public IEnumerable<String> Prefixes { get; }
    public IEnumerable<(String, Int32)> Exponents { get; }
    public IEnumerable<(String, IEnumerable<String>)> Units { get; }
}

public sealed class UnitRepository
{
    private readonly Repository prefixes, siUnits, metricUnits, imperialUnits, nonStandardUnits;

    private UnitRepository(Repository prefixes, Repository siUnits, Repository metricUnits, Repository imperialUnits, Repository nonStandardUnits)
    {
        this.prefixes = prefixes;
        this.siUnits = siUnits;
        this.metricUnits = metricUnits;
        this.imperialUnits = imperialUnits;
        this.nonStandardUnits = nonStandardUnits;
    }
    internal Type Si(String name) => this.siUnits[name];
    internal Type Metric(String name) => this.metricUnits[name];
    internal Type Imperial(String name) => this.imperialUnits[name];
    internal Type NonStandard(String name) => this.nonStandardUnits[name];
    internal Type Prefix(String name) => this.prefixes[name];
    internal ISystems Subset<TDimension>()
        where TDimension : IDimension
    {
        Type[] interfaces = [.. Interfaces<TDimension>()];
        IEnumerable<(String, IEnumerable<String>)> units = [
            ("si", [..this.siUnits.Select(interfaces)]),
            ("metric", [..this.metricUnits.Select(interfaces)]),
            ("imperial", [..this.imperialUnits.Select(interfaces)]),
            ("any", [..this.nonStandardUnits.Select(interfaces)])
        ];
        return new Systems { Prefixes = this.prefixes, Units = units };
    }
    public static UnitRepository Create() => Create([]);
    public static UnitRepository Create(IEnumerable<Assembly> assemblies)
    {
        var prefixes = new RepoBuilder("prefix", typeof(IPrefix));
        var siUnits = new RepoBuilder("si unit", typeof(ISiUnit));
        var metricUnits = new RepoBuilder("metric unit", typeof(IMetricUnit));
        var imperialUnits = new RepoBuilder("imperial unit", typeof(IImperialUnit));
        var nonStandardUnits = new RepoBuilder("non standard unit", typeof(INonStandardUnit));
        foreach (var type in assemblies.Append(typeof(UnitRepository).Assembly).Reverse().SelectMany(a => a.GetExportedTypes().Where(t => t.IsValueType))) {
            prefixes.TryAdd(type);
            siUnits.TryAdd(type);
            metricUnits.TryAdd(type);
            imperialUnits.TryAdd(type);
            nonStandardUnits.TryAdd(type);
        }
        return new UnitRepository(prefixes.Build(), siUnits.Build(), metricUnits.Build(), imperialUnits.Build(), nonStandardUnits.Build());
    }

    private sealed class Repository : IEnumerable<String>
    {
        private readonly String kind;
        private readonly Dictionary<String, Type> repo;
        public Type this[String name] => Retrieve(name);
        public Repository(String kind, Dictionary<String, Type> repo) => (this.kind, this.repo) = (kind, repo);
        public String[] Select(Type[] interfaces) => [.. this.repo.Where(kv => interfaces.Any(i => kv.Value.IsAssignableTo(i))).Select(kv => kv.Key)];
        private Type Retrieve(String name)
        {
            if (this.repo.TryGetValue(name, out var type)) {
                return type;
            }
            var message = $"Could not find the {this.kind} '{name}'. Has the assembly that defines '{name}' been registered with the deserializer?";
            throw new ArgumentException(message);
        }
        public IEnumerator<String> GetEnumerator() => this.repo.Keys.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private sealed class RepoBuilder
    {
        private readonly Dictionary<String, Type> repo = new();
        private readonly String kind;
        private readonly Type interfaceType;
        public RepoBuilder(String kind, Type interfaceType) => (this.kind, this.interfaceType) = (kind, interfaceType);
        public void TryAdd(Type type)
        {
            if (type.IsAssignableTo(this.interfaceType)) {
                this.repo[GetRepresentation(type)] = type;
            }
        }
        public Repository Build() => new(this.kind, this.repo);
    }

    private sealed class Systems : ISystems
    {
        private static readonly IEnumerable<(String, Int32)> exponents = Enumerable.Range(-9, 19).Where(e => e != 1).Select(e => (Tools.ToExponent(e), e)).ToList();
        public required IEnumerable<String> Prefixes { get; init; }

        public IEnumerable<(String, Int32)> Exponents => exponents;

        public required IEnumerable<(String, IEnumerable<String>)> Units { get; init; }
    }
}

